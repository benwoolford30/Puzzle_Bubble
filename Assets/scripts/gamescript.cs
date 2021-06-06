using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 initialise first level of game- layout of bubbles 
     */
public class gamescript : MonoBehaviour {
    public List<mytransform> bubbles;
    
	// Use this for initialization
	void Start () {
        transform.hideFlags = HideFlags.HideInInspector | HideFlags.NotEditable;
        //x= 66 -- 74
        //y= 20--29
        List<mytransform> startingbubs= new List<mytransform>();
        int count = 0;
        Color col = Color.blue;
        for (int i = 1; i <= 9; i++)    //create rows
        {
            for (int i2 = 1; i2 <= 3; i2++) //create collumns of bubbles
            { 
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Destroy(go.GetComponent<SphereCollider>());
                go.AddComponent<mytransform>();
                go.GetComponent<mytransform>().position = (new Vector3(65+i, 30-i2, 0));
                go.GetComponent<mytransform>().Scaling = (new Vector3(1, 1, 1));
                go.GetComponent<mytransform>().angle = (new Vector3(0, 0, 0));           
                go.GetComponent<mytransform>().doupdate = true;
                //go.GetComponent<mytransform>().bubblesintersect.Add(go.GetComponent<mytransform>());
                go.tag = "Stationarybubble";
                count += 1;
                if (count > 6)
                    count = 1;
                
                switch (count)  //get a certain colour
                {
                    case 1: { col = Color.red; }break;
                    case 2: { col = Color.magenta; } break;
                    case 3: { col = Color.blue; } break;
                    case 4: { col = Color.black; } break;
                    case 5: { col = Color.green; } break;
                    case 6: { col = Color.yellow; } break;
                }
                
                go.GetComponent<MeshRenderer>().material.SetColor("_Color", col);   //set the colour
                startingbubs.Add(go.GetComponent<mytransform>());
            }
        }        
        GameObject firstbub = GameObject.Find("target");
        firstbub.GetComponent<BubbleCollisions>().bubbles = startingbubs;
        firstbub.GetComponent<MeshRenderer>().material.SetColor("_Color", col);
        GameObject nextbub = GameObject.Find("nextbub");
        nextbub.GetComponent<MeshRenderer>().material.SetColor("_Color", col);  //set bubble colours
    }

    // Update is called once per frame
    void Update () {
		
	}
}
