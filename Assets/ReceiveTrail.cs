using UnityEngine;
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


    public int port;

    public Evaluation eval;



    private UdpClient _udpClient = null;
    private IPEndPoint _anyIP;
    private List<byte[]> _stringsToParse; // TMA: Store the bytes from the socket instead of converting to strings. Saves time.
    private byte[] _receivedBytes;
    //so we don't have to create again

    void Start()
    {
        udpRestart();
        remotePoints = new List<Vector3>();
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
            Debug.Log(s);

            string transforms = s;

            

            if (receptionComplete == false && eval.localIsDemonstrator)
            {
                Debug.Log("Received once");
                //Debug.Log(remotePoints.Count);
                remotePoints = _stringToList(transforms).ToList();
                //Debug.Log(remotePoints.Count);
                logPoints = transforms;
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
            //Debug.Log(points.Count);

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


    void OnApplicationQuit()
    {
        if (_udpClient != null) _udpClient.Close();
    }

    void OnQuit()
    {
        OnApplicationQuit();
    }
}