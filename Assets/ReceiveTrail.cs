﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using GK;

public class ReceiveTrail : MonoBehaviour
{

    //Strings for Loging
   
    public string logPoints;

    
    public List<Vector3> remotePoints;
    public bool receptionComplete = false;
    public bool udpWriteFile = true;


    public Evaluation eval;


    void Start()
    {
        remotePoints = new List<Vector3>();
    }

    
    void Update()
    {
    }

    public void newTrailMessage(string trailMessage)
    {
        if (trailMessage != "")
        {
            Debug.Log("TrailMessage diferente de zero" + trailMessage);
            
            if (receptionComplete == false && eval.localIsDemonstrator)
            {
                Debug.Log("Received once");
                
                remotePoints = _stringToList(trailMessage).ToList();
                Debug.Log(remotePoints.Count);
                logPoints = trailMessage;
                receptionComplete = true;
                udpWriteFile = true;
            }
        }
    }

   

    private HashSet<Vector3> _stringToList(string msg)
    {
        HashSet<Vector3> points = new HashSet<Vector3>();

        string[] stringPoints = msg.Split((char)MessageSeparators.L2);
        //Debug.Log(stringPoints.Length);
        //Debug.Log("a1");

        for (int i = 0; i < stringPoints.Length - 1; i++)
        {
            points.Add(_parsePosition(stringPoints[i]));
            //Debug.Log(points.ToList()[i]);
            //Debug.Log("points count: " + points.Count);

        }

        //Debug.Log("a3");
        return points;
    }

    private Vector3 _parsePosition(string v)
    {
        Vector3 ret = Vector3.zero;

        string[] values = v.Split((char)MessageSeparators.L3);

        ret.x = float.Parse(values[0]);
        ret.y = float.Parse(values[1]);
        ret.z = float.Parse(values[2]);

        return ret;
    }

}