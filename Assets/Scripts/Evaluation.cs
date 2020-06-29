using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ConditionType
{
    Veridical,
    Approach,
    SideToSide
}

public enum TestType
{
    T1,
    T2,
    T3,
    T4
}


public class Evaluation : MonoBehaviour
{

    public ConditionType condition;
    public TestType test;
    public string participantID;

    public Transform localWorkspace;
    public Transform remoteWorkspace;

    public GameObject veridicalSetUp;
    public GameObject approachSetUp;

    public GameObject model;
    

    public Transform remoteAvatar;
    public StartEndLogs startEnd;

    void Start()
    {
        remoteWorkspace.localRotation = Quaternion.Euler(0, 180, 0);
        remoteWorkspace.localScale = new Vector3(1, 1, 1);

        /*if ( condition == ConditionType.Veridical)
        {           
            remoteWorkspace.localRotation = Quaternion.Euler(0, 180, 0);            
            remoteWorkspace.localScale = new Vector3(1, 1, 1);
            
        }

        /*else if (condition == ConditionType.SideToSide)
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
        }*/


    }

    void Update()
    {
        if (startEnd.showWorkspace)
        {
            model.SetActive(true);
            
        }
        else
        {
            model.SetActive(false);
        }
        
    }
}


