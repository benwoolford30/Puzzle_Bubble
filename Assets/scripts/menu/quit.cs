using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 script to assign a quit button code
     */
public class quit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Button myButton = GetComponent<Button>(); // <-- you get access to the button component here

        myButton.onClick.AddListener(leave);  

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void leave()
    {
        Application.Quit();
    }
}
