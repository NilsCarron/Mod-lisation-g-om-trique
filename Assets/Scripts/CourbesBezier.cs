using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.Mathf;
public class CourbesBezier : MonoBehaviour
{

    public Vector3 p0;
    public Vector3 p1;
    public Vector3[] pointsControle;
    private GameObject SelectCube;
    public List<GameObject> listSpheres;
    public Vector3 v0;
    public Vector3 v1;
    private LineRenderer lr;
    public List<Vector3> listePoints;
    private int isSelecting;
    public void Start()
    {
        lr = GetComponent<LineRenderer>();
        foreach (var points in pointsControle)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = points;
            sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            listSpheres.Add(sphere);
            isSelecting = -1;
            Debug.Log((pointsControle.Length));
        }
        lr.SetVertexCount(listePoints.Count);
        for(int i = 0;i<listePoints.Count;i++){
            lr.SetPosition(i,listePoints[i]);    
        }

        UpdatePointsList();
        lr.positionCount = listePoints.Count;
        for (int indexListePoints = 0; indexListePoints < listePoints.Count; indexListePoints++)

        {
            lr.SetPosition(indexListePoints, listePoints[indexListePoints]);
        }
    }

    private void Update()
    {

        var input = Input.inputString;
        //ignore null input to avoid unnecessary computation
        if (!string.IsNullOrEmpty(input))

            switch (input)
            {
                case "5":
                    
                    

                    break;

                case "0":
                    ChangeSelected(0);
                    break;
                case "1":
                    ChangeSelected(1);
                    break;
                case "2":
                    ChangeSelected(2);
                    break;
                case "3":
                    ChangeSelected(3);
                    break;

            }
        
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        
        
        if (isSelecting > 0 && (x!= 0 || y !=0))
        {
            listSpheres[isSelecting].transform.position += new Vector3(x*0.01f, y *0.01f, 0);
            pointsControle[isSelecting]  += new Vector3(x*0.01f, y *0.01f, 0);
            SelectCube.transform.position  += new Vector3(x*0.01f, y*0.01f, 0);
            x = 0;
            y = 0;
            lr.SetVertexCount(listePoints.Count);
            for(int i = 0;i<listePoints.Count;i++){
                lr.SetPosition(i,listePoints[i]);    
            }

            UpdatePointsList();
            lr.positionCount = listePoints.Count;
            for (int indexListePoints = 0; indexListePoints < listePoints.Count; indexListePoints++)

            {
                lr.SetPosition(indexListePoints, listePoints[indexListePoints]);
            }
        }

    }
    
    


    void ChangeSelected(int index)
        {
            isSelecting = index;

            if(SelectCube == null)
                SelectCube  = GameObject.CreatePrimitive(PrimitiveType.Cube);
            SelectCube.transform.position = pointsControle[index];
            SelectCube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    
        }

    void MoveSelected(float x, float y)
    {
        Vector3 lateralMove = new (pointsControle[isSelecting].x + x, 0,0);
        Vector3 upMove = new (0,pointsControle[isSelecting].y + y,0);
        Vector3 move =  lateralMove + upMove ;




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

    private void UpdatePointsList()
    {


         List<Vector3> temporaryList;
         temporaryList = new List<Vector3>();

        Vector3 PlotPoint;
        for (float t = 0f;  t <= 1.0f; t += 0.01f)
        {
            PlotPoint = GetPoint(t, pointsControle[0], pointsControle[1], pointsControle[2], pointsControle[3]);
            
            temporaryList.Add(PlotPoint);
        }

        listePoints = temporaryList;

    }


    private void OnDrawGizmos()
    {
        /*
        float[,] matrixA = { { 2f, -2f, 1f, 1f }, { -3f, 3f, -2f, -1f }, { 0f, 0f, 1f, 0f }, { 1f, 0f, 0f, 0f } };
        Vector3[] matrixB = { p0, p1, v0, v1 };

        Vector3[] matrixRet = MutliplyMatrix(matrixA, matrixB);
        float u;
        //Vector3 point = MutliplyMatrix(1/1000f, matrixRet);
        
        Vector3 point = new Vector3(0, Bernstein(0, 1000, (float)(1/1000)), 0);

        for (int i = 1; i <= 1000; i++)
        {
            u = (float)i / 1000;
           Vector3 point2 = MutliplyMatrix(u, matrixRet);
            Gizmos.DrawLine(point, point2);

            point = point2;

       Vector3 point2 = new Vector3(u, Bernstein(i, 1000, u), 0);
       Gizmos.DrawLine(point, point2);
       point = point2; */
        
     
        
        // preset your p0,p1,p2,p3

    }
    private Vector3 GetPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float cx = 3 * (p1.x - p0.x);
        float cy = 3 * (p1.y - p0.y);
        float bx = 3 * (p2.x - p1.x) - cx;
        float by = 3 * (p2.y - p1.y) - cy;
        float ax = p3.x - p0.x - cx - bx;
        float ay = p3.y - p0.y - cy - by;
        float Cube = t * t * t;
        float Square = t * t;
        float resX = (ax * Cube) + (bx * Square) + (cx * t) + p0.x;
        float resY = (ay * Cube) + (by * Square) + (cy * t) + p0.y;

        return new Vector3(resX, resY, 0);
    }
    
    static int Factoriel(int n)
    {
        return n > 1 ? n * Factoriel(n - 1) : 1;
    }
    
    float Bernstein(int i, int n, float t)
    {
        return (Factoriel(n) / (Factoriel(i) * (Factoriel(n - i)))) * Mathf.Pow(t, i) * Mathf.Pow(1 - t, n - i);
    }
    
    
    
     
}
