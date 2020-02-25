using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkspaceTransformation : MonoBehaviour
{


    public bool inWorkspace;
    public BoxCollider volumeCollider;

    public Evaluation evaluation;

    public Transform remoteLeft;
    public Transform warpedHandLeft;


    public Transform handLeftTip;
    public Transform TIP;

    public Transform remoteRight;
    public Transform warpedHandRight;

    public Transform newPivotLeft;
    public Transform targetLeft;

    void Start()
    {
        
    }


    void Update()
    {
        if (volumeCollider.bounds.Contains(remoteLeft.position) || volumeCollider.bounds.Contains(remoteRight.position))
        {
           inWorkspace = true;
        }
        else
        {           
            inWorkspace = false;
        }


        if (evaluation.condition == ConditionType.Approach && inWorkspace == true)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            transform.localScale = new Vector3(-1, 1, 1);

            warpedHandLeft.localPosition = _invert(remoteRight.localPosition);
            warpedHandLeft.localRotation = remoteRight.localRotation;

           // warpedHandLeft.LookAt(targetLeft.position - (-newPivotLeft.right), newPivotLeft.forward);

            //Vector3 d = handLeftTip.position - remoteLeft.position;
            //warpedHandLeft.localPosition = warpedHandLeft.localPosition - d;



            warpedHandRight.localPosition = _invert(remoteLeft.localPosition);
            warpedHandRight.localRotation = remoteLeft.localRotation;


            //TIP.position = handLeftTip.position;
            //TIP.rotation = handLeftTip.rotation;

            Debug.Log("Approach in workspace");


        }

        else if (evaluation.condition == ConditionType.Approach && inWorkspace == false)
        {

            transform.localRotation = Quaternion.Euler(0, 180, 0);
            transform.localScale = new Vector3(1, 1, 1);

            warpedHandLeft.localPosition = remoteLeft.localPosition;
            warpedHandLeft.localRotation = remoteLeft.localRotation;

            warpedHandRight.localPosition = remoteRight.localPosition;
            warpedHandRight.localRotation = remoteRight.localRotation;

            Debug.Log("Approach out workspace");

        }


        else  // VERIDICAL
        {
           // Debug.Log("Veridical");

            warpedHandLeft.localPosition = remoteLeft.localPosition; 
            warpedHandLeft.localRotation = remoteLeft.localRotation;

            warpedHandRight.localPosition = remoteRight.localPosition; 
            warpedHandRight.localRotation = remoteRight.localRotation;
        }
    }

    private Vector3 _invert(Vector3 p)
    {
        return new Vector3(p.x, p.y, -p.z);
    }
}
