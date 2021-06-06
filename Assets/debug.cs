using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debug : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
        Matrix4by4 pitch= new Matrix4by4(new Vector4(1,0,0,0) ,new Vector4(0, Mathf.Cos(5.148722f), Mathf.Sin(5.148722f), 0),new Vector4(0, -Mathf.Sin(5.148722f), Mathf.Cos(5.148722f), 0),new Vector4(0, 0, 0, 1));
        Matrix4by4 yaw = new Matrix4by4(new Vector4( Mathf.Cos(5.654867f), 0, -Mathf.Sin(5.654867f), 0), new Vector4(0, 1, 0, 0), new Vector4(Mathf.Sin(5.654867f), 0, Mathf.Cos(5.654867f), 0), new Vector4(0, 0, 0, 1));
        Matrix4by4 roll = new Matrix4by4(new Vector4(Mathf.Cos(1.082104f), -Mathf.Sin(1.082104f), 0, 0), new Vector4(-Mathf.Sin(1.082104f), Mathf.Cos(1.082104f), 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
        Matrix4by4 rotation=Matrix4by4.Identity;
        rotation = roll* yaw;
        rotation *= pitch;        


    }

    // Update is called once per frame
    void Update () {
		
	}
}
