using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTarget : MonoBehaviour
{
    public Transform remoteWrist;
    public Transform localTip;
    public Transform remoteTip;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = remoteWrist.localPosition;
        //transform.rotation = remoteWrist.localRotation;
        //transform.LookAt(tip, );

        
        offset = localTip.position - remoteTip.position;
        Debug.Log(localTip.position);
        Debug.Log(remoteTip.position);
        Debug.Log(offset);


        //transform.position = remoteWrist.localPosition + offset;
    }
}
