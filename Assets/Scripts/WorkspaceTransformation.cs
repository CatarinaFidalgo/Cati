using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkspaceTransformation : MonoBehaviour
{


    public bool inWorkspace;
    public BoxCollider volumeCollider;

    public Transform warpedSpace;

    public Evaluation evaluation;

    public VRRig2 remoteVRRig;
    public Transform remoteAvatar;

    public Transform remoteHMD;
    public Transform warpedHMD;

    public Transform remoteLeft;
    public Transform warpedHandLeft;    
    
    public Transform remoteRight;
    public Transform warpedHandRight;

    public Transform remoteLeftTip;
    public Transform remoteRightTip;
          
    public Transform targetLeft; // targets are the tips of the local avatar hands calculated from the remote hands
    public Transform targetRight;  
                 
    [Space(15)]
    public Vector3 transformLeftHandVector;
    public bool d;

    void Start()
    {
        /*if (evaluation.condition == ConditionType.Approach)
        {

            warpedSpace.localScale = new Vector3(-1, 1, 1);

            //Mirror hands and put them in the right place

            warpedHMD.localPosition = remoteHMD.localPosition;

            warpedHandRight.localPosition = remoteLeft.localPosition;

            warpedHandLeft.localPosition = remoteRight.localPosition;



            warpedHMD.localRotation = remoteHMD.localRotation;

            warpedHandRight.localRotation = remoteLeft.localRotation;

            warpedHandLeft.localRotation = remoteRight.localRotation;

            remoteVRRig.updateRig();

            Debug.Break();

        }*/
    }

    void Update()
    {

        inWorkspace = volumeCollider.bounds.Contains(remoteLeft.position) || volumeCollider.bounds.Contains(remoteRight.position);


        if (evaluation.condition == ConditionType.Approach)
        {
            
            warpedSpace.localScale = new Vector3(-1, 1, 1);

            //Mirror hands and put them in the right place

            warpedHMD.localPosition = remoteHMD.localPosition;

            warpedHandRight.localPosition = remoteLeft.localPosition;

            warpedHandLeft.localPosition = remoteRight.localPosition;



            warpedHMD.localRotation = remoteHMD.localRotation;

            warpedHandRight.localRotation = remoteLeft.localRotation;

            warpedHandLeft.localRotation = remoteRight.localRotation;

            
            Debug.Break();
            remoteVRRig.updateRig();
            Debug.Break();
            

            //Truques Mauricio para calcular a local tip a partir da remota
            Transform parent = remoteLeftTip.parent;
            remoteLeftTip.parent = transform;
            targetLeft.localPosition = new Vector3(remoteLeftTip.localPosition.x, remoteLeftTip.localPosition.y, -remoteLeftTip.localPosition.z);
            remoteLeftTip.parent = parent;

            //transformLeftHandVector is the transform vector from the remote to the local tip
            transformLeftHandVector = targetLeft.position - remoteLeftTip.position;

            Debug.Log(transformLeftHandVector);

            Debug.DrawLine(warpedHandLeft.position, warpedHandLeft.position + transformLeftHandVector, Color.cyan);


            GameObject.Find("Bolinhaaaaa").transform.position = warpedHandLeft.position + transformLeftHandVector;

            Debug.Break();

            warpedHandLeft.position = warpedHandLeft.position + transformLeftHandVector; // <------------------ DANIEL!!!! NAO FUNCIONMAAAA. #touchorando
            Debug.Break();
           

            remoteVRRig.updateRig(); // <----- aha--- tá aqui o fora da lei

            
            Debug.Break();
        }




        if (evaluation.condition == ConditionType.Veridical || evaluation.condition == ConditionType.SideToSide)
        {
            
            warpedHMD.localPosition = remoteHMD.localPosition;
            warpedHMD.localRotation = remoteHMD.localRotation;


            warpedHandLeft.localPosition = remoteLeft.localPosition; 
            warpedHandLeft.localRotation = remoteLeft.localRotation;

            warpedHandRight.localPosition = remoteRight.localPosition; 
            warpedHandRight.localRotation = remoteRight.localRotation;
        }
    }

    void LateUpdate()
    {

    }

    
}
