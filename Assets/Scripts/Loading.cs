using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class Loading : MonoBehaviour
{

    public TextAsset file;
    public Material mat;
    public Vector3 pointPositif;
    public Vector3 pointNegatif;
    public bool simplified;
    public int nbCubes;

    public void UpdateBoxDimensions(Vector3 coord)
    {



            if (coord.x > pointPositif.x)
                pointPositif.x = coord.x;
            if (coord.y > pointPositif.y)
                pointPositif.y = coord.y;
            if (coord.z > pointPositif.z)
                pointPositif.z = coord.z;
            if (coord.x < pointNegatif.x)
                pointNegatif.x = coord.x;
            if (coord.y < pointNegatif.y)
                pointNegatif.y = coord.y;
            if (coord.z < pointNegatif.z)
                pointNegatif.z = coord.z;

      




    }


    

    public float getBiggestArete()
    {

        float dist = Abs((pointPositif.x - pointNega  tif.x));

        if (dist < Abs((pointPositif.y - pointNegatif.y)))
            dist = Abs((pointPositif.y- pointNegatif.y));
        if (dist < Abs((pointPositif.z- pointNegatif.z)))
            dist = Abs((pointPositif.z- pointNegatif.z));

        return dist;


    }

    private void Start()
    {
        Vector3 pointPositif = new Vector3(-int.MaxValue, -int.MaxValue, -int.MaxValue);
        Vector3 pointNegatif = new Vector3(int.MaxValue, int.MaxValue, int.MaxValue);

        string content = file.text;
        string[] splitContent = content.Split("\n");

        string[] infos = splitContent[1].Split(" ");
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();


        int[] triangles = new int[int.Parse(infos[1]) * 3];

        int compteur = 0;
        for (int i = 2; i < int.Parse(infos[0])+2; ++i)
        {
            splitContent[i] = splitContent[i].Replace(".", ",");
            string[] coords = splitContent[i].Split(" ");
            float x = float.Parse(coords[0]);
            float y = float.Parse(coords[1]);
            float z = float.Parse(coords[2]);
            vertices[compteur] = new Vector3(x, y, z);


            UpdateBoxDimensions(vertices[compteur]);
            compteur++;
        }




        float sizeArete = getBiggestArete();



        for(int index = 0; index< compteur; ++index)
        {
            vertices[index] = vertices[index] / sizeArete;
        }
        //simplification 
        for (int index = 0; index < compteur - 2; index += 3)
        {
            vertices[index + 1] = vertices[index];
            vertices[index + 2] = vertices[index];
        }


        int initialI = int.Parse(infos[0]) + 2;
        compteur = 0;
        // index * distace cube (longueur boite) / nbCubes
        Vector3 pointPositifCube = new Vector3.zeros;
        Vector3 pointNegatifCube = new Vector3.zeros;



        for (int indexZ = -nbCubes/2; indexZ <= nbCubes/2; indexZ++)
        {
            for (int indexY = -nbCubes / 2; indexY <= nbCubes / 2; indexY++)
            {
                for (int indexX = -nbCubes / 2 ; indexX <= nbCubes / 2; indexX++)
                {

                    pointNegatifCube = (pointNegatif.x + (indexX * (sizeArete / nbCubes)), pointNegatif.y + (indexY * (sizeArete / nbCubes)), pointNegatif.z + (indexZ * (sizeArete / nbCubes)));
                    pointPositifCube = (pointPositif.x + ((indexX + 1) * (sizeArete / nbCubes)), pointPositif.y + ((indexY + 1) * (sizeArete / nbCubes)), pointPositif.z + ((indexZ + 1) * (sizeArete / nbCubes)));

                    Vector3 firstVertice = new Vector3.zeros;
                    foreach (int index = 0; index < vertices.Lenght ; index++)
                    {
                        
                        if((vertices[index].x <= pointPositifCube.x && vertices[index].y <= pointPositifCube.y && vertices[index].z <= pointPositifCube.z) && (vertices[index].x >= pointNegatifCube.x && vertices[index].y >= pointNegatifCube.y && vertices[index].z >= pointNegatifCube.z))
                        {
                if (firstVertice == Vector.zeros)
                    firstVertice = vertices[index];
                else
                {
                    vertices[index] = firstVertice;
                }



                        }
                    }
                }
            }
        }
        for (int i = initialI; i < initialI + int.Parse(infos[1]);i++)
            {

           



                string[] coords = splitContent[i].Split(" ");

                triangles[compteur] = int.Parse(coords[1]);
                triangles[compteur + 1] = int.Parse(coords[2]);
                triangles[compteur + 2] = int.Parse(coords[3]);
            
                compteur += 3;

            }
        
        Mesh msh = new Mesh();

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;
        
    }


}