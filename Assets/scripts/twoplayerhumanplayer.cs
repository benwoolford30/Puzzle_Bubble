using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 initialise the level/scene for two sets of players
     */
public class twoplayerhumanplayer : MonoBehaviour {
    public List<mytransform> bubbles;
    public List<mytransform> combubbles;
    /*
     component passed to each bubble 
     each time bubble  changes control add ubble to this lsit 
     then assign component to new bubble and remove it from curretn 
     go through list and check for collisions 
         */
    // Use this for initialization
    void Start()
    {
        transform.hideFlags = HideFlags.HideInInspector | HideFlags.NotEditable;
        //x= 66 -- 74
        //y= 20--29
        List<mytransform> startingbubs = new List<mytransform>();
        List<mytransform> comstartingbubs = new List<mytransform>();
        int count = 0;
        Color col = Color.blue;
        for (int i = 1; i <= 9; i++)
        {
            for (int i2 = 1; i2 <= 3; i2++)
            {
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Destroy(go.GetComponent<SphereCollider>());
                go.AddComponent<mytransform>();
                go.GetComponent<mytransform>().position = (new Vector3(51 + i, 30 - i2, 0));
                go.GetComponent<mytransform>().Scaling = (new Vector3(1, 1, 1));
                go.GetComponent<mytransform>().angle = (new Vector3(0, 0, 0));
                go.GetComponent<mytransform>().doupdate = true;
                //go.GetComponent<mytransform>().bubblesintersect.Add(go.GetComponent<mytransform>());
                go.tag = "Stationarybubble";
                GameObject comgo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Destroy(comgo.GetComponent<SphereCollider>());
                comgo.AddComponent<mytransform>();
                comgo.GetComponent<mytransform>().position = (new Vector3(78 + i, 30 - i2, 0));
                comgo.GetComponent<mytransform>().Scaling = (new Vector3(1, 1, 1));
                comgo.GetComponent<mytransform>().angle = (new Vector3(0, 0, 0));
                comgo.GetComponent<mytransform>().doupdate = true;
                //go.GetComponent<mytransform>().bubblesintersect.Add(go.GetComponent<mytransform>());
                comgo.tag = "Stationarybubble";
                count += 1;
                if (count > 6)
                    count = 1;

                switch (count)
                {
                    case 1: { col = Color.red; } break;
                    case 2: { col = Color.magenta; } break;
                    case 3: { col = Color.blue; } break;
                    case 4: { col = Color.black; } break;
                    case 5: { col = Color.green; } break;
                    case 6: { col = Color.yellow; } break;
                }

                go.GetComponent<MeshRenderer>().material.SetColor("_Color", col);
                startingbubs.Add(go.GetComponent<mytransform>());
                comgo.GetComponent<MeshRenderer>().material.SetColor("_Color", col);
                comstartingbubs.Add(comgo.GetComponent<mytransform>());
            }
        }
        //nextbub y=15.2 x=58.3
        GameObject firstbub = GameObject.Find("target");
        firstbub.GetComponent<BubbleCollisions>().bubbles = startingbubs;
        firstbub.GetComponent<MeshRenderer>().material.SetColor("_Color", col);
        GameObject nextbub = GameObject.Find("nextbub");
        nextbub.GetComponent<MeshRenderer>().material.SetColor("_Color", col);
        GameObject firstcombub = GameObject.Find("comtarget");
        firstcombub.GetComponent<combubblecol>().bubbles = comstartingbubs;
        firstcombub.GetComponent<MeshRenderer>().material.SetColor("_Color", col);
        GameObject comnextbub = GameObject.Find("comnextbub");
        comnextbub.GetComponent<MeshRenderer>().material.SetColor("_Color", col);
    }
        // Update is called once per frame
        void Update () {
		
	}
}
