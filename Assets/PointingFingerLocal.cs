using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointingFingerLocal : MonoBehaviour
{
    public Transform rightTip;
    public Transform leftTip;
    public Transform pointingHandTipLocal;

    public bool isPointingRight;

    void Update()
    {
        //Assumes the pointing hand is the one that is further ahead
        if (rightTip.position.z >= leftTip.position.z) //pointing hand is the RIGHT hand
        {
            pointingHandTipLocal.position = rightTip.position;
            pointingHandTipLocal.rotation = rightTip.rotation;
            
            isPointingRight = true;
        }
        else if (rightTip.position.z < leftTip.position.z) //pointing hand is the LEFT hand
        {
            pointingHandTipLocal.position = leftTip.position;
            pointingHandTipLocal.rotation = leftTip.rotation;
            
            isPointingRight = false;
        }
    }
}
