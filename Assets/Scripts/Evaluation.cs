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
            //MAIS MIL CENAS
            remoteWorkspace.localScale = new Vector3(1, 1, 1);
        }

        else if (condition == ConditionType.SideToSide)
        {
            remoteWorkspace.localRotation = Quaternion.Euler(0, -25, 0); //Por agora. Mais tarde: 0 diferença na rotação
            localWorkspace.localRotation = Quaternion.Euler(0, 25, 0);
            remoteWorkspace.localScale = new Vector3(1, 1, 1);
        }

        else // condition == ConditionType.Approach
        {
            remoteWorkspace.localRotation = Quaternion.Euler(0, 180, 0);
            remoteWorkspace.localScale = new Vector3(-1, 1, 1);
        }
    }


    void Update()
    {


        
    }
}
