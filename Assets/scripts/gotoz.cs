using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 Unused script
     */
public class gotoz : MonoBehaviour
{
    public static float oldmouseX;
    public static float oldmouseY;
    // Use this for initialization
    void Start()
    {
        float oldmouseX= Input.mousePosition.x;
        float oldmouseY = Input.mousePosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 directvec = vectormaths.Eulerangletodirectvec(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z));
            Vector3 rightdir = vectormaths.Crossproduct(Vector3.up,directvec);
            transform.Translate(-(rightdir * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 directvec = vectormaths.Eulerangletodirectvec(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z));
            Vector3 rightdir = vectormaths.Crossproduct(Vector3.up, directvec);
            transform.Translate(rightdir * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 directvec = vectormaths.Eulerangletodirectvec(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z));

            transform.Translate(directvec*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 directvec = vectormaths.Eulerangletodirectvec(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z));

            transform.Translate(-(directvec * Time.deltaTime));
        }
        float x=Input.mousePosition.x-oldmouseX;
        float y=Input.mousePosition.y - oldmouseY;
        transform.Rotate(y*0.1f, x*0.1f, 0);
        oldmouseX = Input.mousePosition.x;
        oldmouseY = Input.mousePosition.y;
        
    }
}
