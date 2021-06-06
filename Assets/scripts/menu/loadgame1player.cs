using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 script for button to send player to the 1st level
     */
public class loadgame1player : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Button myButton = GetComponent<Button>(); // <-- you get access to the button component here

        myButton.onClick.AddListener(Gotooneplayermode);
    }

    // Update is called once per frame
    void Gotooneplayermode()
    {
        SceneManager.LoadScene("scenes/wk5scene");
    }
    void Update () {
		
	}
}
