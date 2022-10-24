using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;
public class CourbesBezier : MonoBehaviour
{

    public Vector3 p0;
    public Vector3 p1;
    public Vector3 v0;
    public Vector3 v1;


    // Start is called before the first frame update
    void Start()
    {
        
      //  GenerateCourbe(p0, p1, v0, v1);
    }

    void GenerateCourbe(Vector3 p0, Vector3 p1, Vector3 v0, Vector3 v1)
    {

      

    }


    Vector3[] MutliplyMatrix(float[,] matrixA, Vector3[] matrixB){
        Vector3[] matrixRet =  { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero }  ;
        for (int indexMatrixACol = 0; indexMatrixACol < 4; ++indexMatrixACol)
        {
            for (int indexMatrixALin = 0; indexMatrixALin < 4; ++indexMatrixALin)
            {
                matrixRet[indexMatrixACol] += matrixA[indexMatrixACol, indexMatrixALin] * matrixB[indexMatrixALin];
            }
        }


        return matrixRet;
    }

    Vector3 MutliplyMatrix(float u, Vector3[] matrixB)
    {
        float[] matrixA = { Mathf.Pow(u,3), Mathf.Pow(u, 2), u, 0 };
        Vector3 ret = Vector3.zero;
        for (int indexMatrix = 0; indexMatrix < 4; ++indexMatrix)
        {
            ret += matrixA[indexMatrix] * matrixB[indexMatrix];
        }


        return ret;
    }




    private void OnDrawGizmos()
    {
        /*Vector3 p0 = Vector3.zero;
        p0.x = 0;
        p0.y = 0;
        Vector3 p1 = Vector3.zero;
        p1.x = 0;
        p1.y = 2;
        Vector3 v0 = Vector3.zero;
        v0.x = 1;
        v0.y = 1;
        Vector3 v1 = Vector3.zero;
        v1.x = 1;
        v1.y = -1;
        */
        float[,] matrixA = { { 2f, -2f, 1f, 1f }, { -3f, 3f, -2f, -1f }, { 0f, 0f, 1f, 0f }, { 1f, 0f, 0f, 0f } };
        Vector3[] matrixB = { p0, p1, v0, v1 };

        Vector3[] matrixRet = MutliplyMatrix(matrixA, matrixB);
        float u;

        for (int i = 0; i <= 1000; i++)
        {
            u = (float)i / 1000;

            Gizmos.DrawSphere(MutliplyMatrix(u, matrixRet), 0.01f);
        }
    }
     
}
