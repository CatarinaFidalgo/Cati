using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MessageSeparators
{
    L1 = '#', // sep transforms
    L2 = '/', // sep propertines 
    L3 = ':' // sep values
}


public class SendAvatar : MonoBehaviour
{
    public Transform head;
    public Transform controllerRight;
    public Transform controllerLeft;
    //public Transform OBJECT;
    public Transform fingertipRight;
    public Transform fingertipLeft;


    public int port = 7001;
    private UdpBroadcast _upd;


    public bool SENDING = true; //TODO


    void Start()
    {

        _upd = new UdpBroadcast(port);
    }

    void Update()
    {
        if (!SENDING) return; // TODO 


        if (_upd != null)
        {
            /* Message is:
             * 
             *  head, controllerRight, controllerLeft, fingertipRight, fingertipLeft
             * 
             */
            string msg = "";

            //msg += _getValues(OBJECT) + (char)MessageSeparators.L1;
            msg += _getValues(head) + (char)MessageSeparators.L1;
            msg += _getValues(controllerRight) + (char)MessageSeparators.L1;
            msg += _getValues(controllerLeft) + (char)MessageSeparators.L1;
            msg += _getValues(fingertipRight) + (char)MessageSeparators.L1;
            msg += _getValues(fingertipLeft);

            _upd.send(msg);
        }
    }

    private string _getValues(Transform t)
    {
        string ret = "";

        ret += _positionToString(t.position) + (char)MessageSeparators.L2;
        ret += _rotationToString(t.rotation);

        return ret;
    }

    private string _positionToString(Vector3 p)
    {
        return "" + p.x + (char)MessageSeparators.L3 + p.y + (char)MessageSeparators.L3 + p.z;

    }

    private string _rotationToString(Quaternion r)
    {
        return "" + r.x + (char)MessageSeparators.L3 + r.y + (char)MessageSeparators.L3 + r.z + (char)MessageSeparators.L3 + r.w;
    }

}
