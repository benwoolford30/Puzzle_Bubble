using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 code for button to navigate to level 2
     */
public class nav2ndlevel : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Button myButton = GetComponent<Button>(); // <-- you get access to the button component here

        myButton.onClick.AddListener(gotoplayervscom);
    }
    void gotoplayervscom()
    {
        SceneManager.LoadScene("scenes/wk5scene 2");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
