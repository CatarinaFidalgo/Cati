﻿using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityScript.TypeSystem;

public enum MachineType
{ 
    A,
    B
}

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
    public MachineType machine; 
    public ConditionType condition;
    public TestType test;
    public string participantID;
    public string _resultsFolder;

    public bool localIsDemonstrator;

    public Transform localWorkspace;
    public Transform remoteWorkspace;

    public GameObject veridicalSetUp;
    public GameObject approachSetUp;

    public GameObject model;
    

    public Transform remoteAvatar;
    public StartEndLogs startEnd;

    public bool showUI;

    public SendSetUpInfo sendInfo;

    public SendAvatar sendAvatar;
    public UdpListener receiveAvatar;

    void Start()
    {
        if (machine == MachineType.A)
        {
            sendAvatar.port = 7001;
            receiveAvatar.port = 7101;
        }

        else if (machine == MachineType.B)
        {
            sendAvatar.port = 7101;
            receiveAvatar.port = 7001;
        }

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
        

        if (Input.GetKeyDown(KeyCode.Return) && machine == MachineType.A)
        {
            showUI = !showUI;
        }

    }







    private ConditionType UI_condition = ConditionType.Approach;
    private string UI_ParticipantID_A = "";
    private string UI_ParticipantID_B = "";
    private string UI_ResultsFolder = "";
    private int UI_test = 1;
    public GUIStyle myStyle;
    void OnGUI()
    {
        if (machine == MachineType.A && showUI) 
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

            int left = 10;
            int top = 10;
            int linestep = 25;

            GUI.Label(new Rect(left, top, 100, linestep), "Condition:", myStyle);
            if (GUI.Button(new Rect(left + 200, top, 100, linestep), UI_condition.ToString()))
            {
                if (UI_condition == ConditionType.Approach)
                    UI_condition = ConditionType.Veridical;
                else if (UI_condition == ConditionType.Veridical)
                    UI_condition = ConditionType.Approach;
            }
            top += linestep;
            
            GUI.Label(new Rect(left, top, 100, linestep), "Test:", myStyle);
            if (UI_test > 1 && GUI.Button(new Rect(left + 200, top, 20, 20), "<"))
            {
                UI_test -= 1;
            }
            GUI.Label(new Rect(left + 200 + 30, top, 100, linestep), "Test " + UI_test);
            if (UI_test < 4 && GUI.Button(new Rect(left + 200 + 100 - 20, top, 20, 20), ">"))
            {
                UI_test += 1;
            }

            top += linestep;


            GUI.Label(new Rect(left, top, 100, linestep), "Participant ID A:", myStyle);
            UI_ParticipantID_A = GUI.TextField(new Rect(left + 200, top, 100, linestep), UI_ParticipantID_A);
            top += linestep;

            GUI.Label(new Rect(left, top, 100, linestep), "Participant ID B:", myStyle);
            UI_ParticipantID_B = GUI.TextField(new Rect(left + 200, top, 100, linestep), UI_ParticipantID_B);
            top += linestep;

            GUI.Label(new Rect(left, top, 100, linestep), "Results Folder:", myStyle);
            UI_ResultsFolder = GUI.TextField(new Rect(left + 200, top, 100, linestep), UI_ResultsFolder);
            top += linestep;

            if (GUI.Button(new Rect(left, top, 100, linestep), "START"))
            {
                string s = "start" + '#';
                s += UI_condition.ToString() + '#';
                s += (UI_test - 1).ToString() + '#';
                s += UI_ParticipantID_A + '#';
                s += UI_ParticipantID_B + '#';
                s += UI_ResultsFolder;
                sendInfo.send(s);
               // Debug.Log("sent:" + s);
                showUI = false;
            }
        }
    }



}


