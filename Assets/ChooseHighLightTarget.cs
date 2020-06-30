using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;


public class ChooseHighLightTarget : MonoBehaviour
{
    public int i = 1;
    public int j = 0;

    public List<Vector3> Test1 = new List<Vector3>();
    public List<Vector3> Test2 = new List<Vector3>();
    public List<Vector3> Test3 = new List<Vector3>();
    public List<Vector3> Test4 = new List<Vector3>();
    public List<Vector3> TestOn = new List<Vector3>();

    public Evaluation evaluation;

    public Transform target;
    public Transform p101;
    public Transform p102;
    public Transform p103;




    void Start()
    {
        FillTargetPoints(Test1, Test2, Test3, Test4);

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

        

    }

    
    void Update()
    {
        
        

        if (j < 5)
        {
            //101 active
            target.position = TestOn[j];

            p101.GetComponent<HighLight>().radius = 0.05f;
            p102.GetComponent<HighLight>().radius = 0.0f;
            p103.GetComponent<HighLight>().radius = 0.0f;

            /*p101.GetComponent<HighLight>().enabled = true;            
            p102.GetComponent<HighLight>().enabled = false;
            p103.GetComponent<HighLight>().enabled = false;*/
        }

        else if ( j >= 5 && j < 12)
        {
            //102 active
            target.position = TestOn[j];

            p101.GetComponent<HighLight>().radius = 0.0f;
            p102.GetComponent<HighLight>().radius = 0.05f;
            p103.GetComponent<HighLight>().radius = 0.0f;
        }

        else if (j >= 12 && j < 16)
        {
            //103 active
            target.position = TestOn[j];

            p101.GetComponent<HighLight>().radius = 0.0f;
            p102.GetComponent<HighLight>().radius = 0.0f;
            p103.GetComponent<HighLight>().radius = 0.05f;
        }

        else
        {

            Debug.Log("END OF EXPERIMENT");
            
        }

    }

    void FillTargetPoints(List<Vector3> Test1, List<Vector3> Test2, List<Vector3> Test3, List<Vector3> Test4)
    {
        string[] lines = System.IO.File.ReadAllLines(@"C:\Users\catar\Documents\GitHub\Cati\Assets\TargetPoints.txt");
        
        //int i = 0;

        //Debug.Log("Nr Lines in Text File: " + lines.Length);

        foreach (string line in lines)
        {
            string[] c = line.Split('#');

            //Debug.Log("Nr entries in line: " + c.Length);

            Test1.Add(new Vector3(float.Parse(c[0]), float.Parse(c[1]), float.Parse(c[2])));
            Test2.Add(new Vector3(float.Parse(c[3]), float.Parse(c[4]), float.Parse(c[5])));
            Test3.Add(new Vector3(float.Parse(c[6]), float.Parse(c[7]), float.Parse(c[8])));
            Test4.Add(new Vector3(float.Parse(c[9]), float.Parse(c[10]), float.Parse(c[11])));
            i++;
            //Debug.Log(i);
        }
        
        return;
    }

}
