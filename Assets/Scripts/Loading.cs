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

        float dist = Abs((pointPositif.x - pointNegatif.x));

        if (dist < Abs((pointPositif.y - pointNegatif.y)))
            dist = Abs((pointPositif.y- pointNegatif.y));
        if (dist < Abs((pointPositif.z- pointNegatif.z)))
            dist = Abs((pointPositif.z- pointNegatif.z));
        Debug.Log(dist);
        return dist;


    }

    private void MakeABox(float size)
    {
        if(Abs((pointPositif.z - pointNegatif.z)) != size)
        {
            pointPositif.z = pointPositif.z + (size - pointNegatif.z);
        }
        if (Abs((pointPositif.x - pointNegatif.x)) != size)
        {
            pointPositif.x = pointPositif.x + (size - pointNegatif.x);
        }
        if (Abs((pointPositif.y - pointNegatif.y)) != size)
        {
            pointPositif.y = pointPositif.y + (size - pointNegatif.y);
        }




    }
    private void Start()
    {
        
        string content = file.text;
        string[] splitContent = content.Split("\n");

        string[] infos = splitContent[1].Split(" ");
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();


        int[] triangles = new int[int.Parse(infos[1]) * 3];
        Vector3[] vertices = new Vector3[int.Parse(infos[0])];
        int compteur = 0;
        for (int i = 2; i < int.Parse(infos[0]) + 2; ++i)
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
        MakeABox(sizeArete);

        for (int index = 0; index < compteur; ++index)
        {
            vertices[index] = vertices[index] / sizeArete;
        }
        int initialI = int.Parse(infos[0]) + 2;
        compteur = 0;
        // index * distace cube (longueur boite) / nbCubes
        Vector3 pointPositifCube = Vector3.zero;
        Vector3 pointNegatifCube = Vector3.zero;


        GenerateCube(pointNegatif, 0.01f);
        GenerateCube(pointPositif, 0.01f);


        for (int indexZ = 0; indexZ <= nbCubes; indexZ++)
        {
            for (int indexY =0; indexY <= nbCubes ; indexY++)
            {
                for (int indexX = 0; indexX <= nbCubes ; indexX++)
                {

                    pointNegatifCube.x = pointNegatif.x + (indexX * (sizeArete / nbCubes));
                    pointNegatifCube.y = pointNegatif.y + (indexY * (sizeArete / nbCubes));
                    pointNegatifCube.z = pointNegatif.z + (indexZ * (sizeArete / nbCubes));

                    pointPositifCube.x = pointNegatif.x + ((indexX + 1) * (sizeArete / nbCubes));
                    pointPositifCube.y = pointNegatif.y + ((indexY + 1) * (sizeArete / nbCubes));
                    pointPositifCube.z = pointNegatif.z + ((indexZ + 1) * (sizeArete / nbCubes));



                    Vector3 firstVertice = Vector3.zero;

                    for (int index = 0; index < vertices.Length; index++)
                    {
                        Debug.Log("qdbg");
                        if ((vertices[index].x <= pointPositifCube.x && vertices[index].y <= pointPositifCube.y && vertices[index].z <= pointPositifCube.z) && (vertices[index].x >= pointNegatifCube.x && vertices[index].y >= pointNegatifCube.y && vertices[index].z >= pointNegatifCube.z))
                        {
                            
                            if (firstVertice == Vector3.zero)
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


        compteur = 0;
        for (int i = initialI; i < initialI + int.Parse(infos[1]);i++)
        {
            string[] coords = splitContent[i].Split(" ");

            if (vertices[int.Parse(coords[1])] != vertices[int.Parse(coords[2])] && vertices[int.Parse(coords[3])] != vertices[int.Parse(coords[1])] && vertices[int.Parse(coords[3])] != vertices[int.Parse(coords[2])]) { 
            triangles[compteur] = int.Parse(coords[1]);
            triangles[compteur+1] = int.Parse(coords[2]);
            triangles[compteur+2] = int.Parse(coords[3]);
            
            compteur += 3;
            }
        }


        Debug.Log("8");
        Mesh msh = new Mesh();
        Debug.Log(vertices[0]);
        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;
        
    }









    public void GenerateCube(Vector3 coordonneesBox, float tailleCube)
    {

        {

            DrawCube(coordonneesBox, tailleCube, mat);

        }
    }
    public void DrawCube(Vector3 coordonneesBox, float TailleBox, Material mat)
    {
        GameObject cube;
        Vector3[] vertices;


        cube = new GameObject("Cube");

        cube.AddComponent<MeshFilter>();
        cube.AddComponent<MeshRenderer>();



        vertices = new Vector3[8];
        int[] triangles = new int[8 * 6];

        vertices[0] = new(coordonneesBox.x - TailleBox, coordonneesBox.y - TailleBox, coordonneesBox.z - TailleBox);
        vertices[1] = new(coordonneesBox.x + TailleBox, coordonneesBox.y - TailleBox, coordonneesBox.z - TailleBox);
        vertices[2] = new(coordonneesBox.x - TailleBox, coordonneesBox.y - TailleBox, coordonneesBox.z + TailleBox);
        vertices[3] = new(coordonneesBox.x + TailleBox, coordonneesBox.y - TailleBox, coordonneesBox.z + TailleBox);
        vertices[4] = new(coordonneesBox.x - TailleBox, coordonneesBox.y + TailleBox, coordonneesBox.z - TailleBox);
        vertices[5] = new(coordonneesBox.x + TailleBox, coordonneesBox.y + TailleBox, coordonneesBox.z - TailleBox);
        vertices[6] = new(coordonneesBox.x - TailleBox, coordonneesBox.y + TailleBox, coordonneesBox.z + TailleBox);
        vertices[7] = new(coordonneesBox.x + TailleBox, coordonneesBox.y + TailleBox, coordonneesBox.z + TailleBox);


        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;

        triangles[6] = 4;
        triangles[7] = 6;
        triangles[8] = 5;
        triangles[9] = 6;
        triangles[10] = 7;
        triangles[11] = 5;

        triangles[12] = 4;
        triangles[13] = 0;
        triangles[14] = 6;
        triangles[15] = 0;
        triangles[16] = 2;
        triangles[17] = 6;


        triangles[18] = 1;
        triangles[19] = 5;
        triangles[20] = 7;
        triangles[21] = 7;
        triangles[22] = 3;
        triangles[23] = 1;

        triangles[24] = 4;
        triangles[25] = 5;
        triangles[26] = 0;
        triangles[27] = 5;
        triangles[28] = 1;
        triangles[29] = 0;

        triangles[30] = 2;
        triangles[31] = 7;
        triangles[32] = 6;
        triangles[33] = 3;
        triangles[34] = 7;
        triangles[35] = 2;

        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = triangles;

        cube.GetComponent<MeshFilter>().mesh = msh;
        cube.GetComponent<MeshRenderer>().material = mat;


    }

}