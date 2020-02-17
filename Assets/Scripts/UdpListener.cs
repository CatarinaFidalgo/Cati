using UnityEngine;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Text;

public class UdpListener : MonoBehaviour
{

    public Transform remoteHead;
    public Transform remoteControllerRight;
    public Transform remotecontrollerLeft;
    
    public int port = 7002;
    

    private UdpClient _udpClient = null;
    private IPEndPoint _anyIP;
    private List<byte[]> _stringsToParse; // TMA: Store the bytes from the socket instead of converting to strings. Saves time.
    private byte[] _receivedBytes;
    //so we don't have to create again

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
            string[] transforms = s.Split((char)MessageSeparators.L1);

            remoteHead.localPosition = _parsePosition(transforms[0].Split((char)MessageSeparators.L2)[0]);
            remoteHead.localRotation = _parseRotation(transforms[0].Split((char)MessageSeparators.L2)[1]);

            remoteControllerRight.localPosition = _parsePosition(transforms[1].Split((char)MessageSeparators.L2)[0]);
            remoteControllerRight.localRotation = _parseRotation(transforms[1].Split((char)MessageSeparators.L2)[1]);

            remotecontrollerLeft.localPosition = _parsePosition(transforms[2].Split((char)MessageSeparators.L2)[0]);
            remotecontrollerLeft.localRotation = _parseRotation(transforms[2].Split((char)MessageSeparators.L2)[1]);

        }
    }

    private Quaternion _parseRotation(string v)
    {
        Quaternion ret = Quaternion.identity;

        string [] values = v.Split((char)MessageSeparators.L3);

        ret.x = float.Parse(values[0]);
        ret.y = float.Parse(values[1]);
        ret.z = float.Parse(values[2]);
        ret.w = float.Parse(values[3]);

        return ret;
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

    void OnApplicationQuit()
    {
        if (_udpClient != null) _udpClient.Close();
    }

    void OnQuit()
    {
        OnApplicationQuit();
    }
}