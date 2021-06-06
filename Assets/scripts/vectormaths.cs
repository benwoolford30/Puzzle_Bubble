using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 My own custom library used to manipulate vectors,matrices,quartnonions,colliders,etc
     */
public class vectormaths  {
    public static Vector2 Addvectors(Vector2 onev, Vector2 twov)
    {        
        return onev + twov;
    }
    public static Vector3 Addvectors(Vector3 onev, Vector3 twov)
    {
        return onev + twov;
    }
    public static Vector3 Addvectors(Vector3 vec, float add)
    {
        return new Vector3(vec.x + add, vec.y + add, vec.z + add);
    }
    public static Vector2 Subtractvectors(Vector2 onev, Vector2 twov)
    {
        return onev - twov;
    }
    public static Vector3 Subtractvectors(Vector3 onev, Vector3 twov)
    {
        return onev - twov;
    }
    public static Vector3 Subtractvectors(Vector3 vec, float subtract)
    {
        return new Vector3(vec.x - subtract, vec.y - subtract, vec.z - subtract);
    }
    public static float Getvlength(Vector2 vec)
    {
        //float xsqr = vec.x * vec.x;
        //float ysqr = vec.y * vec.y;
        //float result = xsqr + ysqr;
        //float answer=Mathf.Sqrt(result);
        //return answer;
        return Mathf.Sqrt(((vec.x * vec.x) + (vec.y * vec.y)));
    }
    public static float Getvlength(Vector3 vec)
    {
        //float xsqr = vec.x * vec.x;
        //float ysqr = vec.y * vec.y;
        //float result = xsqr + ysqr;
        //float answer=Mathf.Sqrt(result);
        //return answer;
        return Mathf.Sqrt(((vec.x * vec.x) + (vec.y * vec.y) + (vec.z * vec.z)));
    }
    public static float LengthSq(Vector2 vec)
    {
        //float xsqr = vec.x * vec.x;
        //float ysqr = vec.y * vec.y;
        //float result = xsqr + ysqr;
        //float answer=Mathf.Sqrt(result);
        //return answer;
        return ((((vec.x * vec.x) + (vec.y * vec.y))));
    }
    public static float LengthSq(Vector3 vec)
    {
        //float xsqr = vec.x * vec.x;
        //float ysqr = vec.y * vec.y;
        //float result = xsqr + ysqr;
        //float answer=Mathf.Sqrt(result);
        //return answer;        
        return ((vec.x * vec.x) + (vec.y * vec.y) + (vec.z * vec.z));
    }
    public static Vector2 MultiplyVector(Vector2 position, float scalar)
    {
        return position*scalar;
    }
    public static Vector3 MultiplyVector(Vector3 position, float scalar)
    {
        return position * scalar;
    }
    public static Vector2 DivideVector(Vector2 position, float divisor)
    {
        return position / divisor;
    }
    public static Vector3 DivideVector(Vector3 position, float divisor)
    {
        return position / divisor;
    }
    public static Vector2 VectorNormalised(Vector2 position  )
    {
        return position/Getvlength(position);
    }
    public static Vector3 VectorNormalised(Vector3 position)
    {
        return position / Getvlength(position);
    }
    public static float Dotproduct(Vector2 VecOne, Vector2 Vectwo, bool Normalise=true)
    {
        if (Normalise==true)
        {
         VecOne = VectorNormalised(VecOne);
         Vectwo = VectorNormalised(Vectwo);
        //float dotproduct = (N1.x * N2.x) + (N1.y * N2.y);
        }
        //return dotproduct;
        return ((VecOne.x * Vectwo.x) + (VecOne.y * Vectwo.y));
    }
    public static float Dotproduct(Vector3 VecOne, Vector3 Vectwo, bool Normalise = true)
    {
        if (Normalise == true)
        {
            VecOne = VectorNormalised(VecOne);
            Vectwo = VectorNormalised(Vectwo);
            //float dotproduct = (N1.x * N2.x) + (N1.y * N2.y)+ (N1.z * N2.z);
        }
        //return dotproduct;
        return ((VecOne.x * Vectwo.x) + (VecOne.y * Vectwo.y) + (VecOne.z * Vectwo.z));

    }
    public static Vector2 Degreestoradians(Vector2 degreevec)
    {      
        return degreevec/(180/Mathf.PI);
    }
    public static float Degreestoradians(float degreevec)
    {
        return degreevec / (180 / Mathf.PI);
    }
    public static Vector3 Degreestoradians(Vector3 degreevec)
    {
        return degreevec / (180 / Mathf.PI);
    }
    public static Vector3 Degreestoradians(Vector4 degreevec)
    {
        return degreevec / (180 / Mathf.PI);
    }
    public static Vector2 Radianstodegrees(Vector2 radianvec)
    {
        return radianvec * 180 / Mathf.PI;
    }
    public static Vector3 Radianstodegrees(Vector3 radianvec)
    {
        return radianvec * 180 / Mathf.PI;
    }
    public static Vector3 Radianstodegrees(Vector4 radianvec)
    {
        return radianvec * 180 / Mathf.PI;
    }
    public static Vector2 Rangletodirectionvec(float rangle)
    {
        //Vector2 direction;
        //direction.x= Mathf.Cos(rangle);
        //direction.y= Mathf.Sin(rangle);
        //return direction;
        return new Vector2(Mathf.Cos(rangle), Mathf.Sin(rangle));
    }
    public static Vector3 Eulerangletodirectvec(Vector3 eulerangle)
    {
        /*
        Vx = cos(Y) cos(P)
        Vy = sin(P)
        Vz = cos(P) sin(Y)
         */
        Vector3 direction;
        direction.x = Mathf.Cos(eulerangle.y) * Mathf.Cos(eulerangle.x);
        direction.y = Mathf.Sin(eulerangle.x);
        direction.z = Mathf.Cos(eulerangle.x) * Mathf.Sin(eulerangle.y);
        return direction;
    }
    public static Vector3 Crossproduct(Vector3 Vecone, Vector3 Vectwo)
    {
        Vector3 crossproduct;
        crossproduct.x = (Vecone.y * Vectwo.z) - (Vecone.z * Vectwo.y);
        crossproduct.y = (Vecone.z * Vectwo.x) - (Vecone.x * Vectwo.z);
        crossproduct.z = (Vecone.x * Vectwo.y) - (Vecone.y * Vectwo.x);
        return crossproduct;
    }
    public static Vector2 LinearinterpolatedV(Vector2 onev, Vector2 twov, float fraction)
    {

        return (onev*(1.0f-fraction))+ (twov*fraction);
    }
    public static Vector3 LinearinterpolatedV(Vector3 onev, Vector3 twov, float fraction)
    {

        return (onev * (1.0f - fraction)) + (twov * fraction);
    }
    public static Vector4 LinearinterpolatedV(Vector4 onev, Vector4 twov, float fraction)
    {

        return (onev * (1.0f - fraction)) + (twov * fraction);
    }
    public static Vector3 Rotatevertexaroundaxis(float angle, Vector3 axisdir, Vector3 vertex)
    {
        Vector3 vectorthree = (vertex * Mathf.Cos(angle)) + Dotproduct(vertex,axisdir) * 
            axisdir * (1-Mathf.Cos(angle))+Crossproduct(axisdir,vertex) *Mathf.Sin(angle);

        return vectorthree;
    }
    public static bool Intersects(AABB2D boxA, AABB2D boxB)
    {
        return !(boxB.Left() > boxA.Right() || boxB.Right() < boxA.Left()
            || boxB.Top() < boxA.Bottom() || boxB.Bottom() > boxA.Top());        
    }
    public static bool Intersects(AABB3D boxA, AABB3D boxB)
    {
        return !(boxB.Left() > boxA.Right() || boxB.Right() < boxA.Left()
            || boxB.Top() < boxA.Bottom() || boxB.Bottom() > boxA.Top()
            || boxB.Back() > boxA.Front() || boxB.Front() < boxA.Back());
    }

    public static float Sqdistancefrompointtoseg(Vector3 bottom, Vector3 top, Vector3 othercentre)
    {
        Vector3 AB = top-bottom;
        Vector3 AC = othercentre - bottom;
        if (vectormaths.Dotproduct(AC, AB,false) < 0)
        {
            return vectormaths.LengthSq(AC);
        }
        Vector3 BA =  bottom-top;
        Vector3 BC = othercentre - top;
        if (vectormaths.Dotproduct(BA, BC,false) < 0)
        {
            return vectormaths.LengthSq(BC);
        }
        //A + dot(AP,AB) / dot(AB,AB) * AB projection formula AP=AC AB= ABA= bottom
       // bottom += vectormaths.Dotproduct(AC, AB,false) / vectormaths.Dotproduct(AB, AB,false) * AB;
      
        //return vectormaths.LengthSq(bottom);
        return vectormaths.LengthSq(AC) - (vectormaths.Dotproduct(AC, AB,false)) * (vectormaths.Dotproduct(AC, AB,false)) / vectormaths.LengthSq(AB);
        // if dot product is >0 fo r both A and B formula is 
        // SquaredDistance = AC.LengthSq – (AC· AB) * (AC· AB) / AB.LengthSq
        
    }
    public static AABB3D Convertboundingc3dtoAABB3d(Vector3 Centrepoint, float radius)
    {
        Vector3 minpoint = new Vector3(Centrepoint.x - radius, Centrepoint.y - radius, Centrepoint.z - radius);
        Vector3 maxpoint = new Vector3(Centrepoint.x + radius, Centrepoint.y + radius, Centrepoint.z + radius);
        return new AABB3D(minpoint, maxpoint);
    }
}
public class Childm : Matrix4by4
{
    public Childm(Vector4 c1, Vector4 c2, Vector4 c3, Vector4 c4) : base(c1, c2, c3, c4)
    {
     
        matrix = new float[4, 4];
        //matric [c,r]
        /*
         | Ax Bx Cx Dx | | Qx |
         | Ey Fy Gy Hy | | Ry |
         | Iz Jz Kz Lz | | Sz |
         | Mw Nw Ow Pw | | Tw |
         */
        matrix[0, 0] = c1.x; //A
        matrix[1, 0] = c1.y; //E
        matrix[2, 0] = c1.z; //I
        matrix[3, 0] = c1.w; //M

        matrix[0, 1] = c2.x; //B
        matrix[1, 1] = c2.y; //F
        matrix[2, 1] = c2.z; //J
        matrix[3, 1] = c2.w; //N 

        matrix[0, 2] = c3.x; //C
        matrix[1, 2] = c3.y; //G
        matrix[2, 2] = c3.z; //K
        matrix[3, 2] = c3.w; //O

        matrix[0, 3] = c4.x; //D
        matrix[1, 3] = c4.y; //H
        matrix[2, 3] = c4.z; //L
        matrix[3, 3] = c4.w; //P
    }

    public new static Childm Identity
    {
        get
        {
            return new Childm(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));

        }
    }
}

public class Matrix4by4
{
    public float[,] matrix;
    

    public Matrix4by4(Matrix4by4 init)
    {
        matrix = init.matrix;
    }
    
    public Matrix4by4(Vector4 c1, Vector4 c2, Vector4 c3, Vector4 c4)
    {
        matrix = new float[4, 4];
        //matric [c,r]
        /*
         | Ax Bx Cx Dx | | Qx |
         | Ey Fy Gy Hy | | Ry |
         | Iz Jz Kz Lz | | Sz |
         | Mw Nw Ow Pw | | Tw |
         */
        matrix[0, 0] = c1.x; //A
        matrix[1, 0] = c1.y; //E
        matrix[2, 0] = c1.z; //I
        matrix[3, 0] = c1.w; //M

        matrix[0, 1] = c2.x; //B
        matrix[1, 1] = c2.y; //F
        matrix[2, 1] = c2.z; //J
        matrix[3, 1] = c2.w; //N 

        matrix[0, 2] = c3.x; //C
        matrix[1, 2] = c3.y; //G
        matrix[2, 2] = c3.z; //K
        matrix[3, 2] = c3.w; //O

        matrix[0, 3] = c4.x; //D
        matrix[1, 3] = c4.y; //H
        matrix[2, 3] = c4.z; //L
        matrix[3, 3] = c4.w; //P       
    }
    public Matrix4by4(Vector3 c1, Vector3 c2, Vector3 c3, Vector3 c4)
    {
        matrix = new float[4, 4];
        matrix[0, 0] = c1.x;
        matrix[1, 0] = c1.y;
        matrix[2, 0] = c1.z;
        matrix[3, 0] = 0.0f;

        matrix[0, 1] = c2.x;
        matrix[1, 1] = c2.y;
        matrix[2, 1] = c2.z;
        matrix[3, 1] =0.0f;

        matrix[0, 2] = c3.x;
        matrix[1, 2] = c3.y;
        matrix[2, 2] = c3.z;
        matrix[3, 2] = 0.0f;

        matrix[0, 3] = c4.x;
        matrix[1, 3] = c4.y;
        matrix[2, 3] = c4.z;
        matrix[3, 3] = 1.0f;
    }
    public static Vector4 operator *(Matrix4by4 classm, Vector4 vector)
    {
        Vector4 rv = new Vector4(0,0,0,1);
        vector.w = 1;
        /*
        | Ax Bx Cx Dx | | Qx |
        | Ey Fy Gy Hy | | Ry |
        | Iz Jz Kz Lz | | Sz |
        | Mw Nw Ow Pw | | Tw |
        */
        rv.x = classm.matrix[0, 0]*vector.x + classm.matrix[0,1] * vector.y + classm.matrix[0, 2] * vector.z + classm.matrix[0,3]* vector.w;
        rv.y = classm.matrix[1, 0] * vector.x + classm.matrix[1, 1] * vector.y + classm.matrix[1, 2] * vector.z + classm.matrix[1, 3] * vector.w;
        rv.z = classm.matrix[2, 0] * vector.x + classm.matrix[2, 1] * vector.y + classm.matrix[2, 2] * vector.z + classm.matrix[2, 3] * vector.w;
        rv.w = classm.matrix[3, 0] * vector.x + classm.matrix[3, 1] * vector.y + classm.matrix[3, 2] * vector.z + classm.matrix[3, 3] * vector.w;
       

        return rv;
    }
    public static Matrix4by4 operator *(Matrix4by4 matrix1, Matrix4by4 matrix2)
    {
        /*
         | Ax Bx Cx Dx | | Qx |
         | Ey Fy Gy Hy | | Ry |
         | Iz Jz Kz Lz | | Sz |
         | Mw Nw Ow Pw | | Tw |
         */
        Matrix4by4 newmatrix =Identity;
        int i = 0;
      
        for (i = 0; i <= 3; i++)
        {
            newmatrix.matrix[i, 0] = (matrix1.matrix[i, 0] * matrix2.matrix[0, 0]) +
                                    (matrix1.matrix[i, 1] * matrix2.matrix[1, 0]) +
                                    (matrix1.matrix[i, 2] * matrix2.matrix[2, 0]) +
                                    (matrix1.matrix[i, 3] * matrix2.matrix[3, 0]);         
            newmatrix.matrix[i, 1] = (matrix1.matrix[i, 0] * matrix2.matrix[0, 1]) +
                                    (matrix1.matrix[i, 1] * matrix2.matrix[1, 1]) +
                                    (matrix1.matrix[i, 2] * matrix2.matrix[2, 1]) +
                                    (matrix1.matrix[i, 3] * matrix2.matrix[3, 1]);          
            newmatrix.matrix[i, 2] = (matrix1.matrix[i, 0] * matrix2.matrix[0, 2]) +
                                    (matrix1.matrix[i, 1] * matrix2.matrix[1, 2]) +
                                    (matrix1.matrix[i, 2] * matrix2.matrix[2, 2]) +
                                    (matrix1.matrix[i, 3] * matrix2.matrix[3, 2]);          
            newmatrix.matrix[i, 3] = (matrix1.matrix[i, 0] * matrix2.matrix[0, 3]) +
                                    (matrix1.matrix[i, 1] * matrix2.matrix[1, 3]) +
                                    (matrix1.matrix[i, 2] * matrix2.matrix[2, 3]) +
                                    (matrix1.matrix[i, 3] * matrix2.matrix[3, 3]);
           

        }
     
        return newmatrix;
    }
    public static Matrix4by4 Identity
    {
        get
            {
            return new Matrix4by4(new Vector4(1,0,0,0),new Vector4(0,1,0,0),new Vector4(0,0,1,0),new Vector4(0,0,0,1));

            }
    }
    public Vector4 GetRow(int row)
    {
        Vector4 rv = new Vector4(0, 0, 0, 1);        
        rv.x=matrix[row,0];
        rv.y = matrix[row,1];
        rv.z = matrix[row,2];
        rv.w = matrix[row,3];
        
        return rv;
    }
    public Vector4 GetCol(int Col)
    {
        Vector4 rv = new Vector4(0, 0, 0, 1);
        rv.x = matrix[0,Col];
        rv.y = matrix[1,Col];
        rv.z = matrix[2,Col];
        rv.w = matrix[3,Col];

        return rv;
    }
    public void SetRow(int Row, Vector4 vector)
    {

        matrix[Row, 0] = vector.x;
        matrix[Row, 1] = vector.y;
        matrix[Row, 2] = vector.z;
        matrix[Row, 3] = vector.w;


    }
    public void SetCol(int Col,Vector4 vector)
    {

      matrix[0, Col] =vector.x;
      matrix[1, Col] =vector.y;
      matrix[2, Col] =vector.z;
      matrix[3, Col] =vector.w;

     
    }
    public void SetPosition(Vector3 pos)
    {
        SetCol(3, new Vector4(pos.x, pos.y, pos.z, 1));        
    }
    public void Translate(Vector3 pos)
    {
        SetCol(3, new Vector4(matrix[0,3]+pos.x, matrix[1, 3] + pos.y, matrix[2, 3] + pos.z, 1));
    }
    public Matrix4by4 getrotation(Vector3 Eulerangles)
    {
        Matrix4by4 pitchm = Identity;
        Matrix4by4 yawm = Identity;
        Matrix4by4 rollm = Identity;
        pitchm.matrix[1, 1] = Mathf.Cos(Eulerangles.x);
        pitchm.matrix[1, 2] = -Mathf.Sin(Eulerangles.x);
        pitchm.matrix[2, 1] = Mathf.Sin(Eulerangles.x);
        pitchm.matrix[2, 2] = Mathf.Cos(Eulerangles.x);
        yawm.matrix[0,0]= Mathf.Cos(Eulerangles.y);
        yawm.matrix[0,2]= Mathf.Sin(Eulerangles.y);
        yawm.matrix[2, 0] = -Mathf.Sin(Eulerangles.y);
        yawm.matrix[2, 2] = Mathf.Cos(Eulerangles.y);
        rollm.matrix[0, 0] = Mathf.Cos(Eulerangles.z);
        rollm.matrix[0, 1] = -Mathf.Sin(Eulerangles.z);
        rollm.matrix[1, 0] = Mathf.Sin(Eulerangles.z);
        rollm.matrix[1, 1] = Mathf.Cos(Eulerangles.z);
        Matrix4by4 rotation = Identity;
        rotation = yawm * (pitchm * rollm);
        return rotation;
    }
    public Matrix4by4 rotate(Vector3 Eulerangles)
    {
        Matrix4by4 pitchm = Identity;
        Matrix4by4 yawm = Identity;
        Matrix4by4 rollm = Identity;
        pitchm.matrix[1, 1] = Mathf.Cos(Eulerangles.x);
        pitchm.matrix[1, 2] = -Mathf.Sin(Eulerangles.x);
        pitchm.matrix[2, 1] = Mathf.Sin(Eulerangles.x);
        pitchm.matrix[2, 2] = Mathf.Cos(Eulerangles.x);
        yawm.matrix[0, 0] = Mathf.Cos(Eulerangles.y);
        yawm.matrix[0, 2] = Mathf.Sin(Eulerangles.y);
        yawm.matrix[2, 0] = -Mathf.Sin(Eulerangles.y);
        yawm.matrix[2, 2] = Mathf.Cos(Eulerangles.y);
        rollm.matrix[0, 0] = Mathf.Cos(Eulerangles.z);
        rollm.matrix[0, 1] = -Mathf.Sin(Eulerangles.z);
        rollm.matrix[1, 0] = Mathf.Sin(Eulerangles.z);
        rollm.matrix[1, 1] = Mathf.Cos(Eulerangles.z);
        Matrix4by4 rotation = Identity;
        rotation = yawm * (pitchm * rollm);
        return rotation;
    }
    public Matrix4by4 InverseTranslations()
    {
        /*
         | Ax Bx Cx Dx | | Qx |
         | Ey Fy Gy Hy | | Ry |
         | Iz Jz Kz Lz | | Sz |
         | Mw Nw Ow Pw | | Tw |
         */
   
        Matrix4by4 newmatrix = Identity;        
        newmatrix.matrix[0, 3] = -matrix[0,3];
        newmatrix.matrix[1, 3] = -matrix[1, 3];
        newmatrix.matrix[2, 3] = -matrix[2, 3];
        //newmatrix.matrix[3, 3] = -matrix[3, 3];
        return newmatrix;
    }
    public Matrix4by4 InverseTranslationsm()
    {
        /*
         | Ax Bx Cx Dx | | Qx |
         | Ey Fy Gy Hy | | Ry |
         | Iz Jz Kz Lz | | Sz |
         | Mw Nw Ow Pw | | Tw |
         */

        Matrix4by4 newmatrix = Identity;
        newmatrix.matrix = matrix;
        newmatrix.matrix[0, 3] = -matrix[0, 3];
        newmatrix.matrix[1, 3] = -matrix[1, 3];
        newmatrix.matrix[2, 3] = -matrix[2, 3];
        //newmatrix.matrix[3, 3] = -matrix[3, 3];
        return newmatrix;
    }
    public Matrix4by4 InverseRotations()
    {
        /*
         | Ax Bx Cx Dx | | Qx |
         | Ey Fy Gy Hy | | Ry |
         | Iz Jz Kz Lz | | Sz |
         | Mw Nw Ow Pw | | Tw |
         */
        return new Matrix4by4(GetRow(0),GetRow(1),GetRow(2),GetRow(3));
    }
    public Matrix4by4 InverseScale()
    {

        Matrix4by4 rv = Identity;
        rv.matrix[0, 0] = 1.0f / matrix[0, 0];
        rv.matrix[1, 1] = 1.0f / matrix[1, 1];
        rv.matrix[2, 2] = 1.0f / matrix[2, 2];
        return rv;
    }
    public Matrix4by4 InverseTR()
    {
        Matrix4by4 returnval = Matrix4by4.Identity;
        for (int a = 0; a < 3; a++)
            {
                for (int b=0; b<3;b++)
                {
                    returnval.matrix[a, b] = matrix[b, a];
                }
            }
        returnval.SetCol(3, -(returnval * GetCol(3)));
        return returnval;
    }
    public Matrix4by4 InverseM()
    {
        Matrix4by4 invt = InverseTranslations();
        Matrix4by4 invr = InverseRotations();
        Matrix4by4 invs = InverseScale();
        return (invs * (invr * invt));
    }
    public Quarty BecomeQuarty()
    {
        /*
        qw= √(1 + m[00] + m11 + m22) /2
        qx = (m21 - m12)/( 4 *qw)
        qy = (m02 - m20)/( 4 *qw)
        qz = (m10 - m01)/( 4 *qw)    
        */
        Quarty returnval = new Quarty();
        returnval.w = Mathf.Sqrt(1.0f + matrix[0,0]+matrix[1,1]+matrix[2,2])/ 2.0f;
        float w4 = (4.0f * returnval.w);
        returnval.vector.x = (matrix[2, 1] - matrix[1, 2]) / w4;
        returnval.vector.y = (matrix[0, 2] - matrix[2, 0]) / w4;
        returnval.vector.z = (matrix[1, 0] - matrix[0, 1]) / w4;
        return returnval;
    }
}
public class Quarty
{
    public  float w = 0;
    public  Vector3 vector;
    public Matrix4by4 Becomematrix() //convert quartonion to a matrix
    {
        Matrix4by4 temp = Matrix4by4.Identity;
        temp.matrix[0, 0] = 1 - (2 * (vector.y*vector.y))- (2 * (vector.z * vector.z));
        temp.matrix[0, 1] = (2 * (vector.x * vector.y)) - (2 * (vector.z * w));
        temp.matrix[0, 2] = (2 * (vector.x * vector.z)) + (2 * (vector.y * w));
        temp.matrix[1, 0] = (2 * (vector.x * vector.y)) + (2 * (vector.z * w));
        temp.matrix[1, 1] = 1 - (2 * (vector.x * vector.x)) - (2 * (vector.z * vector.z));
        temp.matrix[1, 2] = (2 * (vector.y * vector.z)) - (2 * (vector.x * w));
        temp.matrix[2, 0] = (2 * (vector.x * vector.z)) - (2 * (vector.y * w));
        temp.matrix[2, 1] = (2 * (vector.y * vector.z)) + (2 * (vector.x * w));
        temp.matrix[2, 2] = 1 - (2 * (vector.x * vector.x)) - (2 * (vector.y * vector.y));
        return temp;
    }
    public Quarty()
    {
        w = 0;
        vector.x = 0;
        vector.y = 0;
        vector.z = 0;
    }
    public Quarty(float angle, Vector3 axis)
    {
        float half = angle / 2;
        w = Mathf.Cos(half);
        vector.x = axis.x * Mathf.Sin(half);
        vector.y = axis.y * Mathf.Sin(half);
        vector.z = axis.z * Mathf.Sin(half);
    }
    public Quarty(Vector3 axis)
    {       
        w = 0;
        vector.x = axis.x;
        vector.y = axis.y;
        vector.z = axis.z;
    }
    public static Quarty operator *(Quarty qa, Quarty qb)
    {
        Quarty tempQ = new Quarty
        {
            w = (((qa.w * qb.w) - vectormaths.Dotproduct(qa.vector, qa.vector))),
            vector = (qb.w * qa.vector) + (qa.w * qb.vector) + (vectormaths.Crossproduct(qa.vector, qb.vector))
        };
        return tempQ;
    }
    public Vector3 GetAxis()
    {
        return vector;
    }
    public void SetAxis(Vector3 newaxis)
    {
        this.vector = newaxis;
    }
    public Quarty InverseQuarty()
    {
        Quarty InverseQ= new Quarty();
        InverseQ.w = this.w;
        //keep the angle the same
        InverseQ.SetAxis(-GetAxis());
        //return the inverse of the axis
        return InverseQ;
    }
    public Vector4 GetAxisAngle()
    {
        Vector4 newv = new Vector4();
        float halfangle = Mathf.Acos(this.w);
        newv.w = halfangle * 2;
        //inversed the angle
        newv.x = this.vector.x / Mathf.Sin(halfangle);
        newv.y = this.vector.y / Mathf.Sin(halfangle);
        newv.z = this.vector.z / Mathf.Sin(halfangle);
        //inverse x,y,z
        return newv;
    }
    public static Quarty SLERP(Quarty A, Quarty B, float amount)
    {
        amount = Mathf.Clamp(amount, 0.0f, 1.0f);
        Quarty thirdQ = B * A.InverseQuarty();
        Vector4 Axisangle = thirdQ.GetAxisAngle();
        Quarty thirdQamount = new Quarty(Axisangle.w * amount, new Vector3(Axisangle.x,Axisangle.y,Axisangle.z));

        return thirdQamount * A;
    }
}
public class BoundingCircle2D
{
    public Vector2 Centrepoint; 
    public float radius;
    public BoundingCircle2D(Vector2 cpoint, float r)
    {
        this.Centrepoint = cpoint;
        this.radius = r;
    }    
    public bool Intersectiontest(BoundingCircle2D other)
    {
        
        Vector2 directiontovec = other.Centrepoint;
        float combinedsq = (other.radius + radius);
        combinedsq *= combinedsq;
        return vectormaths.LengthSq(directiontovec)<=combinedsq;
    }   
}
public class BoundingCircle3D
{
    public Vector3 Centrepoint;
    public float radius;
    public BoundingCircle3D(Vector3 cpoint, float r)
    {
        this.Centrepoint = cpoint;
        this.radius = r;        
    }    
    public bool Intersectiontest(BoundingCircle3D other) //sphere to sphere
    {

        Vector3 directiontovec = other.Centrepoint- Centrepoint;
        float combinedsq = (other.radius + radius);
        combinedsq *= combinedsq;
        return vectormaths.LengthSq(directiontovec) <= combinedsq;
         
    }
    
    public bool IntersectAABB(AABB3D other)
    {
        AABB3D circlebox =vectormaths.Convertboundingc3dtoAABB3d(Centrepoint,radius);
        var result= vectormaths.Intersects(circlebox, other);
        return result;
    }
    public bool Intersectiontest(BoundingCapsule other) //sphere to capsule version
    {
        return other.Intersectscircle(this);
    }
}
public class BoundingCapsule
{
    public Vector3 Bottompoint;
    public Vector3 Toppoint;
    public float radius;
    public BoundingCapsule(Vector3 bottom, Vector3 top, float r)
    {
        this.Bottompoint = bottom;
        this.Toppoint = top;
        this.radius = r;
    }
    public bool Intersectscircle(BoundingCircle3D other)
    {
        float combinedradiussq = (radius + other.radius) * (radius + other.radius);
        return vectormaths.Sqdistancefrompointtoseg(Bottompoint, Toppoint, other.Centrepoint) <= combinedradiussq;
    }
    public Vector3 Projectpointonline(BoundingCircle3D other) //used to get centrepoint of sphere in a capsule
    {
        
        Vector3 bottom = Bottompoint;
        Vector3 top = Toppoint;
        Vector3 othercentre = other.Centrepoint;
        Vector3 AB = top - bottom;
        Vector3 AC = othercentre - bottom;
        Vector3 BA = bottom - top;
        Vector3 BC = othercentre - top;
        
        //A + dot(AP,AB) / dot(AB,AB) * AB projection formula AP=AC AB= ABA= bottom
        bottom += vectormaths.Dotproduct(AC, AB, false) / vectormaths.Dotproduct(AB, AB, false) * AB;
        return bottom;
    }
}
public class AABB2D
{
    Vector2 min;
    Vector2 max;    
    public AABB2D(Vector2 minimum, Vector2 maximum)
    {
        min = minimum;
        max = maximum;
    }
    public float Top()
    {
        return max.y;
    }
    public float Bottom()
    {
        return min.y;
    }
    public float Left()
    {
        return min.x;
    }
    public float Right()
    {
        return max.x;
    }
}
public class AABB3D
{
    Vector3 min;
    Vector3 max;
    public AABB3D(Vector3 minimum, Vector3 maximum)
    {
        min = minimum;
        max = maximum;
    }
   public BoundingCircle3D Convertoboundingcircle3d()
    {
        Vector3 distance = max - min;
        distance /= 2;
        Vector3 centrepoint = min + distance;
        float radius = max.x-distance.x;
        return new BoundingCircle3D(centrepoint, radius);
    }
    public float Top()
    {
        return max.y;
    }
    public float Bottom()
    {
        return min.y;
    }
    public float Left()
    {
        return min.x;
    }
    public float Right()
    {
        return max.x;
    }
    public float Front()
    {
        return max.z;
    }
    public float Back()
    {
        return min.z;
    }
    
}
