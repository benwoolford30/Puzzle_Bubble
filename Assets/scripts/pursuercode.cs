using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Unused script
     */
public class pursuercode : MonoBehaviour {
    private static GameObject Victim;
    private static float changeddirect;
    // Use this for initialization
    void Start () {
         Victim = GameObject.Find("Evader");
        changeddirect=vectormaths.Dotproduct(transform.position, Victim.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        float newdirect = vectormaths.Dotproduct(transform.position, Victim.transform.position);
        if (changeddirect!=newdirect)
        {
            changeddirect = newdirect;
            Vector3 direction=vectormaths.Subtractvectors(Victim.transform.position,transform.position);
            Vector3 unit = vectormaths.VectorNormalised(direction);
            transform.Translate(vectormaths.DivideVector(unit,100));
        }
	}
}
