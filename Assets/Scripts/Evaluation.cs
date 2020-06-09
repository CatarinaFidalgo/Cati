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
    public GameObject clutter;

    public Transform remoteAvatar;
    public StartEndLogs startEnd;

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

        
    }

    void Update()
    {
        if (test == TestType.T1 && startEnd.showWorkspace)
        {
            test1.SetActive(true);
            test2.SetActive(false);
            test3.SetActive(false);
            test4.SetActive(false);
            clutter.SetActive(true);

        }

        /*else if (test == TestType.T1 && !startEnd.showWorkspace)
        {
            test1.SetActive(false);
            test2.SetActive(false);
            test3.SetActive(false);
            test4.SetActive(false);
            clutter.SetActive(false);
        }*/

        else if (test == TestType.T2 && startEnd.showWorkspace)
        {
            test1.SetActive(false);
            test2.SetActive(true);
            test3.SetActive(false);
            test4.SetActive(false);
            clutter.SetActive(true);
        }

        /*else if (test == TestType.T2 && !startEnd.showWorkspace)
        {
            test1.SetActive(false);
            test2.SetActive(false);
            test3.SetActive(false);
            test4.SetActive(false);
            clutter.SetActive(false);
        }*/

        else if (test == TestType.T3 && startEnd.showWorkspace)
        {
            test1.SetActive(false);
            test2.SetActive(false);
            test3.SetActive(true);
            test4.SetActive(false);
            clutter.SetActive(true);
        }

        /*else if (test == TestType.T3 && !startEnd.showWorkspace)
        {
            test1.SetActive(false);
            test2.SetActive(false);
            test3.SetActive(false);
            test4.SetActive(false);
            clutter.SetActive(false);
        }*/

        else if (test == TestType.T4 && startEnd.showWorkspace)
        {
            test1.SetActive(false);
            test2.SetActive(false);
            test3.SetActive(false);
            test4.SetActive(true);
            clutter.SetActive(true);
        }

        /*else if (test == TestType.T4 && !startEnd.showWorkspace)
        {
            test1.SetActive(false);
            test2.SetActive(false);
            test3.SetActive(false);
            test4.SetActive(false);
            clutter.SetActive(false);
        }*/
        else
        {
            test1.SetActive(false);
            test2.SetActive(false);
            test3.SetActive(false);
            test4.SetActive(false);
            clutter.SetActive(false);
        }
    }
}

/**/



/*if (test == TestType.T1)
    {
        if (startEnd.showWorkspace)
        {
            test1.SetActive(true);
            test2.SetActive(false);
            test3.SetActive(false);
            test4.SetActive(false);
            clutter.SetActive(true);
        }

        else
        {
            test1.SetActive(false);
            clutter.SetActive(false);
        }

    }

    if (test == TestType.T2)
    {
        if (startEnd.showWorkspace)
        {
            test1.SetActive(false);
            test2.SetActive(true);
            test3.SetActive(false);
            test4.SetActive(false);
            clutter.SetActive(true);
        }

        else
        {
            test2.SetActive(false);
            clutter.SetActive(false);
        }

    }

    if (test == TestType.T3)
    {
        if (startEnd.showWorkspace)
        {
            test1.SetActive(false);
            test2.SetActive(false);
            test3.SetActive(true);
            test4.SetActive(false);
            clutter.SetActive(true);
        }

        else
        {
            test3.SetActive(false);
            clutter.SetActive(false);
        }

    }

    if (test == TestType.T4)
    {
        if (startEnd.showWorkspace)
        {
            test1.SetActive(false);
            test2.SetActive(false);
            test3.SetActive(false);
            test4.SetActive(true);
            clutter.SetActive(true);
        }

        else
        {
            test4.SetActive(false);
            clutter.SetActive(false);
        }

    }        */
