using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTest : MonoBehaviour
{
    public int i = 1;
    public int j = 0;

    public Transform Test1;
    public Transform Test2;
    public Transform Test3;
    public Transform Test4;
    
    private Transform previous;

    public Evaluation evaluation;

    private Transform TestOn;

    

    void Start()
    {
        //previous = Q1.GetChild(j).GetChild(0);

        if (evaluation.test == TestType.T1)
        {
            TestOn = Test1;
        }
        else if (evaluation.test == TestType.T2)
        {
            TestOn = Test2;
        }
        else if (evaluation.test == TestType.T3)
        {
            TestOn = Test3;
        }
        else
        {
            TestOn = Test4;
        }

        previous = TestOn.GetChild(0).GetChild(0).GetChild(0);

    }

    // Update is called once per frame
    void Update()
    {
        //return;
        if (i == 1)
        {
            previous.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            TestOn.GetChild(0).GetChild(j).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.HSVToRGB(148.0f / 360.0f, 1, 1));
            previous = TestOn.GetChild(0).GetChild(j).GetChild(0);
        }

        if (i == 2)
        {
            previous.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            TestOn.GetChild(1).GetChild(j).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.HSVToRGB(148.0f / 360.0f, 1, 1));
            previous = TestOn.GetChild(1).GetChild(j).GetChild(0);
        }

        if (i == 3)
        {
            previous.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            TestOn.GetChild(2).GetChild(j).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.HSVToRGB(148.0f / 360.0f, 1, 1));
            previous = TestOn.GetChild(2).GetChild(j).GetChild(0);
        }

        if (i == 4)
        {
            previous.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            TestOn.GetChild(3).GetChild(j).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.HSVToRGB(148.0f / 360.0f, 1, 1));
            previous = TestOn.GetChild(3).GetChild(j).GetChild(0);
        }

    }
}
