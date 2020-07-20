﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using GK;

public class UdpEvaluationCommands : MonoBehaviour
{

    public Evaluation eval;
    public int port;

    private UdpClient _udpClient = null;
    private IPEndPoint _anyIP;
    private List<byte[]> _stringsToParse; // TMA: Store the bytes from the socket instead of converting to strings. Saves time.
    private byte[] _receivedBytes;
    //so we don't have to create again

    public StartEndLogs startend;

    void Start()
    {
        udpRestart();
    }

    public void udpRestart()
    {
        if (_udpClient != null)
        {
            _udpClient.Close();
        }

        _stringsToParse = new List<byte[]>();
        _anyIP = new IPEndPoint(IPAddress.Any, port);
        _udpClient = new UdpClient(_anyIP);
        _udpClient.BeginReceive(new AsyncCallback(this.ReceiveCallback), null);

    }

    public void ReceiveCallback(IAsyncResult ar)
    {
        Byte[] receiveBytes = _udpClient.EndReceive(ar, ref _anyIP);
        _udpClient.BeginReceive(new AsyncCallback(this.ReceiveCallback), null);
        _stringsToParse.Add(receiveBytes);
    }

    void Update()
    {
        while (_stringsToParse.Count > 0)
        {
            try
            {
                byte[] toProcess = _stringsToParse.First();
                if (toProcess != null)
                {

                    string stringToParse = Encoding.ASCII.GetString(toProcess);
                    _parseString(stringToParse);

                }
                _stringsToParse.RemoveAt(0);
            }
            catch (Exception /*e*/) { _stringsToParse.RemoveAt(0); }
        }
    }

    private void _parseString(string s)
    {
        if (s != "")
        {
            //Debug.Log(s);
            string[] tokens = s.Split('#');

            if (tokens[0] == "start")
            {
                _startEvaluation(tokens);
            }
            else if (tokens[0] == "update")
            {
                startend.showWorkspace = Convert.ToBoolean(tokens[1]);
            }


        }
    }

    private void _startEvaluation(string[] tokens)
    {
        eval.condition = (ConditionType)Enum.Parse(typeof(ConditionType), tokens[1]);
        eval.test = (TestType)Enum.Parse(typeof(TestType), tokens[2]);
        if (eval.machine == MachineType.A)
        {
            eval.participantID = tokens[3];
        }
        else if (eval.machine == MachineType.B)
        {
            eval.participantID = tokens[4];
        }

        eval._resultsFolder = tokens[5];

        
    }

    void OnApplicationQuit()
    {
        if (_udpClient != null) _udpClient.Close();
    }

    void OnQuit()
    {
        OnApplicationQuit();
    }
}