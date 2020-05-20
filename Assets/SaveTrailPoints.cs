using GK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrailPoints : MonoBehaviour
{
    public Transform trail;
    private int i = 0;
    public List<Vector3> pointsTrail = new List<Vector3>();
    public bool pressed = false;
    public ConvexHullTry convexHullTry;
    public Renderer rend;
    public TrailRenderer trailRend;
    private Vector3 mousePosition;
    //public GameObject spherePoint;

    void LateUpdate()
    {
         if (Input.GetKey(KeyCode.Mouse0))
         {
            /*

            //Make this transform follow the mouse position
            //Change to make follow the finger position

            mousePosition = Input.mousePosition;
            mousePosition.z = 10f;
            this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
            */

            //Activate trail renderer

            rend.enabled = true;
            pointsTrail.Add(trail.position); // save trail positions in the points list for the CH
            Debug.Log("Entry number" + i + ": " + pointsTrail[i]);
            //Instantiate(spherePoint, pointsTrail[i], Quaternion.identity);
            i++;

            //Change flags
            pressed = true;
            convexHullTry.generateHullDone = false;
            
            
         }  
         
         else
         {
            i = 0;
            trailRend.Clear(); //Clear past trails

            //Change flags
            pressed = false;            
            rend.enabled = false;
           

         }

    }
}
