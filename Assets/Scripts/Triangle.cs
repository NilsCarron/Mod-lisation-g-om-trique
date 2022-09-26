using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Triangle : MonoBehaviour
{
    Vector3[] vertices;

    public Material mat;
    void Start()
    {
        Sphere(10, 10, 10, new (0f,0f,0f));
    }


    public void Sphere(float rayon, int parallele, int meridian, Vector3 centre) { 
        if (parallele < 2 || meridian < 3)
        {
            return;
        }

        Vector3 northPole =  new Vector3(centre.x, centre.y, centre.z + rayon);
        Vector3 southPole = new Vector3(centre.x, centre.y, centre.z - rayon);

        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        int[] triangles = new int[meridian * (parallele )*6];
        vertices = new Vector3[meridian * (parallele + 1)];


        for (int indexParallele = 1; indexParallele < parallele; ++indexParallele)
        {
            double Fi = (Math.PI * indexParallele) / (parallele);
            for(int indexMeridian = 0; indexMeridian < meridian; ++indexMeridian)
            {
                double tetaMerdidian = (2 * Math.PI * indexMeridian) / (meridian);

                vertices[indexMeridian* parallele + indexParallele] = new Vector3((float)(rayon * Math.Sin(Fi) * Math.Cos(tetaMerdidian)), (float)(rayon * Math.Sin(tetaMerdidian) * Math.Sin(Fi)), (float)(rayon * Math.Cos(Fi)));

            }

        }
        
        }

    private void OnDrawGizmos()
    {
        foreach (Vector3 cords in vertices)
        {
            Gizmos.DrawSphere(cords, 0.1f);
        }
    }






    public void Cylindre(float rayon, float height, int meridian)
    {

        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        int[] triangles = new int[meridian * 12];
         vertices = new Vector3[2 * meridian + 2];

        for (int i = 0; i < meridian; ++i)
        {
            double teta = (2 * Math.PI) * i / (meridian);
            vertices[i] = new Vector3(rayon * (float)(Math.Cos(teta)), rayon * (float)(Math.Sin(teta)), height);

        }

        for (int i = 0; i < meridian; ++i)
        {
            double teta = (2 * Math.PI) * i / (meridian);
            vertices[meridian + i] = new Vector3(rayon * (float)Math.Cos(teta), rayon * (float)Math.Sin(teta), 0);

        }
        vertices[2 * meridian ] = new Vector3(0 ,0, 0) ;
        vertices[2 * meridian + 1] = new Vector3(0, 0, height);


        int cpt = 0;
        for (int index = 0; index < meridian-1; ++index)
        {

                triangles[cpt] = index;
                triangles[cpt + 2] = index + 1;
                triangles[cpt + 1] = index + meridian;
                // triangle 2 




                triangles[cpt + 3] = index + meridian + 1;
                triangles[cpt + 5] = index + meridian;

                triangles[cpt + 4] = index + 1;

                
            cpt  +=6;
        }

            triangles[cpt] = meridian-1;
            triangles[cpt + 2] = 0;
            triangles[cpt + 1] = meridian - 1 + meridian;




            triangles[cpt + 3] =   meridian;
            triangles[cpt + 5] = meridian - 1 + meridian;

            triangles[cpt + 4] = 0;
        cpt += 6;

        for (int index = 0; index < meridian- 1 ; ++index)
        {
            triangles[cpt +2] = index;
            triangles[cpt] = index + 1;
            triangles[cpt + 1] = 2 * meridian + 1;
            // triangle 2 




            triangles[cpt + 3] = index + meridian + 1;
            triangles[cpt + 5] = 2 * meridian ;

            triangles[cpt + 4] = index + meridian;


            cpt += 6;
        }
        triangles[cpt +2] = meridian - 1;
        triangles[cpt ] = 0;
        triangles[cpt + 1] = 2 * meridian + 1;


        triangles[cpt + 4] = meridian *2 -1;
        triangles[cpt+5] = 2 * meridian;
        triangles[cpt + 3] =  meridian ;
        cpt += 6;
        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;

    }

    // Use this for initialization
    public void Plan()
    {
        int planHeight = 20;

        int planLenght = 15;
        int sizeTab = 6;
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        vertices = new Vector3[sizeTab];
        int[] triangles = new int[sizeTab];

        int pas = 2;
        int cpt = 0;
        for (int indexY = 0; indexY < planHeight; ++indexY)
        {
            for (int indexX = 0; indexX < planLenght; ++indexX)
            {


                vertices[cpt] = new Vector3(pas * indexX, pas*indexY , 0);
                cpt = cpt+1;

            }
        }



            for (int index = 0; index < cpt ; ++index )
            {
                
                if(!(( (index+1)  % planLenght) == 0) && !(index + planLenght >= cpt)) { 
                triangles[(index) * 6] = index;
                triangles[(index) * 6 + 1] = index +  1;
                triangles[(index) * 6 + 2] = index + planLenght;
                // triangle 2 
         

                

                triangles[(index) * 6 + 3] = index + planLenght + 1;
                triangles[(index) * 6 + 4] = index + planLenght;

                triangles[(index) * 6 + 5] =  index + 1;
            }
        }
    

                Mesh msh = new Mesh();
            msh.vertices = vertices;
            msh.triangles = triangles;

            gameObject.GetComponent<MeshFilter>().mesh = msh;
            gameObject.GetComponent<MeshRenderer>().material = mat;
        
        
    }

}