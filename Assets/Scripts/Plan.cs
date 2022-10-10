using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Plan : MonoBehaviour
{
    Vector3[] vertices;

    public Material mat;
    void Start()
    {
        DrawPlan(20, 15);
    }






public void DrawPlan(int planHeight, int planLenght)
    { 
    gameObject.AddComponent<MeshFilter>();
    gameObject.AddComponent<MeshRenderer>();
    vertices = new Vector3[planLenght* planHeight];
    int[] triangles = new int[planLenght * planHeight];

    int pas = 2;
    int cpt = 0;
    for (int indexY = 0; indexY < planHeight; ++indexY)
    {
        for (int indexX = 0; indexX < planLenght; ++indexX)
        {


            vertices[cpt] = new Vector3(pas * indexX, pas * indexY, 0);
            cpt = cpt + 1;

        }
    }



    for (int index = 0; index < planLenght; ++index)
    {

        if (!(((index + 1) % planLenght) == 0) && !(index + planLenght >= cpt))
        {
            triangles[(index) * 6] = index;
            triangles[(index) * 6 + 1] = index + 1;
            triangles[(index) * 6 + 2] = index + planLenght;
            // triangle 2 




            triangles[(index) * 6 + 3] = index + planLenght + 1;
            triangles[(index) * 6 + 4] = index + planLenght;

            triangles[(index) * 6 + 5] = index + 1;
        }




    }






    Mesh msh = new Mesh();
    msh.vertices = vertices;
    msh.triangles = triangles;

    gameObject.GetComponent<MeshFilter>().mesh = msh;
    gameObject.GetComponent<MeshRenderer>().material = mat;


}

}
