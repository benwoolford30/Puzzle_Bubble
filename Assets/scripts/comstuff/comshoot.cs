using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 script of basic ai turret shooting balls 
     */
public class comshoot : MonoBehaviour {
    private Vector3 direction;
    private float seconds;
    private float rotation;
    private bool left;
    // Use this for initialization
    void Start()    //initalise variables
    {
        seconds = 0.0f;
        left = false;
        rotation = 0.0f;
        //0.95 and -1.25
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        int gametime = (int)seconds % 60;
        if (gametime % 2 == 0)  //every two secnonds
        {
            if (left == false)
            {
                rotation += 5f; //rotate left
                if (rotation > 150)
                {
                    left = true;
                }
            }
            else
            {
                rotation -= 5f; //rotate right
                if (rotation < -150)
                {
                    left = false;
                }
            }
        }
        GetComponent<mytransform>().SetRotation(new Vector3(0, 0, (rotation))*2*Time.deltaTime );   //applu new rotation
        if (gametime % 3 == 0)  //every 3 seconds
        {
            if (GetComponent<mytransform>().Children != null)
            {
                GetComponent<mytransform>().Children.GetComponent<mytransform>().Freefromparent();
                GetComponent<mytransform>().Children.GetComponent<mytransform>().Direction = vectormaths.VectorNormalised((GetComponent<mytransform>().Children.GetComponent<mytransform>().position - GetComponent<mytransform>().position)) * 10;
                GetComponent<mytransform>().Children = null;    //fire puzzle bubble 
            }
        }
    }
}
