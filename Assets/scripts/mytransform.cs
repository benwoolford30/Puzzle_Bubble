using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 My own version of the Unity Transform component
     */

public class mytransform : MonoBehaviour {
    /*
     Declaration of all variables
         */
    public Vector3 position;   //position- public so it can be edited at runtime like an actual transform component
    public Vector3 angle;       //angle- public so it can be edited at runtime like an actual transform component
    public  Vector3 Scaling;    //Scaling- public so it can be edited at runtime like an actual transform component
    public bool doupdate;   //if enabled- updates the position of the object every frame
    public GameObject Parentmatrix; //parent matrix- if it exists
    public GameObject Children; //check to see if the object has any children
    public Matrix4by4 matrix;   //world matrix
    public Matrix4by4 TRmatrix;// rigid body matrix
    public CapsuleCollider debug;   //used for debug purposes in editor to check to see if my capsules were working correctly
    private Matrix4by4 pitchm;  //pitch matrix
    private Matrix4by4 yawm;    //yaw matrix
    private Matrix4by4 rollm;   //roll matrix
    private Matrix4by4 translatem;  //translation matrix
    private Matrix4by4 rotationm;   //rotation matrix
    private Matrix4by4 scalem;  //scale matrix
    private Matrix4by4 prevmatrix;  //previous matrix- if there was a collision can assign to current matrix rather than recalculate it
    private Matrix4by4 prevpitchm;
    private Matrix4by4 prevyawm;
    private Matrix4by4 prevrollm;
    private Matrix4by4 prevtranslatem;
    private Matrix4by4 prevrotationm;
    private Matrix4by4 prevscalem;    
    private Vector3[] localverts;   //mesh of the object
    private Vector3[] Worldverts;   //mesh in relation to the world space of the game
    private Vector3 centrepoint;    //centrepoint of collider
    public float radius;    //radius of collider    
    public BoundingCircle3D collide;    //sphere collider
    public BoundingCapsule capsulecollider; //capsule collider
    private MeshFilter objmesh; //mesh component- for vertices    
    public Vector3 Direction;   //apply a directional unit vector that r=transforms the matrix every frame
    private Quarty Qrotation;   // UNused- in place of a rotation matrix if used
    public List<mytransform> bubblesintersect;  //store a list of bubbles thaty are intersecting below it
    public List<mytransform> samecolor; //Store a list of bubbles that are intersecting and are the same colour
    
    void Start()    //initialise the transform component
    {
        bubblesintersect = new List<mytransform>(); //initialise lists
        samecolor = new List<mytransform>();
        HideFlags self=GetComponent<Transform>().hideFlags= HideFlags.HideInInspector | HideFlags.NotEditable;  //hide unbitys transform component since we're not using it 
        //doupdate = false;
        //Scaling = new Vector3(1, 1, 1);
        //angle = new Vector3(0.0f, 0.0f, 0.0f);
        Direction = new Vector3(0.0f, 0.0f, 0.0f);
        matrix = Matrix4by4.Identity;
        pitchm = Matrix4by4.Identity;
        yawm = Matrix4by4.Identity;
        rollm = Matrix4by4.Identity;
        translatem = Matrix4by4.Identity;
        rotationm = Matrix4by4.Identity;
        scalem = Matrix4by4.Identity;
        prevmatrix = Matrix4by4.Identity;
        prevpitchm = Matrix4by4.Identity;
        prevyawm = Matrix4by4.Identity;
        prevrollm = Matrix4by4.Identity;
        prevtranslatem = Matrix4by4.Identity;
        prevrotationm = Matrix4by4.Identity;
        prevscalem = Matrix4by4.Identity;
        objmesh = GetComponent<MeshFilter>();
        localverts = objmesh.mesh.vertices;
        Worldverts = objmesh.mesh.vertices;
        Children = null;    
        centrepoint = new Vector3(0, 0, 0);
        for (int i = 0; i < localverts.Length; i++) //add all vertices
        {
            centrepoint += new Vector3(Worldverts[i].x * Mathf.Sign(Worldverts[i].x), Worldverts[i].y * Mathf.Sign(Worldverts[i].y), Worldverts[i].z * Mathf.Sign(Worldverts[i].z));
        }
        centrepoint /= Worldverts.Length;   //get average for centrepoint
        centrepoint = new Vector3(matrix.matrix[0, 3], matrix.matrix[1, 3], matrix.matrix[2, 3]);   
        radius = 0;
        for (int i = 0; i < localverts.Length; i++) //get largest x vertice value
        {
            if (radius < Worldverts[i].x * Mathf.Sign(Worldverts[i].x))
            {
                radius = Worldverts[i].x * Mathf.Sign(Worldverts[i].x);
            }
        }
        radius -= centrepoint.x;    //subtract centrepoint to get the radius on the x
        capsulecollider = null;
        collide = null;
        if ("Wall" == tag || "comwall"==tag)    //check if the object is a capsule
        {
            if (Scaling.y > 1)  //check if horizontal or vertical
                capsulecollider = new BoundingCapsule(new Vector3(centrepoint.x, centrepoint.y - (radius * (Scaling.y * 2)), centrepoint.z), new Vector3(centrepoint.x, centrepoint.y + (radius * (Scaling.y * 2)), centrepoint.z), radius);
            else
            {
                radius = 0;
                for (int i = 0; i < localverts.Length; i++)
                {
                    if (radius < Worldverts[i].y * Mathf.Sign(Worldverts[i].y))
                    {
                        radius = Worldverts[i].y * Mathf.Sign(Worldverts[i].y);
                    }
                }
                radius -= centrepoint.y;
                capsulecollider = new BoundingCapsule(new Vector3(centrepoint.x - (radius * (Scaling.x * 2)), centrepoint.y, centrepoint.z), new Vector3(centrepoint.x + (radius * (Scaling.x * 2)), centrepoint.y, centrepoint.z), radius);

            }
            debug = gameObject.AddComponent<CapsuleCollider>() as CapsuleCollider; 
            debug.center = centrepoint;
            debug.radius = radius;
            debug.height = Scaling.y;
        }
        else
        { 
            collide = new BoundingCircle3D(centrepoint, radius);    //set a bounding sphere collider
        }
    }       
    public void SetScale(Vector3 Scale) //set the scale of the transform 
    {
        prevscalem = scalem;
        Scaling = Scale;
        scalem.matrix[0, 0] = Scaling.x;
        scalem.matrix[1, 1] = Scaling.y;
        scalem.matrix[2, 2] = Scaling.z;
        Recalculatematrix();
    }
    public void SetScale(float Scalex, float Scaley, float Scalez)  //overload
    {
        prevscalem = scalem;
        Scaling.x = Scalex;
        Scaling.y = Scaley;
        Scaling.z = Scalez;
        scalem.matrix[0, 0] = Scaling.x;
        scalem.matrix[1, 1] = Scaling.y;
        scalem.matrix[2, 2] = Scaling.z;
        Recalculatematrix();
    }
    public void Scale(Vector3 Scale)    //scale the transform
    {
        prevscalem = scalem;
        Scaling += Scale;
        scalem.matrix[0, 0] = Scaling.x;
        scalem.matrix[1, 1] = Scaling.y;
        scalem.matrix[2, 2] = Scaling.z;
        Recalculatematrix();    //recalculate all matrices again and apply to mesh
    }
    public void Scale(float Scalex, float Scaley, float Scalez) //overload
    {
        prevscalem = scalem;
        Scaling.x += Scalex;
        Scaling.y += Scaley;
        Scaling.z += Scalez;
        scalem.matrix[0, 0] = Scaling.x;
        scalem.matrix[1, 1] = Scaling.y;
        scalem.matrix[2, 2] = Scaling.z;
        Recalculatematrix();
    }
    public Matrix4by4 TranslationfromMatrix()   //extract the translation matrix from the world matrix
    {
        Matrix4by4 newtranslatem= Matrix4by4.Identity;
        newtranslatem.matrix[0, 3] = matrix.matrix[0, 3];
        newtranslatem.matrix[1, 3] = matrix.matrix[1, 3];
        newtranslatem.matrix[2, 3] = matrix.matrix[2, 3];
        newtranslatem.matrix[3, 3] = matrix.matrix[3, 3];
        position = new Vector3(newtranslatem.matrix[0, 3], newtranslatem.matrix[1, 3], newtranslatem.matrix[2, 3]);
        return newtranslatem;   //return new translation matrix
    }
    public Matrix4by4 ScalefromMatrix() //extract the scale from the world matrix
    {
        Matrix4by4 newscalem = Matrix4by4.Identity;


        Scaling.x=Mathf.Sqrt(vectormaths.LengthSq(new Vector3(matrix.matrix[0, 0],matrix.matrix[1,0],matrix.matrix[2,0])));
        Scaling.y=Mathf.Sqrt(vectormaths.LengthSq(new Vector3(matrix.matrix[0, 1], matrix.matrix[1, 1], matrix.matrix[2, 1])));
        Scaling.z=Mathf.Sqrt(vectormaths.LengthSq(new Vector3(matrix.matrix[0, 2], matrix.matrix[1, 2], matrix.matrix[2, 2])));
        newscalem.matrix[0, 0] = Scaling.x;
        newscalem.matrix[1, 1] = Scaling.y;
        newscalem.matrix[2, 2] = Scaling.z;
        return newscalem;
    } 
    
    public Matrix4by4 RotationfromMatrix()//extract the rotation matrix from the world matrix
    {
        Matrix4by4 newrotatem = Matrix4by4.Identity;
        Vector3 removescalar;
        removescalar.x = Mathf.Sqrt(vectormaths.LengthSq(new Vector3(matrix.matrix[0, 0], matrix.matrix[1, 0], matrix.matrix[2, 0])));
        removescalar.y = Mathf.Sqrt(vectormaths.LengthSq(new Vector3(matrix.matrix[0, 1], matrix.matrix[1, 1], matrix.matrix[2, 1])));
        removescalar.z = Mathf.Sqrt(vectormaths.LengthSq(new Vector3(matrix.matrix[0, 2], matrix.matrix[1, 2], matrix.matrix[2, 2])));
        newrotatem.matrix[0, 0] = matrix.matrix[0, 0] / removescalar.x;
        newrotatem.matrix[1, 0] = matrix.matrix[1, 0] / removescalar.x;
        newrotatem.matrix[2, 0] = matrix.matrix[2, 0] / removescalar.x;

        newrotatem.matrix[0, 1] = matrix.matrix[0, 1] / removescalar.y;
        newrotatem.matrix[1, 1] = matrix.matrix[1, 1] / removescalar.y;
        newrotatem.matrix[2, 1] = matrix.matrix[2, 1] / removescalar.y;

        newrotatem.matrix[0, 2] = matrix.matrix[0, 2] / removescalar.z;
        newrotatem.matrix[1, 2] = matrix.matrix[1, 2] / removescalar.z;
        newrotatem.matrix[2, 2] = matrix.matrix[2, 2] / removescalar.z;
        return newrotatem;
    }
    public Vector3 EulerangfromMatrix() //extract the euler angles  from the world matrix
    {
        Vector3 Euler = new Vector3(0,0,0);
        float m00 = rotationm.matrix[0, 0];
        float m02 = rotationm.matrix[0, 2];
        float m10 = rotationm.matrix[1, 0];
        float m11 = rotationm.matrix[1, 1];
        float m12 = rotationm.matrix[1, 2];
        float m22 = rotationm.matrix[2, 2];

        if (m10 > 0.998)
            {
                Euler.x = 0;
                Euler.y = Mathf.PI / 2;
                Euler.z = Mathf.Atan2(m02, m22);
            }
        else
        {
            if (m10 < -0.998)
                {
                    Euler.x = 0;
                    Euler.y = -Mathf.PI / 2;
                    Euler.z = Mathf.Atan2(m02, m22);
                }
            else
                {
                Euler.x = Mathf.Atan2(-m12,m11);
                Euler.y = Mathf.Asin(m10);
                Euler.z = Mathf.Atan2(-m10,m00);
                }
        }
        return Euler;
    }
    public void Rotate(Vector3 Eulerangles) //rotate the world matrix
    {
        angle +=  vectormaths.Degreestoradians(Eulerangles);    //convert euler agnles from degrees to radians and add onto angle 
        prevpitchm = pitchm;
        prevyawm = yawm;
        prevrollm = rollm;
        prevrotationm = rotationm;  //assign current unaltered matrices to previous
        pitchm.matrix[1, 1] = Mathf.Cos(angle.x);
        pitchm.matrix[1, 2] = -Mathf.Sin(angle.x);
        pitchm.matrix[2, 1] = Mathf.Sin(angle.x);
        pitchm.matrix[2, 2] = Mathf.Cos(angle.x);
        yawm.matrix[0, 0] = Mathf.Cos(angle.y);
        yawm.matrix[0, 2] = Mathf.Sin(angle.y);
        yawm.matrix[2, 0] = -Mathf.Sin(angle.y);
        yawm.matrix[2, 2] = Mathf.Cos(angle.y);
        rollm.matrix[0, 0] = Mathf.Cos(angle.z);
        rollm.matrix[0, 1] = -Mathf.Sin(angle.z);
        rollm.matrix[1, 0] = Mathf.Sin(angle.z);
        rollm.matrix[1, 1] = Mathf.Cos(angle.z);    //store new angle values in matrices 
        
        rotationm = yawm * (pitchm * rollm);    //calculate new rotation matrix
        
        Recalculatematrix();    //recaluclate world matrix and transform mesh
    }
    public void SetRotation(Vector3 Eulerangles)    //set the rotation of world matrix
    {
        angle =  vectormaths.Degreestoradians(Eulerangles); //set angle 
        prevpitchm = pitchm;
        prevyawm = yawm;
        prevrollm = rollm;
        prevrotationm = rotationm;
        pitchm.matrix[1, 1] = Mathf.Cos(angle.x);
        pitchm.matrix[1, 2] = -Mathf.Sin(angle.x);
        pitchm.matrix[2, 1] = Mathf.Sin(angle.x);
        pitchm.matrix[2, 2] = Mathf.Cos(angle.x);
        yawm.matrix[0, 0] = Mathf.Cos(angle.y);
        yawm.matrix[0, 2] = Mathf.Sin(angle.y);
        yawm.matrix[2, 0] = -Mathf.Sin(angle.y);
        yawm.matrix[2, 2] = Mathf.Cos(angle.y);
        rollm.matrix[0, 0] = Mathf.Cos(angle.z);
        rollm.matrix[0, 1] = -Mathf.Sin(angle.z);
        rollm.matrix[1, 0] = Mathf.Sin(angle.z);
        rollm.matrix[1, 1] = Mathf.Cos(angle.z);
        rotationm = yawm * (pitchm * rollm);
        Recalculatematrix();
    }
    public void Freefromparent()    //free child object from parent
    {
        mytransform parentm = Parentmatrix.GetComponent<mytransform>(); //get parent matrix
        matrix=parentm.TRmatrix*matrix; //combine both matrices together
        
        translatem = TranslationfromMatrix();   //extract the combined translation matrix

        rotationm = RotationfromMatrix();   //extract the combined rotation matrix
        angle = EulerangfromMatrix();   //extract the combined euler angles from matrix
        pitchm.matrix[1, 1] = Mathf.Cos(angle.x);
        pitchm.matrix[1, 2] = -Mathf.Sin(angle.x);
        pitchm.matrix[2, 1] = Mathf.Sin(angle.x);
        pitchm.matrix[2, 2] = Mathf.Cos(angle.x);
        yawm.matrix[0, 0] = Mathf.Cos(angle.y);
        yawm.matrix[0, 2] = Mathf.Sin(angle.y);
        yawm.matrix[2, 0] = -Mathf.Sin(angle.y);
        yawm.matrix[2, 2] = Mathf.Cos(angle.y);
        rollm.matrix[0, 0] = Mathf.Cos(angle.z);
        rollm.matrix[0, 1] = -Mathf.Sin(angle.z);
        rollm.matrix[1, 0] = Mathf.Sin(angle.z);
        rollm.matrix[1, 1] = Mathf.Cos(angle.z);    //set new angle values in pitch.yaw and roll matrices
        rotationm = yawm * (pitchm * rollm);
        scalem = ScalefromMatrix(); //extract the combined scale matrix
        matrix = translatem * (rotationm * scalem);
        for (int i = 0; i < localverts.Length; i++)
        {
            Worldverts[i] = (matrix * localverts[i]);   //get world space vertices
        }
        objmesh.GetComponent<MeshFilter>().mesh.vertices = Worldverts;  //assign new mesah to object
        objmesh.GetComponent<MeshFilter>().mesh.RecalculateBounds();
        objmesh.GetComponent<MeshFilter>().mesh.RecalculateNormals();            
        Parentmatrix = null;        //remove parent matrix from object
        
    }
    
    public Matrix4by4 InverseM()    //inverse world matrix-unused
    {
        translatem = translatem.InverseTranslations();  //get inverse of translation matrix
        position.x = translatem.matrix[0, 3];
        position.y = translatem.matrix[1, 3];
        position.z = translatem.matrix[2, 3];
        pitchm = pitchm.InverseRotations();//get inverse of pitch matrix

        yawm = yawm.InverseRotations(); //get inverse of yaw matrix
        rollm = rollm.InverseRotations();       //get inverse of roll matrix
        angle.x = Mathf.Acos(pitchm.matrix[2, 2]);        
        angle.y=Mathf.Acos(yawm.matrix[2, 2]);       
        angle.z=Mathf.Acos(rollm.matrix[1, 1]);
     
        rotationm = yawm * (pitchm * rollm);
        //scalem = scalem.InverseScale();        
        //Scaling.x=scalem.matrix[0, 0];
        //Scaling.y=scalem.matrix[1, 1];
        //Scaling.z=scalem.matrix[2, 2];
        return matrix = scalem * (rotationm * translatem);  //return inverse matrix- 
        //Recalculatematrix();
    }
    private void Transformobject()  //using new matrices transform vertices and assign to mesh
    {
        if (objmesh != null)    //check existing object has a mesh
        {
            if (Parentmatrix != null)   //check if the object has a child-parent transformation
            {
                Matrix4by4 parentm = Parentmatrix.GetComponent<mytransform>().TRmatrix; //get parent TRmatrix

                if (Parentmatrix.GetComponent<mytransform>() != null)  
                {
                    for (int i = 0; i < localverts.Length; i++)
                    {
                        Matrix4by4 parentchild = parentm * matrix; //combine parent and child matrices together


                        Worldverts[i] = parentchild * localverts[i];   //get world vertices from local
                    }

                }

                Parentmatrix.GetComponent<mytransform>().Children = gameObject; //assign object to parent as a child
            }
            else
            {
                for (int i = 0; i < localverts.Length; i++)
                {
                    Worldverts[i] = (matrix * localverts[i]); //get world vertices from local
                }
            }
            objmesh.mesh.vertices = Worldverts; //assign world vertices to object
            objmesh.mesh.RecalculateBounds();
            objmesh.mesh.RecalculateNormals();

          
            centrepoint = new Vector3(0.0f, 0.0f, 0.0f);    //initialise centrepoint
            for (int i = 0; i < localverts.Length; i++) 
            {
                centrepoint += Worldverts[i];
            }
            centrepoint /= localverts.Length;   //get average of all vertices
            radius = 0;
            for (int i = 0; i < localverts.Length; i++)
            {
                if (radius < Worldverts[i].x * Mathf.Sign(Worldverts[i].x))
                {
                    radius = Worldverts[i].x * Mathf.Sign(Worldverts[i].x);
                }
            }
            radius -= centrepoint.x;    //get radius
            capsulecollider = null;
            collide = null;     //reset collider incase 
            if ("Wall" == tag|| "comwall"==tag) //check if the object is a capsule
            {
                if (Scaling.y > 1)  //check horizontal or vertical
                    capsulecollider = new BoundingCapsule(new Vector3(centrepoint.x, centrepoint.y - (radius * (Scaling.y * 2)), centrepoint.z), new Vector3(centrepoint.x, centrepoint.y + (radius * (Scaling.y * 2)), centrepoint.z), radius);
                else
                {
                    radius = 0;
                    for (int i = 0; i < localverts.Length; i++)
                    {
                        if (radius < Worldverts[i].y * Mathf.Sign(Worldverts[i].y))
                        {
                            radius = Worldverts[i].y * Mathf.Sign(Worldverts[i].y);
                        }
                    }
                    radius -= centrepoint.y;
                    capsulecollider = new BoundingCapsule(new Vector3(centrepoint.x - (radius * (Scaling.x * 2)), centrepoint.y, centrepoint.z), new Vector3(centrepoint.x + (radius * (Scaling.x * 2)), centrepoint.y, centrepoint.z), radius);

                }

            }
            else
            {
                collide = new BoundingCircle3D(centrepoint, radius);    //assign a sphere collider
            }
         

        }
    }
    public void Recalculatematrix() //calculate new matrices after either scale,rotation or translation matrix is changed
    {
        prevmatrix = matrix;
        TRmatrix = translatem * rotationm;        
        matrix = translatem * (rotationm * scalem);
        Transformobject();
        
    }
    public void SetPosition(Vector3 pos)    //set position of world matrix
    {
        prevtranslatem = translatem;
        position = pos;
        translatem.SetCol(3, new Vector4(position.x, position.y, position.z, 1));
        Recalculatematrix();
    }
    public Vector3 GetPosition()    //get position of world matrix
    {
        return new Vector3(matrix.matrix[0, 3], matrix.matrix[1, 3], matrix.matrix[2, 3]);
    }
    public Vector3 Getcentre()  //get centrepoint of the mesh
    {
        return centrepoint;
    }
    public void Translate(Vector3 pos)  //translate world matrix
    {
        prevtranslatem = translatem;
        position += pos;
        translatem.matrix[0, 3] = position.x;
        translatem.matrix[1, 3] = position.y;
        translatem.matrix[2, 3] = position.z;
        Recalculatematrix();
    }
    public bool Intersectiontest(mytransform other) //chewck for a collision with an object
    {

        if (collide != null) //check if object has a sphere collider
        {
            if (other.collide != null) //check if other object is a sphere
                return collide.Intersectiontest(other.collide); //do sphere intersect test
            else
                return collide.Intersectiontest(other.capsulecollider); //do capsule intersect test
        }
        else //  object has a capsule collider
        {      
                return capsulecollider.Intersectscircle(other.collide);  
        }
    }
    public void Goprev()    //go back to previous spot- used for intersections
    {

        matrix = prevmatrix;
        pitchm = prevpitchm;
        yawm = prevyawm;
        rollm = prevrollm;
        translatem = prevtranslatem;
        rotationm = prevrotationm;
        scalem = prevscalem;
        matrix = translatem * (rotationm * scalem);
        Transformobject();

    }
    private void Update()   //every frame
    {
        if (doupdate==true) //if enabled 
        { 
            SetScale(Scaling);
            SetPosition(position+(Direction*Time.deltaTime));   //apply direction to object 
            SetRotation(vectormaths.Radianstodegrees(angle));
            Recalculatematrix();
        }
    }

}

