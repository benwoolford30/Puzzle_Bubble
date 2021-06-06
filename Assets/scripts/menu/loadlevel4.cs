using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 Script for navigating to the 3rd level scene
     */
public class loadlevel4 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Button myButton = GetComponent<Button>(); // <-- you get access to the button component here

        myButton.onClick.AddListener(gotoplayervscom);
    }
    void gotoplayervscom()
    {
        SceneManager.LoadScene("scenes/wk5scene 3");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
