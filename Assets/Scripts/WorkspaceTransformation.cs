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

    public Transform remoteHMD;
    public Transform warpedHMD;

    public Transform newPivotLeft;
    public Transform targetLeft;

    public VRRig remoteVRRig;
    public Transform remoteAvatar;

    public Transform warpedSpace;

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


        if (evaluation.condition == ConditionType.Approach)
        {

            warpedSpace.localScale = new Vector3(-1, 1, 1);

            warpedHMD.localPosition = remoteHMD.localPosition;

            warpedHandRight.localPosition = remoteLeft.localPosition;

            warpedHandLeft.localPosition = remoteRight.localPosition;



            warpedHMD.localRotation = remoteHMD.localRotation;

            warpedHandRight.localRotation = remoteLeft.localRotation;

            warpedHandLeft.localRotation = remoteRight.localRotation;



            ////transform.localRotation = Quaternion.Euler(0, 180, 0);
            ////transform.localScale = new Vector3(-1, 1, 1);



            //warpedHandLeft.localPosition = remoteRight.localPosition;
            //warpedHandLeft.LookAt(warpedHandLeft.position + remoteRight.forward, remoteRight.up);

            //warpedHandRight.localPosition = remoteLeft.localPosition;
            //warpedHandRight.LookAt(warpedHandRight.position + remoteLeft.forward, remoteLeft.up);



            //if (inWorkspace)
            //{
            //    /* LEFt HAND */

            //    //TIP.position = handLeftTip.position;
            //    //TIP.RotateAroundLocal(Vector3.up, 180f); //TIP.Rotate();







            //    Debug.Log("Approach in workspace");
            //}
            //else
            //{




            //    Debug.Log("Approach out workspace");
            //}




        }




        if (evaluation.condition == ConditionType.Veridical)
        {
            // Debug.Log("Veridical");

            warpedHMD.localPosition = remoteHMD.localPosition;
            warpedHMD.localRotation = remoteHMD.localRotation;


            warpedHandLeft.localPosition = remoteLeft.localPosition; 
            warpedHandLeft.localRotation = remoteLeft.localRotation;

            warpedHandRight.localPosition = remoteRight.localPosition; 
            warpedHandRight.localRotation = remoteRight.localRotation;
        }
    }

    void LateUpdate()
    {

    }

    private Vector3 _invert(Vector3 p)
    {
        return new Vector3(p.x, p.y, -p.z);
    }
}
