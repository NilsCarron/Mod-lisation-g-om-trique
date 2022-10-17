using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Simplification : MonoBehaviour
{
    [SerializeField] private Material mat;

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD


       Sphere orb1 = new Sphere( 3f, new Vector3(0f, 0f, 0f));
       Sphere orb2 = new Sphere( 6f, new Vector3(15f, 10f, 10f));
       Sphere orb3 = new Sphere( 5f, new Vector3(20f, 25f, 35f));

        List<Sphere> listOfSphere = new List<Sphere>();
        listOfSphere.Add(orb1);
        listOfSphere.Add(orb2);
        listOfSphere.Add(orb3);

        DrawSimplifiedSphere(listOfSphere); 
        
        
        
        
=======
        Vector3 centre = new(6f, 6f, 6f);
        float rayon = 3f;
        DrawSimplifiedSphere(centre, rayon, mat, 0.2f); 
>>>>>>> parent of cbb1553 (Multiple shpere simplification)
    }

    public void DrawSimplifiedSphere(Vector3 centre, float rayon , Material mat, float arete)
    {
        Vector3 coordonneesBox;
        coordonneesBox = new(0, 0, 0);
        int tailleBox = (int)Mathf.Floor((rayon) / (arete) +1);

        for (int indexZ = -tailleBox - 1; indexZ < tailleBox - 1; indexZ++)
        {
            for (int indexY = -tailleBox - 1; indexY < tailleBox - 1; indexY++)
            {
                for (int indexX = -tailleBox - 1; indexX < tailleBox - 1; indexX++)
                {
                    coordonneesBox = new(indexX* arete + centre.x , indexY* arete + centre.y , indexZ* arete + centre.z );
                    if (Vector3.Distance(coordonneesBox, centre) < rayon){
                        GenerateCube(coordonneesBox, centre, arete, mat);
                    }
                }
            }
        }

    }
    public void GenerateCube(Vector3 coordonneesBox, Vector3 centre, float tailleCube, Material mat) {
    
        {

            DrawCube( coordonneesBox, tailleCube, mat); 
         
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
        int[] triangles = new int[8*6];

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



