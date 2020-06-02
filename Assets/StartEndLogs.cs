using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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

    private string participantID;
    private string _logBodyLocalPath;
    private string _logBodyRemotePath;
    private string _logVolumePointsPath;
    private string _logIntersectionPath;
    private string _logTimePath;
    void Start()
    {
        //Application.runInBackground = true;
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


        /*_loadConfig();
        _sessionTimeMilliseconds = SessionTimeMinutes * 60000;*/

    }

    // Update is called once per frame
    void Update()
    {
        if (role.localIsDemonstrator)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                taskStartTime = DateTime.Now;
            }

            if (udpListener.receptionComplete)
            {
                taskEndTime = DateTime.Now;
                //Save drawn data points (Local + Remote)
                File.AppendAllText(_logVolumePointsPath, System.DateTime.Now.ToString("HH:mm:ss") + "#Local:#" + udpSend.logPoints + "\n");
                File.AppendAllText(_logVolumePointsPath, System.DateTime.Now.ToString("HH:mm:ss") + "#Remote:#" + udpListener.logPoints + "\n");
                //Save volumes and intersection
                File.AppendAllText(_logIntersectionPath, System.DateTime.Now.ToString("HH:mm:ss") + "#" + intersection.percentageString + "\n");
                //Save time elapsed
                File.AppendAllText(_logTimePath, taskStartTime.ToString() + "#" + taskEndTime.ToString() + "#" + MinutesToString(StringToMinutes(taskEndTime) - StringToMinutes(taskStartTime)) + "\n");
            }

            //Save body data for both participants
            File.AppendAllText(_logBodyLocalPath, System.DateTime.Now.ToString("HH:mm:ss") + "#" + udpSend.logGeneral + "\n");
            File.AppendAllText(_logBodyRemotePath, System.DateTime.Now.ToString("HH:mm:ss") + "#" + udpListener.logGeneral + "\n");
            

        }

        

        //if local is not the demonstrator the data won't be stored in his unit
        //Logs are always saved in the demonstrators site
    }

    float StringToMinutes(DateTime time)
    {
        string[] parcels = time.ToString().Split(':');

        return (float.Parse(parcels[0]) * 60.0f + float.Parse(parcels[1]) + float.Parse(parcels[2]) / 60.0f);

    }


    string MinutesToString(float time)
    {
        double seconds = Math.Truncate((time - Math.Truncate(time)) * 60.0f);

        return "00:" + Math.Truncate(time) + ":" + seconds + "\n\n\n";

    }
}
