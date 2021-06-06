using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class loadplayersvscom : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Button myButton = GetComponent<Button>(); // <-- you get access to the button component here

        myButton.onClick.AddListener(gotoplayervscom);
    }
    void gotoplayervscom()
    {
        SceneManager.LoadScene("scenes/wk5scene 1");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
