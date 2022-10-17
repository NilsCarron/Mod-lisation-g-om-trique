using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static System.Math;
public class Simplification : MonoBehaviour
{
    [SerializeField] private Material mat;

    // Start is called before the first frame update
    void Start()
    {


<<<<<<< HEAD
       Sphere orb1 = new Sphere( 3f, new Vector3(0f, 0f, 0f));
=======
       Sphere orb1 = new Sphere(  3f, new Vector3(0f, 0f, 0f));
>>>>>>> cbb1553346af07d307ce9b2ed0315d7371cf0f98
       Sphere orb2 = new Sphere( 6f, new Vector3(15f, 10f, 10f));
       Sphere orb3 = new Sphere( 5f, new Vector3(20f, 25f, 35f));

        List<Sphere> listOfSphere = new List<Sphere>();
        listOfSphere.Add(orb1);
        listOfSphere.Add(orb2);
        listOfSphere.Add(orb3);

        DrawSimplifiedSphere(listOfSphere); 
        
        
        
        
    }

    public struct Sphere
    {
         public float rayon;
         public Vector3 position;

         public Sphere(float rayon, Vector3 position)
         {
             this.position = position;
             this.rayon = rayon;
         }
    }

    public List<Vector3> GetBoxDimensions(List<Sphere> spheres)
    {
        Vector3 pointPositif = new Vector3(-int.MaxValue, -int.MaxValue, -int.MaxValue);
        Vector3 pointNegatif = new Vector3(int.MaxValue, int.MaxValue, int.MaxValue);



        foreach (Sphere orb in spheres)
        {
            if (orb.position.x + orb.rayon > pointPositif.x)
                pointPositif.x = (int)Mathf.Floor(orb.position.x+ orb.rayon) +1;
            if (orb.position.y+ orb.rayon > pointPositif.y)
                pointPositif.y = (int)Mathf.Floor(orb.position.y+ orb.rayon) +1;
            if (orb.position.z+ orb.rayon > pointPositif.z)
                pointPositif.z = (int)Mathf.Floor(orb.position.z+ orb.rayon) +1;
            if (orb.position.x - orb.rayon < pointNegatif.x)
                pointNegatif.x = (int)Mathf.Floor(orb.position.x- orb.rayon) -1;
            if (orb.position.y - orb.rayon< pointNegatif.y)
                pointNegatif.y = (int)Mathf.Floor(orb.position.y- orb.rayon) -1;
            if (orb.position.z - orb.rayon< pointNegatif.z)
                pointNegatif.z = (int)Mathf.Floor(orb.position.z- orb.rayon) -1;
            
        }

        List<Vector3> dimentionsBox = new List<Vector3>();
        dimentionsBox.Add(pointNegatif);
        dimentionsBox.Add(pointPositif);

        Debug.Log(pointNegatif);
        Debug.Log(pointPositif);


        return dimentionsBox;
    }
  
       
    
    public void DrawSimplifiedSphere(List<Sphere> spheres)
    {

        List<Vector3> BoxExtremites = new List<Vector3>();
        BoxExtremites = GetBoxDimensions( spheres);
        
        
        
        
        Vector3 coordonneesCentreBox;
        coordonneesCentreBox = new(0, 0, 0);

      
        
        for (int indexZ = (int)BoxExtremites[0].z - 1; indexZ < (int)BoxExtremites[1].z - 1; indexZ++)
        {
            for (int indexY = (int)BoxExtremites[0].y - 1; indexY < (int)BoxExtremites[1].y - 1; indexY++)
            {
                for (int indexX = (int)BoxExtremites[0].x - 1; indexX < (int)BoxExtremites[1].x - 1; indexX++)
                {
                   
                    foreach (Sphere orb in spheres)
                    {
                        coordonneesCentreBox.x = indexX ;
                        coordonneesCentreBox.y = indexY;
                        coordonneesCentreBox.z = indexZ ;

                        
                        CollisionCube(orb, coordonneesCentreBox);
                    }
                }
            }
        }

    }

    private void CollisionCube(Sphere orb, Vector3 coordonneesCentreBox)
    {
        if (Abs(Vector3.Distance(coordonneesCentreBox, orb.position)) < orb.rayon)
        {
            GenerateCube(coordonneesCentreBox, 0.5f);
        }
        
        
    }

    public void GenerateCube(Vector3 coordonneesBox, float tailleCube) {
    
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



