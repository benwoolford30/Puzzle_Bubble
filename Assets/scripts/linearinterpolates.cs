using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 Despite the name this script doesn't do Lerp

    it was amalgamated from my lerp script previously to be used as a a script to check for colliders between the walls and the launched bubble
     */
public class linearinterpolates : MonoBehaviour {
    public GameObject Self;
    public GameObject target;   
    private mytransform objmatrix;
    public mytransform tarmatrix;
    private Vector3 playerposition;
    private Vector3 playerprevpos;
    private bool original;
    private float seconds;
    // Use this for initialization
    void Start () {
        seconds = 0.0f;
        objmatrix = GetComponent<mytransform>();
        tarmatrix = target.GetComponent<mytransform>();   //positions are initialised in unity editor       
        playerposition = objmatrix.GetPosition();
        playerprevpos = playerposition;
      
    }

    // Update is called once per frame
    void Update () {
        //Vector3 translatedv = vectormaths.LinearinterpolatedV(objmatrix.GetCol(3), tarmatrix.GetCol(3), 0.01f);
        

        if (objmatrix.Intersectiontest(tarmatrix)==true)    //check to see if a intersection has occured
        {            
            Vector3 capsulecentre = objmatrix.capsulecollider.Projectpointonline(tarmatrix.collide);    //get point where collision happened using projection 
            objmatrix.Goprev();
            tarmatrix.Goprev();
            if (tarmatrix.Direction.y>=0)   //check to see if bubble has collided with a velocity heading above or below capsule
                capsulecentre.y -= tarmatrix.radius;    //shift centrepoint below
            else
                capsulecentre.y += tarmatrix.radius;    //shift the centrepoint above   
            //enables accurate bounce off walls
            /*
             |
             |  0   hit above
             |0     centrepoint below 
           O |  0   allows prefect rebound
             */
            tarmatrix.Direction = vectormaths.VectorNormalised(tarmatrix.GetPosition()-capsulecentre) * 10; //apply unit vector direction 
            if (objmatrix.Scaling.x > 1)    //collided with top wall
            { 
                tarmatrix.Direction = new Vector3(0.0f, 0.0f, 0.0f);
                GameObject Turret=GameObject.FindGameObjectWithTag("Turret");   //get the turret
                if (Turret.GetComponent<mytransform>().Children == null)    //check to see if the parent doesn't have children already
                {
                    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);   //initalise a new ball
                    Destroy(go.GetComponent<SphereCollider>());                    
                    go.AddComponent<mytransform>();
                    go.GetComponent<mytransform>().position=(new Vector3(-0.13f, 3.1f, 0));
                    go.GetComponent<mytransform>().Scaling = (new Vector3(1, 1, 1));
                    go.GetComponent<mytransform>().angle = (new Vector3(0, 0, 0));
                    go.GetComponent<mytransform>().Parentmatrix = Turret;
                    go.GetComponent<mytransform>().doupdate = true;
                    Turret.GetComponent<mytransform>().Children = go;                   
                    tarmatrix.GetComponent<BubbleCollisions>().bubbles.Add(tarmatrix);
                    go.AddComponent<BubbleCollisions>();
                    go.GetComponent<BubbleCollisions>().bubbles = tarmatrix.GetComponent<BubbleCollisions>().bubbles;   //assign new list 
                    bool[] cols = new bool[6];
                    Color[] coloured = new Color[6];
                    for (int i = 0; i < 6; i++)
                    {
                        cols[i] = false;
                        coloured[i] = Color.white;
                    }
                    int count = 0;
                    foreach (mytransform allbubs in go.GetComponent<BubbleCollisions>().bubbles)    //go through all stationary bubbles and get remanining colours
                    {
                        if (allbubs.GetComponent<MeshRenderer>().material.color == Color.red && cols[0]==false)
                        {
                            cols[0] = true;
                            coloured[count] = Color.red;
                            count++;
                        }
                        if (allbubs.GetComponent<MeshRenderer>().material.color == Color.magenta && cols[1]==false)
                        {
                            cols[1] = true;
                            coloured[count] = Color.magenta;
                            count++;
                        }
                        if (allbubs.GetComponent<MeshRenderer>().material.color == Color.blue && cols[2]==false)
                        {
                            cols[2] = true;
                            coloured[count] = Color.blue;
                            count++;
                        }
                        if (allbubs.GetComponent<MeshRenderer>().material.color == Color.black && cols[3]==false)
                        {
                            cols[3] = true;
                            coloured[count] = Color.black;
                            count++;
                        }
                        if (allbubs.GetComponent<MeshRenderer>().material.color == Color.green && cols[4]==false)
                        {
                            cols[4] = true;
                            coloured[count] = Color.green;
                            count++;
                        }
                        if (allbubs.GetComponent<MeshRenderer>().material.color == Color.yellow && cols[5]==false)
                        {
                            cols[5] = true;
                            coloured[count] = Color.yellow;
                            count++;
                        }
                    }

                    go.GetComponent<MeshRenderer>().material.SetColor("_Color", coloured[Random.Range(0, count)]);  //set next ball colour
                    Destroy(tarmatrix.GetComponent<BubbleCollisions>());
                    tarmatrix.tag = "Stationarybubble";
                    tarmatrix = go.GetComponent<mytransform>();
                    GameObject[] Walls= GameObject.FindGameObjectsWithTag("Wall");
                    foreach (GameObject p in Walls)
                    {
                        p.GetComponent<linearinterpolates>().tarmatrix = tarmatrix; //assign next ball to wall
                    }


                }
            }        
            tarmatrix.Direction.z = 0;  //we don't want the direction in the z axis so it is set to 0 if its somehow not 0         
        }
        else
        { 
            if (Input.GetKey(KeyCode.R))    //reset back to menu
            {
                SceneManager.LoadScene("menu");
            }          
        }
    }
}
