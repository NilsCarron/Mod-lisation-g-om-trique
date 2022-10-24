using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static System.Math;


public class Loading : MonoBehaviour
{

    public TextAsset file;
    public Material mat;
    public Vector3 pointPositif;
    public Vector3 pointNegatif;
    public int nbCubes;
    

    private List<Vector3> normales = new List<Vector3>();




    private void Start()
    {
        
        string content = file.text;
        string[] splitContent = content.Split("\n");

        string[] infos = splitContent[1].Split(" ");
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        Vector3[] vertices = new Vector3[int.Parse(infos[0])];
        int[] triangles = new int[int.Parse(infos[1]) * 3];

        int compteur = 0;
        for (int i = 2; i < int.Parse(infos[0]) + 2; ++i)
        {
            splitContent[i] = splitContent[i].Replace(".", ",");
            string[] coords = splitContent[i].Split(" ");
            float x = float.Parse(coords[0]);
            float y = float.Parse(coords[1]);
            float z = float.Parse(coords[2]);
            vertices[compteur] = new Vector3(x, y, z);

            if (i == 2)
            {
                pointPositif = vertices[compteur];
                pointNegatif = vertices[compteur];
            }
            else
            {
                if (x < pointNegatif.x)
                    pointNegatif.x = x;
                if (y < pointNegatif.y)
                    pointNegatif.y = y;
                if (z < pointNegatif.z)
                    pointNegatif.z = z;
                if( x > pointPositif.x)
                    pointPositif.x = x;
                if( y > pointPositif.y)
                    pointPositif.y = y;
                if( z > pointPositif.z)
                    pointPositif.z = z;
            }
            
            
            compteur++;
        }
        //turn the box into a cube





        float distX = pointPositif.x - pointNegatif.x;
        float distY = pointPositif.y - pointNegatif.y;
        float distZ = pointPositif.z - pointNegatif.z;

        
        float sizeArete = distX;

        if (sizeArete < distY)
            sizeArete = distY;
        if (sizeArete < distZ)
            sizeArete = distZ;

        if (sizeArete > distX)
        {
            pointPositif.x = pointNegatif.x + (sizeArete );
        }
        if (sizeArete > distY)
        {
            pointPositif.y = pointNegatif.y + (sizeArete);

        }if (sizeArete > distZ)
        {
            pointPositif.z = pointNegatif.z + (sizeArete );

        }
       
        
        Vector3 pointPositifCube = Vector3.zero;
        Vector3 pointNegatifCube = Vector3.zero;


        
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



        int initialI = int.Parse(infos[0]) + 2;
        compteur = 0;
        for (int i = initialI; i < initialI + int.Parse(infos[1]); i++)
        {
            string[] coords = splitContent[i].Split(" ");
            triangles[compteur] = int.Parse(coords[1]);
            triangles[compteur + 1] = int.Parse(coords[2]);
            triangles[compteur + 2] = int.Parse(coords[3]);
            
            //Calcul de la normale
            Vector3 centre = Vector3.zero;
                
            centre.x = (vertices[int.Parse(coords[1])].x + vertices[int.Parse(coords[2])].x + vertices[int.Parse(coords[3])].x) / 3;
            centre.y = (vertices[int.Parse(coords[1])].y + vertices[int.Parse(coords[2])].y + vertices[int.Parse(coords[3])].y) / 3;
            centre.z = (vertices[int.Parse(coords[1])].z + vertices[int.Parse(coords[2])].z + vertices[int.Parse(coords[3])].z) / 3;

            Vector3 normale = Vector3.zero;

            Vector3 S1S2 = vertices[int.Parse(coords[2])] - vertices[int.Parse(coords[1])];
            Vector3 S1S3 = vertices[int.Parse(coords[3])] - vertices[int.Parse(coords[1])];

            normale = Vector3.Cross(S1S2, S1S3);

            normale = centre + normale;

            normales.Add(centre);
            normales.Add(normale);
            
            
            compteur += 3;
        }

        
        
        Mesh msh = new Mesh();

        msh.vertices = vertices;
        msh.triangles = triangles;
        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;
        
        Save();

        
    }
    public void Save()
    {       

        string path = Application.persistentDataPath + "/Simplified.off.txt";
        Debug.Log(path);

        if (File.Exists(path))
        {
            File.WriteAllText(path, "");
        }

        Vector3[] vertices = gameObject.GetComponent<MeshFilter>().mesh.vertices;
        int[] triangles = gameObject.GetComponent<MeshFilter>().mesh.triangles;

        StreamWriter writer = new StreamWriter(path, true);

        writer.WriteLine("OFF");

        string infos = vertices.Length + " " + triangles.Length / 3 + " " + 0;
        writer.WriteLine(infos);
        
        for(int i = 0; i < vertices.Length; i++)
        {
            string coords = "";
            coords += vertices[i].x + " ";
            coords += vertices[i].y + " ";
            coords += vertices[i].z;
            writer.WriteLine(coords);
        }

        for(int i = 0; i < triangles.Length; i+=3)
        {
            string coords = "";
            coords += "3 ";
            coords += triangles[i] + " ";
            coords += triangles[i+1] + " ";
            coords += triangles[i+2];
            writer.WriteLine(coords);
        }
        Debug.Log("fdsfdsf");

        writer.Close();
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (gameObject.GetComponent<MeshFilter>() != null)
        {
            if (normales != null)
            {
                for (int i = 0; i < normales.Count; i+=2)
                {
                    Gizmos.DrawLine(normales[i], normales[i + 1]);
                }
            }
        }

    }
}
