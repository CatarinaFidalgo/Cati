using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK;

public class StartEndLogs : MonoBehaviour
{
    public string _resultsFolder = "/Results";
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

    private string participantID;
    private string _logBodyLocalPath;
    private string _logBodyRemotePath;
    private string _logVolumePointsPath;
    private string _logIntersectionPath;
    private string _logTimePath;

    private string delta_Time;
    private bool start = true;
    private bool getStartTime = true;
    private bool getEndTime = true;

    //IEnumerator Start()
    void Update()
    {
        if (start && role.localIsDemonstrator)
        {
            InitializeFiles();
        }

        if (role.localIsDemonstrator)
        {
            if (trail.pressed && getStartTime)
            {
                taskStartTime = DateTime.Now;
                getStartTime = false;
                Debug.Log("Start Time:" + taskStartTime.ToString());
            }

            if (udpListener.receptionComplete && getEndTime)
            {
                
                taskEndTime = DateTime.Now;
                getEndTime = false;
                Debug.Log("End Time:" + taskEndTime.ToString());

                //delta_Time = MinutesToString(StringToMinutes(taskEndTime) - StringToMinutes(taskStartTime));
                //Debug.Log(delta_Time);
                //Save time elapsed
                File.AppendAllText(_logTimePath, taskStartTime.ToString() + "#" + taskEndTime.ToString() + "\n");//+ "#" + delta_Time + "\n");

            }

            if (chlocal.writeFile)
            {
                //Save drawn data points (Local)
                File.AppendAllText(_logVolumePointsPath, System.DateTime.Now.ToString("HH:mm:ss") + "#Local:#" + udpSend.logPoints + "\n");
                chlocal.writeFile = false;
                Debug.Log("TrailPointsLocal");

            }

            if (chremote.writeFile)
            {
                //Save drawn data points (Remote)
                File.AppendAllText(_logVolumePointsPath, System.DateTime.Now.ToString("HH:mm:ss") + "#Local:#" + udpListener.logPoints + "\n");
                chremote.writeFile = false;
                Debug.Log("TrailPointsRemote");

            }


            if (intersection.writeResult)
            {                            
                //Save volumes and intersection
                File.AppendAllText(_logIntersectionPath, System.DateTime.Now.ToString("HH:mm:ss") + "#" + intersection.percentageString + "\n");
                

                //Update booleans

                getStartTime = true;
                getEndTime = true;                
                intersection.writeResult = false;

                Debug.Log("intersection");
                

            }

            //Save body data for both participants
            File.AppendAllText(_logBodyLocalPath, System.DateTime.Now.ToString("HH:mm:ss") + "#" + udpSend.logGeneral + "\n");
            File.AppendAllText(_logBodyRemotePath, System.DateTime.Now.ToString("HH:mm:ss") + "#" + udpListener.logGeneral + "\n");
            

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

        start = false;
    }

    float StringToMinutes(DateTime time)
    {
        string[] parcels = time.ToString().Split(':');
        Debug.Log("StringToMinutes");

        return (float.Parse(parcels[0]) * 60.0f + float.Parse(parcels[1]) + float.Parse(parcels[2]) / 60.0f);

    }


    string MinutesToString(float time)
    {
        double seconds = Math.Truncate((time - Math.Truncate(time)) * 60.0f);
        Debug.Log("MinutesToString");

        return "00:" + Math.Truncate(time) + ":" + seconds + "\n\n\n";

    }
}
