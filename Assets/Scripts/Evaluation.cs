using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ConditionType
{
    Veridical,
    Approach,
    SideToSide
}

public enum Handedness
{
    Right_Handed,
    Left_Handed
}




public class Evaluation : MonoBehaviour
{

    public ConditionType condition;
    public Handedness handedness;

    public Transform localWorkspace;
    public Transform remoteWorkspace;

    public GameObject veridicalSetUp;
    public GameObject approachSetUp;

    public Transform remoteAvatar;

    void Start()
    {

        if ( condition == ConditionType.Veridical)
        {           
            remoteWorkspace.localRotation = Quaternion.Euler(0, 180, 0);            
            remoteWorkspace.localScale = new Vector3(1, 1, 1);

            veridicalSetUp.SetActive(true);
            approachSetUp.SetActive(false);
        }

        else if (condition == ConditionType.SideToSide)
        {
            remoteWorkspace.localRotation = Quaternion.Euler(0, -25, 0); //Por agora. Mais tarde: 0 diferença na rotação
            localWorkspace.localRotation = Quaternion.Euler(0, 25, 0);
            remoteWorkspace.localScale = new Vector3(1, 1, 1);

            veridicalSetUp.SetActive(true);
            approachSetUp.SetActive(false);
            Debug.Log("I'm side to side");
        }

        else // condition == ConditionType.Approach
        {
            remoteWorkspace.localRotation = Quaternion.Euler(0, 180, 0);
            remoteWorkspace.localScale = new Vector3(1, 1, 1);
                    
            veridicalSetUp.SetActive(false);
            approachSetUp.SetActive(true);
        }
    }


    void LateUpdate()
    {

        //remoteAvatar.parent = remoteWorkspace;

    }
}
