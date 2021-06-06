using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 amended bubble collsiion script to be suitable for a computer or second player 
     */
public class combubblecol : MonoBehaviour {
    public List<mytransform> bubbles;
    public mytransform current;
    // Use this for initialization
    void Start()
    {
        current = GetComponent<mytransform>();
    }
    //this function allows dropping ball logic, currently destroy dropped balls immediately for ease
    void recursivebubbledestroy(mytransform bubbly) // go through each bubble that has a link if theres another go again
    {
        if (bubbly.bubblesintersect.Count > 0)
        {
            foreach (mytransform childbubs in bubbly.bubblesintersect)
            {
                recursivebubbledestroy(childbubs);
            }
        }
        Destroy(bubbly.gameObject); //once leaving recursion- delete a/l unlinkked bubbles
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = current.GetComponent<combubblecol>().bubbles.Count - 1; i > -1; i--)
        {
            if (current.GetComponent<combubblecol>().bubbles[i] == null)
            {
                current.GetComponent<combubblecol>().bubbles.RemoveAt(i);
            }
            //also do code here to see if above bubble exists- if not,kill that bubble and then kill ones below

        }

        foreach (mytransform Sphere in bubbles) //check for collisions with all set spheres
        {
            if (Sphere.Intersectiontest(current))
            {
                Sphere.bubblesintersect.Add(current);
                if (current.GetComponent<MeshRenderer>().material.color == Sphere.GetComponent<MeshRenderer>().material.color)
                {

                    Sphere.samecolor.Add(current);
                    current.samecolor.Add(Sphere);
                    if (Sphere.samecolor.Count > 1)
                    {
                        foreach (mytransform bubble in Sphere.samecolor)
                        {
                            recursivebubbledestroy(bubble);
                            Destroy(bubble.gameObject);
                        }
                        Destroy(Sphere.gameObject);
                    }
                }
                current.Direction = new Vector3(0.0f, 0.0f, 0.0f);
                GameObject Turret = GameObject.FindGameObjectWithTag("comturret");

                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Destroy(go.GetComponent<SphereCollider>());
                go.AddComponent<mytransform>();
                go.GetComponent<mytransform>().position = (new Vector3(-0.13f, 3.1f, 0));
                go.GetComponent<mytransform>().Scaling = (new Vector3(1, 1, 1));
                go.GetComponent<mytransform>().angle = (new Vector3(0, 0, 0));
                go.GetComponent<mytransform>().Parentmatrix = Turret;
                go.GetComponent<mytransform>().doupdate = true;
                Turret.GetComponent<mytransform>().Children = go;
                current.GetComponent<combubblecol>().bubbles.Add(current);
                go.AddComponent<combubblecol>();
                go.GetComponent<combubblecol>().bubbles = current.GetComponent<combubblecol>().bubbles;
                go.GetComponent<combubblecol>().current = go.GetComponent<mytransform>();
                Destroy(current.GetComponent<combubblecol>());
                current.tag = "Stationarybubble";
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
                    if ((allbubs.GetComponent<MeshRenderer>().material.color == Color.red) && cols[0] == false)
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
                GameObject nextbub = GameObject.Find("comnextbub");
                go.GetComponent<MeshRenderer>().material.SetColor("_Color", nextbub.GetComponent<MeshRenderer>().material.color);
                nextbub.GetComponent<MeshRenderer>().material.SetColor("_Color", coloured[Random.Range(0, count)]);

                GameObject[] Walls = GameObject.FindGameObjectsWithTag("comwall");
                foreach (GameObject p in Walls)
                {
                    p.GetComponent<comlerp>().tarmatrix = go.GetComponent<combubblecol>().current;
                }
                break;
            }
        }

    }
}
