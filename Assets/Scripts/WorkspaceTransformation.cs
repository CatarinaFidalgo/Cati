using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkspaceTransformation : MonoBehaviour
{


    public Evaluation evaluation;

    public Transform remoteLeft;
    public Transform warpedHandLeft;


    public Transform handLeftTip;
    public Transform TIP;

    public Transform remoteRight;
    public Transform warpedHandRight;
    

    void Start()
    {
        
    }


    void Update()
    {

        if (evaluation.condition == ConditionType.Approach)
        {
            warpedHandLeft.localPosition = _invert(remoteRight.localPosition);

            //Vector3 d = handLeftTip.position - remoteLeft.position;
            //warpedHandLeft.localPosition = warpedHandLeft.localPosition - d;



            warpedHandRight.localPosition = _invert(remoteLeft.localPosition);



            // if dentro troce
            // else destroca


            TIP.position = handLeftTip.position;
            TIP.rotation = handLeftTip.rotation;







        }
        else  // VERIDICAL
        {
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
