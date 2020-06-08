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

    public GameObject test1;
    public GameObject test2;
    public GameObject test3;
    public GameObject test4;

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

        /*else if (condition == ConditionType.SideToSide)
        {
            remoteWorkspace.localRotation = Quaternion.Euler(0, -25, 0); //Por agora. Mais tarde: 0 diferença na rotação
            localWorkspace.localRotation = Quaternion.Euler(0, 25, 0);
            remoteWorkspace.localScale = new Vector3(1, 1, 1);

            veridicalSetUp.SetActive(true);
            approachSetUp.SetActive(false);
            Debug.Log("I'm side to side");
        }*/

        else // condition == ConditionType.Approach
        {
            remoteWorkspace.localRotation = Quaternion.Euler(0, 180, 0);
            remoteWorkspace.localScale = new Vector3(1, 1, 1);
                    
            veridicalSetUp.SetActive(false);
            approachSetUp.SetActive(false);
        }

        if (test == TestType.T1)
        {
            test1.SetActive(true);
            test2.SetActive(false);
            test3.SetActive(false);
            test4.SetActive(false);

        }

        if (test == TestType.T2)
        {
            test1.SetActive(false);
            test2.SetActive(true);
            test3.SetActive(false);
            test4.SetActive(false);
        }

        if (test == TestType.T3)
        {
            test1.SetActive(false);
            test2.SetActive(false);
            test3.SetActive(true);
            test4.SetActive(false);
        }

        if (test == TestType.T4)
        {
            test1.SetActive(false);
            test2.SetActive(false);
            test3.SetActive(false);
            test4.SetActive(true);
        }
    }

}
