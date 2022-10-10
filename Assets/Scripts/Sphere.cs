using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sphere : MonoBehaviour
{
    Vector3[] vertices;

    public Material mat;
    void Start()
    {
        DrawSphere(3, 100, 10, new(6f, 6f, 6f));
    }


    public void DrawSphere(float rayon, int parallele, int meridian, Vector3 centre)
    {
        if (parallele < 2 || meridian < 3)
        {
            return;
        }

        Vector3 northPole = new Vector3(centre.x, centre.y, centre.z + rayon);
        Vector3 southPole = new Vector3(centre.x, centre.y, centre.z - rayon);

        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        int[] triangles = new int[(meridian * (parallele - 1) + meridian) * 6];
        vertices = new Vector3[meridian * (parallele - 1) + 2];


        for (int indexParallele = 1; indexParallele < parallele; ++indexParallele)
        {
            double Fi = (double)(Math.PI * indexParallele) / (parallele);
            for (int indexMeridian = 0; indexMeridian < meridian; ++indexMeridian)
            {
                double tetaMerdidian = (double)(2f * Math.PI * indexMeridian) / (meridian);

                vertices[indexMeridian * (parallele - 1) + indexParallele - 1] = new Vector3((float)(rayon * Math.Sin(Fi) * Math.Cos(tetaMerdidian) + centre.x), (float)(rayon * Math.Sin(tetaMerdidian) * Math.Sin(Fi) + centre.y), (float)(rayon * Math.Cos(Fi) + centre.z));
            }

        }
        vertices[meridian * (parallele - 1) + 1] = northPole;
        vertices[meridian * (parallele - 1)] = southPole;

        int cpt = 0;
        //plans
        for (int indexParalleles = 1; indexParalleles < parallele - 1; ++indexParalleles)
        {
            for (int indexMeridian = 0; indexMeridian < meridian - 1; ++indexMeridian)
            {
                triangles[cpt + 0] = indexMeridian * (parallele - 1) + indexParalleles - 1;
                triangles[cpt + 1] = indexMeridian * (parallele - 1) + indexParalleles;
                triangles[cpt + 2] = (indexMeridian + 1) * (parallele - 1) + indexParalleles - 1;
                // triangle 2 
                triangles[cpt + 4] = (indexMeridian + 1) * (parallele - 1) + indexParalleles;
                triangles[cpt + 3] = indexMeridian * (parallele - 1) + indexParalleles;
                triangles[cpt + 5] = (indexMeridian + 1) * (parallele - 1) + indexParalleles - 1;


                cpt += 6;
            }
            triangles[cpt + 0] = (meridian - 1) * (parallele - 1) + indexParalleles - 1;
            triangles[cpt + 1] = (meridian - 1) * (parallele - 1) + indexParalleles;
            triangles[cpt + 2] = indexParalleles - 1;

            triangles[cpt + 3] = (meridian - 1) * (parallele - 1) + indexParalleles;
            triangles[cpt + 4] = indexParalleles;
            triangles[cpt + 5] = indexParalleles - 1;

            cpt += 6;

        }



        for (int indexMeridian = 0; indexMeridian < meridian - 1; ++indexMeridian)
        {
            triangles[cpt + 0] = meridian * (parallele - 1) + 1;//North pole
            triangles[cpt + 1] = (indexMeridian) * (parallele - 1);
            triangles[cpt + 2] = (indexMeridian + 1) * (parallele - 1);


            cpt += 3;


        }
        triangles[cpt + 0] = meridian * (parallele - 1) + 1;//North pole
        triangles[cpt + 2] = (0) * (parallele - 1);
        triangles[cpt + 1] = (meridian - 1) * (parallele - 1);
        cpt += 3;

        for (int indexMeridian = 0; indexMeridian < meridian - 1; ++indexMeridian)
        {

            // indexMeridian * (parallele-1) + indexParallele -1


            triangles[cpt + 0] = meridian * (parallele - 1);//south pole
            triangles[cpt + 1] = (indexMeridian + 1) * (parallele - 1) + parallele - 3;
            triangles[cpt + 2] = indexMeridian * (parallele - 1) + parallele - 3;


            cpt += 3;


        }
        triangles[cpt + 0] = meridian * (parallele - 1);//south pole
        triangles[cpt + 1] = (parallele - 1) + parallele - 3;
        triangles[cpt + 2] = (meridian - 1) * (parallele - 1) + parallele - 3;



        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;


    }
}


