using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ConditionType
{
    Veridical,
    Approach,
    SideToSide
}



public class Evaluation : MonoBehaviour
{

    public ConditionType condition;

    public Transform localWorkspace;
    public Transform remoteWorkspace;

    void Start()
    {
        if ( condition == ConditionType.Veridical)
        {           
            remoteWorkspace.localRotation = Quaternion.Euler(0, 180, 0);
        }

        else if (condition == ConditionType.SideToSide)
        {
            remoteWorkspace.localRotation = Quaternion.Euler(0, -25, 0);
            localWorkspace.localRotation = Quaternion.Euler(0, 25, 0);
        }

        else // condition == ConditionType.Approach
        {
            remoteWorkspace.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }


    void Update()
    {


        
    }
}
