using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 This script is used to shift the floor upwards towards the bubbles
     */
public class floorRise : MonoBehaviour {
    private mytransform objmatrix;
    private float seconds;
    private float accumulaltor; //experimented with fixed update- eventually scrapped
    private const float fixedtimestep = 0.02f;
    private bool startrising;
    // initialization
    void Start () { 
        seconds = 0.0f;
        objmatrix = GetComponent<mytransform>();
        startrising = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (objmatrix.Scaling.x > 1)    //check it is horitzontal 
        {
            seconds += Time.deltaTime;  //add time 
            int gametime = (int)seconds % 60;   //get int value of seconds
            if (startrising == false)   //check to see if 20 seconds has passed
            {
                if (gametime > 20)  //if so
                {
                    startrising = true; //floor can begin rising
                }
            }
            else
            {
                if (gametime % 10 == 0) //every 10 seconds the floor rises upwards
                {
                    objmatrix.Translate(new Vector3(0.0f, 1.0f, 0.0f) * Time.deltaTime);    //apply up direction
                    GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Stationarybubble");   //get list of stationary bubbles
                    if (bubbles.Length > 0) //check the list isn't empty
                    {
                        foreach (GameObject bub in bubbles) //go thorough each member in list
                        {
                            if (objmatrix.Intersectiontest(bub.GetComponent<mytransform>()))    //gameover
                            {
                                SceneManager.LoadScene("menu"); //load menu again
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
