using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK;

public class StartEndLogs : MonoBehaviour
{
    public string _resultsFolder;
    public Role role;
    
    public DateTime taskStartTime;
    public DateTime taskEndTime;

    public Evaluation evaluation;
    public SendAvatar udpSend;
    public UdpListener udpListener;
    public checkIntersection intersection;
    public SaveTrailPoints trail;
    public ConvexHullTry chlocal;
    public ConvexHullTryRemote chremote;
    public ChooseTest test;

    private string participantID;
    private string _logBodyLocalPath;
    private string _logBodyRemotePath;
    private string _logVolumePointsPath;
    private string _logIntersectionPath;
    private string _logTimePath;

    private bool start = true;
    private bool getStartTime = true;
    private bool getEndTime = true;
    public bool showWorkspace = false;


    //IEnumerator Start()
    void Start()
    {
        InitializeFiles();
    }
    void Update()
    {
        OVRInput.Update();

        if (role.localIsDemonstrator)
        {
            /*if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) >= 0.9f) //&& getStartTime)
            {
                Debug.Log("Secondary");
            }

            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= 0.9f) //&& getStartTime)
            {
                Debug.Log("Primary");
            }

            Debug.Log("Right: " + OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger));
            Debug.Log("Left: " + OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger));
            //Debug.Log("Rest: " + OVRInput.Get(OVRInput.Touch.SecondaryThumbRest));
            Debug.Log("B: " + OVRInput.Get(OVRInput.Button.Two));
            Debug.Log("A: " + OVRInput.Get(OVRInput.Button.One));*/



            //if (trail.pressed && getStartTime) //CHANGE THE START METHOD, PRESS ANOTHER BUTTON
            if(((OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) >= 0.9f) || (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= 0.9f)) && getStartTime)
            {
                showWorkspace = true;
                taskStartTime = DateTime.Now;
                getStartTime = false; 
                Debug.Log("Entrei " + taskStartTime.ToString());
            }

            if (udpListener.receptionComplete && getEndTime)
            {
                
                taskEndTime = DateTime.Now;
                getEndTime = false;

                showWorkspace = false;

                test.j++; //Change target

               /* if (test.j > 3)
                {
                    test.j = 0;
                    test.i++;

                }

                Debug.Log("i: " + test.i);*/
                Debug.Log("j: " + test.j);

                //Debug.Log("End Time:" + taskEndTime.ToString());
                File.AppendAllText(_logTimePath, taskStartTime.ToString("HH:mm:ss:fff") + "\",\"" + taskEndTime.ToString("HH:mm:ss:fff") + "\",\"" + DeltaDateTimeToSeconds(taskStartTime, taskEndTime).ToString() + "\"\n");

            }

            if (chlocal.writeFile)
            {
                //Save drawn data points (Local)
                File.AppendAllText(_logVolumePointsPath, System.DateTime.Now.ToString("HH:mm:ss:fff") + udpSend.logPoints.Replace(",",".").Replace("#", "\",\"").Replace(":", "\",\"").Replace("/", "\",\"") + "\"\n");
                chlocal.writeFile = false;
                //Debug.Log("TrailPointsLocal");

            }

            if (chremote.writeFile)
            {
                //Save drawn data points (Remote)
                File.AppendAllText(_logVolumePointsPath, System.DateTime.Now.ToString("HH:mm:ss:fff") + "\",\"" + udpListener.logPoints.Replace(",", ".").Replace("#", "\",\"").Replace(":", "\",\"").Replace("/", "\",\"") + "\"\n");
                chremote.writeFile = false;
                //Debug.Log("TrailPointsRemote");

            }

            if (intersection.writeResult)
            {                            
                //Save volumes and intersection
                File.AppendAllText(_logIntersectionPath, System.DateTime.Now.ToString("HH:mm:ss:fff") + "\",\"" + intersection.percentageString.Replace(",", ".").Replace("#", "\",\"") + "\"\n");
                

                //Update booleans

                getStartTime = true;
                getEndTime = true;                
                intersection.writeResult = false;

                //Debug.Log("intersection");
                

            }

            //Save body data for both participants
            File.AppendAllText(_logBodyLocalPath, System.DateTime.Now.ToString("HH:mm:ss:fff") + "\",\"" + udpSend.logGeneral.Replace(",", ".").Replace("#", "\",\"").Replace(":", "\",\"").Replace("/", "\",\"") + "\"" + "\n");
            File.AppendAllText(_logBodyRemotePath, System.DateTime.Now.ToString("HH:mm:ss:fff") + "\",\"" + udpListener.logGeneral.Replace(",", ".").Replace("#", "\",\"").Replace(":", "\",\"").Replace("/", "\",\"") + "\"" + "\n");

            //+ "\",\"" + sendAvatar.logGeneral.Replace("#", "\",\"").Replace(":", "\",\"").Replace("/", "\",\"") + "\""


        }


        //if local is not the demonstrator the data won't be stored in his unit
        //Logs are always saved in the demonstrators site


        //yield return new WaitForSeconds(0.0f);
    }

    void InitializeFiles()
    {
        participantID = evaluation.participantID;

        _resultsFolder = Application.dataPath + _resultsFolder;

        if (!Directory.Exists(_resultsFolder))
        {
            Directory.CreateDirectory(_resultsFolder);
            Debug.Log("Folder created: " + _resultsFolder);
        }
        else
        {
            print("Folder already exists: " + _resultsFolder);
        }

        _logBodyLocalPath = _resultsFolder + "/" + participantID + "_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "_" + evaluation.condition + "_LogBodyLocal" + ".txt";
        _logBodyRemotePath = _resultsFolder + "/" + participantID + "_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "_" + evaluation.condition + "_LogBodyRemote" + ".txt";
        _logVolumePointsPath = _resultsFolder + "/" + participantID + "_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "_" + evaluation.condition + "_LogVolumePoints" + ".txt";
        _logIntersectionPath = _resultsFolder + "/" + participantID + "_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "_" + evaluation.condition + "_LogIntersection" + ".txt";
        _logTimePath = _resultsFolder + "/" + participantID + "_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "_" + evaluation.condition + "_LogTime" + ".txt";

        //start = false;
    }

    TimeSpan DeltaDateTimeToSeconds(DateTime t0, DateTime t1)
    {
        return t1 - t0;

        /**
        string[] parcels0 = t0.ToString("HH:mm:ss:fff").Split(':');
        string[] parcels1 = t1.ToString("HH:mm:ss:fff").Split(':');

        float sec0 = float.Parse(parcels0[0]) * 3600.0f + float.Parse(parcels0[1]) * 60.0f + float.Parse(parcels0[2]) + float.Parse(parcels0[3]) / 60.0f;
        float sec1 = float.Parse(parcels1[0]) * 3600.0f + float.Parse(parcels1[1]) * 60.0f + float.Parse(parcels1[2]) + float.Parse(parcels1[3]) / 60.0f;
        //Debug.Log(sec0 +"   "+ sec1 + "   " + (sec1 - sec0));
        return (sec1-sec0);
        **/
    }


    /*string SecondsToString(float time)
    {
        double milisec = Math.Truncate((time - Math.Truncate(time)) * 60.0f);
        Debug.Log("MinutesToString");

        return "" + Math.Truncate(time) + ":" + milisec.ToString("F0");

    }*/
}
