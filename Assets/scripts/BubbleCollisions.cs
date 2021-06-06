using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    This script is used to track all stationary bubbles in game, the bubble that is launched holds a list of all stationary bubbles
    when the launched bubble lands it adds itself to the list and a new launch bubble is made and the list is passed to it.
     */
public class BubbleCollisions : MonoBehaviour {
    public List<mytransform> bubbles;
    public mytransform current;
    // Use this for initialization
    void Start () { //get the current bubble to be launched
        current = GetComponent<mytransform>();
	}
    //this function allows dropping ball logic, currently destroy dropped balls immediately for ease
    void recursivebubbledestroy(mytransform bubbly) // go through each bubble that has a link if theres another go again
    {
        if (bubbly.bubblesintersect.Count>0)    //if there is a bubble underneath it
        { 
            foreach (mytransform childbubs in bubbly.bubblesintersect)  //go through all bubbles underneath it
            {
                recursivebubbledestroy(childbubs);  //call function again to go through the next list of bubbles
            }
        }
        Destroy(bubbly.gameObject); //once leaving recursion- delete all unlinked bubbles
    }
	// Update is called once per frame
	void Update () {
        for (int i = current.GetComponent<BubbleCollisions>().bubbles.Count - 1; i > -1; i--)   //check that there aren't any empty slots in the list
        {
            if (current.GetComponent<BubbleCollisions>().bubbles[i] == null)    //if a slot is empty
            {
                current.GetComponent<BubbleCollisions>().bubbles.RemoveAt(i);   //remove element from list
            }           
        }
       
        foreach (mytransform Sphere in bubbles) //check for collisions with all set spheres
            {
                if (Sphere.Intersectiontest(current))   //check to see if the vurrent bubble is colliding with any stationary bubbles
                    {
                     Sphere.bubblesintersect.Add(current);       //add current bubble to list since current is underneath that bubble                 
                        if (current.GetComponent<MeshRenderer>().material.color == Sphere.GetComponent<MeshRenderer>().material.color)  //check if they're the same colour
                            {
                            
                                Sphere.samecolor.Add(current);  //add both bubbles to eachothers lists
                                current.samecolor.Add(Sphere);
                                if (Sphere.samecolor.Count > 1) //check if there are 2 neighbours- this means there are 3 or more connected bubbles in total
                                {
                                    foreach (mytransform bubble in Sphere.samecolor)    //iterate thorugh each bubble 
                                    {
                                       recursivebubbledestroy(bubble);  //destroy any hanging bubbles
                                       Destroy(bubble.gameObject);  //destroy that bubble
                                    }                        
                                 Destroy(Sphere.gameObject);    //destroy instigator bubble
                                }
                            }
                    current.Direction = new Vector3(0.0f, 0.0f, 0.0f);  //set direction of launched bubble to 0
                    GameObject Turret = GameObject.FindGameObjectWithTag("Turret"); //get game turret
                
                    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);   //initialise a new ball
                    Destroy(go.GetComponent<SphereCollider>());
                    go.AddComponent<mytransform>();
                    go.GetComponent<mytransform>().position = (new Vector3(-0.13f, 3.1f, 0));
                    go.GetComponent<mytransform>().Scaling = (new Vector3(1, 1, 1));
                    go.GetComponent<mytransform>().angle = (new Vector3(0, 0, 0));
                    go.GetComponent<mytransform>().Parentmatrix = Turret;
                    go.GetComponent<mytransform>().doupdate = true;
                    Turret.GetComponent<mytransform>().Children = go;   //assign child object to turret
                    current.GetComponent<BubbleCollisions>().bubbles.Add(current);  //place current bubble into list
                    go.AddComponent<BubbleCollisions>();                        
                    go.GetComponent<BubbleCollisions>().bubbles = current.GetComponent<BubbleCollisions>().bubbles; //assign list to new bubble
                    go.GetComponent<BubbleCollisions>().current = go.GetComponent<mytransform>();   //assign new ball to script                      
                    Destroy(current.GetComponent<BubbleCollisions>());  //destroy current script since not needed for that bubble anymore
                    current.tag = "Stationarybubble";     //set current bubble to stationary               
                    bool[] cols = new bool[6];  
                    Color[] coloured = new Color[6];
                    for (int i = 0; i < 6; i++)     //set up colours
                    {
                      cols[i] = false;
                      coloured[i] = Color.white;
                    }
                    int count = 0;
                    foreach (mytransform allbubs in go.GetComponent<BubbleCollisions>().bubbles)    //check how many colours are left in game
                    {
                        if ((allbubs.GetComponent<MeshRenderer>().material.color == Color.red) && cols[0]==false)   //enable red in colour array
                        {
                            cols[0] = true;
                            coloured[count] = Color.red;
                            count++;
                        }
                        if (allbubs.GetComponent<MeshRenderer>().material.color == Color.magenta && cols[1]==false)//enable magenta
                        {
                            cols[1] = true;
                            coloured[count] = Color.magenta;
                            count++;
                        }
                        if (allbubs.GetComponent<MeshRenderer>().material.color == Color.blue && cols[2]==false)    //enable blue
                        {
                            cols[2] = true;
                            coloured[count] = Color.blue;
                            count++;
                        }
                        if (allbubs.GetComponent<MeshRenderer>().material.color == Color.black && cols[3]==false)   //enable black
                        {
                            cols[3] = true;
                            coloured[count] = Color.black;
                            count++;
                        }
                        if (allbubs.GetComponent<MeshRenderer>().material.color == Color.green && cols[4]==false)   //enable green
                        {
                            cols[4] = true;
                            coloured[count] = Color.green;
                            count++;
                        }
                        if (allbubs.GetComponent<MeshRenderer>().material.color == Color.yellow && cols[5]==false)  //enable yellow
                        {
                            cols[5] = true;
                            coloured[count] = Color.yellow;
                            count++;
                        }
                    }
                    GameObject nextbub = GameObject.Find("nextbub");    //get indicator bubble of next buble
                    go.GetComponent<MeshRenderer>().material.SetColor("_Color", nextbub.GetComponent<MeshRenderer>().material.color);//set new ball to next bubble
                    nextbub.GetComponent<MeshRenderer>().material.SetColor("_Color", coloured[Random.Range(0, count)]); //assign the next coloured ball to next bubble

                    GameObject[] Walls = GameObject.FindGameObjectsWithTag("Wall"); //go through all walls ingame
                    foreach (GameObject p in Walls)
                    {
                        p.GetComponent<linearinterpolates>().tarmatrix = go.GetComponent<BubbleCollisions>().current;   //assign new ball to walls to check for collisions
                    }
                break;  //don't need to check anymore balls in list so break out of loop
                   }
            }

    }
}
