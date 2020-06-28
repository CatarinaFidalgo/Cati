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
        target.position = TestOn[j];

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
