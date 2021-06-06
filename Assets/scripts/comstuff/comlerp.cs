using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 Computer script version of lerp script
     */
public class comlerp : MonoBehaviour {
    public GameObject Self;
    public GameObject target;
    private mytransform objmatrix;
    public mytransform tarmatrix;
    private Vector3 playerposition;
    private Vector3 playerprevpos;
    private bool original;
    private float seconds;
    // Use this for initialization
    void Start()
    {
        seconds = 0.0f;
        objmatrix = GetComponent<mytransform>();
        tarmatrix = target.GetComponent<mytransform>();  
        playerposition = objmatrix.GetPosition();
        playerprevpos = playerposition;    
    }

    // Update is called once per frame
    void Update()
    {
        if (objmatrix.Intersectiontest(tarmatrix) == true)
        {

            objmatrix.Intersectiontest(tarmatrix);
            Vector3 capsulecentre = objmatrix.capsulecollider.Projectpointonline(tarmatrix.collide);
            objmatrix.Goprev();
            tarmatrix.Goprev();
            if (tarmatrix.Direction.y >= 0)
                capsulecentre.y -= tarmatrix.radius;
            else
                capsulecentre.y += tarmatrix.radius;
            tarmatrix.Direction = vectormaths.VectorNormalised(tarmatrix.GetPosition() - capsulecentre) * 10;
            if (objmatrix.Scaling.x > 1)    //collided with top wall
            {
                tarmatrix.Direction = new Vector3(0.0f, 0.0f, 0.0f);
                GameObject Turret = GameObject.FindGameObjectWithTag("comturret");
                if (Turret.GetComponent<mytransform>().Children == null)
                {
                    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    Destroy(go.GetComponent<SphereCollider>());
                    go.AddComponent<mytransform>();
                    go.GetComponent<mytransform>().position = (new Vector3(-0.13f, 3.1f, 0));
                    go.GetComponent<mytransform>().Scaling = (new Vector3(1, 1, 1));
                    go.GetComponent<mytransform>().angle = (new Vector3(0, 0, 0));
                    go.GetComponent<mytransform>().Parentmatrix = Turret;
                    go.GetComponent<mytransform>().doupdate = true;
                    Turret.GetComponent<mytransform>().Children = go;
                    tarmatrix.GetComponent<combubblecol>().bubbles.Add(tarmatrix);
                    go.AddComponent<combubblecol>();
                    go.GetComponent<combubblecol>().bubbles = tarmatrix.GetComponent<combubblecol>().bubbles;
                    bool[] cols = new bool[6];
                    Color[] coloured = new Color[6];
                    for (int i = 0; i < 6; i++)
                    {
                        cols[i] = false;
                        coloured[i] = Color.white;
                    }
                    int count = 0;
                    foreach (mytransform allbubs in go.GetComponent<combubblecol>().bubbles)
                    {
                        if (allbubs.GetComponent<MeshRenderer>().material.color == Color.red && cols[0] == false)
                        {
                            cols[0] = true;
                            coloured[count] = Color.red;
                            count++;
                        }
                        if (allbubs.GetComponent<MeshRenderer>().material.color == Color.magenta && cols[1] == false)
                        {
                            cols[1] = true;
                            coloured[count] = Color.magenta;
                            count++;
                        }
                        if (allbubs.GetComponent<MeshRenderer>().material.color == Color.blue && cols[2] == false)
                        {
                            cols[2] = true;
                            coloured[count] = Color.blue;
                            count++;
                        }
                        if (allbubs.GetComponent<MeshRenderer>().material.color == Color.black && cols[3] == false)
                        {
                            cols[3] = true;
                            coloured[count] = Color.black;
                            count++;
                        }
                        if (allbubs.GetComponent<MeshRenderer>().material.color == Color.green && cols[4] == false)
                        {
                            cols[4] = true;
                            coloured[count] = Color.green;
                            count++;
                        }
                        if (allbubs.GetComponent<MeshRenderer>().material.color == Color.yellow && cols[5] == false)
                        {
                            cols[5] = true;
                            coloured[count] = Color.yellow;
                            count++;
                        }
                    }

                    go.GetComponent<MeshRenderer>().material.SetColor("_Color", coloured[Random.Range(0, count)]);
                    Destroy(tarmatrix.GetComponent<combubblecol>());
                    tarmatrix.tag = "Stationarybubble";
                    tarmatrix = go.GetComponent<mytransform>();
                    GameObject[] Walls = GameObject.FindGameObjectsWithTag("comwall");
                 
                    foreach (GameObject p in Walls)
                    {
                        p.GetComponent< comlerp>().tarmatrix = tarmatrix;
                    }


                }
            }
            tarmatrix.Direction.z = 0;           
        }
        else
        { 
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("menu");
            }
        }
    }
}