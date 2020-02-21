using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkspaceTransformation : MonoBehaviour
{


    public Evaluation evaluation;

    public Transform handLeft;
    public Transform handLeftTip;
    public Transform warpedHandLeft;

    public Transform handRight;
    public Transform warpedHandRight;

    void Start()
    {
        
    }


    void Update()
    {

        if (evaluation.condition == ConditionType.Approach)
        {
            warpedHandLeft.localPosition = _invert(handLeft.localPosition);

            //Vector3 d = handLeftTip.position - handLeft.position;
            //warpedHandLeft.localPosition = warpedHandLeft.localPosition - d;



            warpedHandRight.localPosition = _invert(handRight.localPosition);



            // if dentro troce
            // else destroca










        }
        else
        {
            warpedHandLeft.localPosition = warpedHandRight.localPosition = Vector3.zero;
        }
    }

    private Vector3 _invert(Vector3 p)
    {
        return new Vector3(p.x, p.y, -p.z);
    }
}
