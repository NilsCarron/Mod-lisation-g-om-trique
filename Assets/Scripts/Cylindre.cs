using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cylindre : MonoBehaviour
{
    Vector3[] vertices;

    public Material mat;
    void Start()
    {
        DrawCylindre(3.6f, 10.5f, 10);
    }





    public void DrawCylindre(float rayon, float height, int meridian)
    {

        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        int[] triangles = new int[meridian * 12];
        vertices = new Vector3[2 * meridian + 2];

        for (int i = 0; i < meridian; ++i)
        {
            double teta = (double)(2 * Math.PI) * i / (meridian);
            vertices[i] = new Vector3(rayon * (float)(Math.Cos(teta)), rayon * (float)(Math.Sin(teta)), height);

        }

        for (int i = 0; i < meridian; ++i)
        {
            double teta = (double)(2 * Math.PI) * i / (meridian);
            vertices[meridian + i] = new Vector3(rayon * (float)Math.Cos(teta), rayon * (float)Math.Sin(teta), 0);

        }
        vertices[2 * meridian] = new Vector3(0, 0, 0);
        vertices[2 * meridian + 1] = new Vector3(0, 0, height);


        int cpt = 0;
        //plans
        for (int index = 0; index < meridian - 1; ++index)
        {

            triangles[cpt] = index;
            triangles[cpt + 2] = index + 1;
            triangles[cpt + 1] = index + meridian;
            // triangle 2 




            triangles[cpt + 3] = index + meridian + 1;
            triangles[cpt + 5] = index + meridian;

            triangles[cpt + 4] = index + 1;


            cpt += 6;
        }
        //dernier plan
        triangles[cpt] = meridian - 1;
        triangles[cpt + 2] = 0;
        triangles[cpt + 1] = meridian - 1 + meridian;




        triangles[cpt + 3] = meridian;
        triangles[cpt + 5] = meridian - 1 + meridian;

        triangles[cpt + 4] = 0;
        cpt += 6;

        for (int index = 0; index < meridian - 1; ++index)//couvercles
        {
            triangles[cpt + 2] = index;
            triangles[cpt] = index + 1;
            triangles[cpt + 1] = 2 * meridian + 1;
            // triangle 2 




            triangles[cpt + 3] = index + meridian + 1;
            triangles[cpt + 5] = 2 * meridian;

            triangles[cpt + 4] = index + meridian;


            cpt += 6;
        }

        //derniers couvercles
        triangles[cpt + 2] = meridian - 1;
        triangles[cpt] = 0;
        triangles[cpt + 1] = 2 * meridian + 1;


        triangles[cpt + 4] = meridian * 2 - 1;
        triangles[cpt + 5] = 2 * meridian;
        triangles[cpt + 3] = meridian;
        cpt += 6;
        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;

    }
}

