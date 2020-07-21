using GK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public enum MessageSeparators
{
    L1 = '#', // sep transforms
    L2 = '/', // sep propertines 
    L3 = ':' // sep values
}*/


public class SendTrail : MonoBehaviour
{
    

    public SaveTrailPoints saveTrailPoints;
    

    public bool sent = false;


    
    public string logPoints;


    public int port;
    private UdpBroadcast _upd;


    

    public Evaluation eval;

    void Start()
    {
        Load();
    }

    public void Load()
    {
        _upd = new UdpBroadcast(port);
    }

    void Update()
    {
        

        if (_upd != null)
        {
            if (saveTrailPoints.pointsTrail.Count != 0 && !saveTrailPoints.pressed && !sent && !eval.localIsDemonstrator)
            {

                logPoints = _listToString(saveTrailPoints.pointsTrail);
               
                Debug.Log("Sent Once");
                sent = true;
            }


            _upd.send(logPoints);
            //Debug.Log(msg);

        }
    }

    
    private string _listToString(List<Vector3> points)
    {
        string msg = "";
        int i;

        //Debug.Log("Nr points sent: " + points.Count);

        for (i = 0; i < points.Count; i++)
        {
            msg += _positionToString(points[i]) + (char)MessageSeparators.L2;
        }

        //Debug.Log(msg);

        return msg;
    }

    private string _positionToString(Vector3 p)
    {
        return "" + p.x + (char)MessageSeparators.L3 + p.y + (char)MessageSeparators.L3 + p.z;

    }

}