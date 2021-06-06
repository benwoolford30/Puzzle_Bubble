using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 Script for player control of turret 
     */
public class rotateinput : MonoBehaviour {
    private Vector3 direction;
    private float rotate;
	// Use this for initialization
	void Start () {
        rotate = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (GetComponent<mytransform>().angle.z<0.98f)//limit how far the player can rotate the turret left
            { 
                //GetComponent<mytransform>().Rotate(new Vector3(-0, 0, 1.0f));
                GetComponent<mytransform>().Rotate( new Vector3(0, 0, 50.0f)*Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (GetComponent<mytransform>().angle.z > -1.16f)//limit how far the player can rotate the turret right
            {
                //GetComponent<mytransform>().Rotate(new Vector3(0, 0, -1.0f));
                GetComponent<mytransform>().Rotate(new Vector3(0, 0, -50.0f) * Time.deltaTime);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))  //check to see if player wants to fire bubble
        {
            if (GetComponent<mytransform>().Children != null)
            {
                GetComponent<mytransform>().Children.GetComponent<mytransform>().Freefromparent(); //free child from parent 
                GetComponent<mytransform>().Children.GetComponent<mytransform>().Direction = vectormaths.VectorNormalised((GetComponent<mytransform>().Children.GetComponent<mytransform>().position- GetComponent<mytransform>().position))*10; //get normalised unit vector direction
                GetComponent<mytransform>().Children = null;
            }
        }
    }
}
