using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Courbe : MonoBehaviour
{

    public List<Vector3> listePoints;

    private LineRenderer lr;


    public void Awake()
    {
        lr = GetComponent<LineRenderer>();
        
    }

 
    
    
    
    
    private void Update()
    {
        
        
        

        if (Input.GetKeyDown("space"))
        {
            Chaikin();
            lr.positionCount = listePoints.Count + 1;
            for (int indexListePoints = 0; indexListePoints < listePoints.Count; indexListePoints++)

            {
                lr.SetPosition(indexListePoints, listePoints[indexListePoints]);
            }
            lr.SetPosition(listePoints.Count , listePoints[0]);
        }
    }
    
  
    
    
    
    private void Chaikin()
    {
        List<Vector3> listPointsTemporary = new List<Vector3>(); 
        for (int indexListePoints = 0; indexListePoints < listePoints.Count; indexListePoints++)
        {
            Vector3 p0 = listePoints[indexListePoints];
            Vector3 p1;
            if (indexListePoints == listePoints.Count - 1)
            {
                p1 = listePoints[0];
            }
            else
            {
                p1 = listePoints[indexListePoints + 1];
            }


            Vector3 pointQ = new Vector3(0.75f * p0.x + 0.25f * p1.x, 0.75f * p0.y + 0.25f * p1.y, 0.75f * p0.z + 0.25f * p1.z);
            Vector3 pointR = new Vector3(0.25f * p0.x + 0.75f * p1.x, 0.25f * p0.y + 0.75f * p1.y, 0.25f * p0.z + 0.75f * p1.z);
            listPointsTemporary.Add(pointQ);
            listPointsTemporary.Add(pointR);
            
        }
        listePoints = listPointsTemporary;
    }


        
}