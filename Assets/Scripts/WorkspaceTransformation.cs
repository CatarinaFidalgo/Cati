using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkspaceTransformation : MonoBehaviour
{


    private bool inWorkspace;
    public BoxCollider volumeCollider;
    [Space(8)]
    public Transform warpedSpace;
    [Space(8)]
    public Evaluation evaluation;
    [Space(8)]
    public Transform remoteAvatar;
    [Space(8)]
    public Transform remoteHMD;
    public Transform warpedHMD;
    [Space(8)]
    public Transform remoteLeft;
    public Transform warpedHandLeft;
    [Space(8)]
    public Transform remoteRight;
    public Transform warpedHandRight;
    [Space(8)]
    public Transform remoteFingertipRight;
    public Transform warpedFingertipRight;
    [Space(8)]
    public Transform remoteFingertipLeft;
    public Transform warpedFingertipLeft;
    [Space(8)]
    public Transform remoteFingertipLeftRig;
    public Transform remoteFingertipRightRig;
    [Space(8)]
    public Transform targetLeft; // targets are the tips of the local avatar hands calculated from the remote hands
    public Transform targetRight;
   
       
    [Header("Right Arm Rig")]

    [Header("Stretch the Arms")]

    public Transform shoulderRight;
    public Transform elbowRight;
    public Transform wristRight;
    public Vector3 differenceAvatarWristToWarpedCRight;
    [Header("Left Arm Rig")]
   
    public Transform shoulderLeft;
    public Transform elbowLeft;
    public Transform wristLeft;
    public Vector3 differenceAvatarWristToWarpedCLeft;


    private Vector3 transformLeftHandVector;
    private Vector3 transformRightHandVector;

    // reset values
    private Transform warpedHMD_reset;
    private Transform warpedHandRight_reset;
    private Transform warpedHandLeft_reset;
    private Transform warpedFingertipRight_reset;
    private Transform warpedFingertipLeft_reset;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    private void Start()
    {
        
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
            warpedFingertipRight.localPosition = remoteFingertipLeft.localPosition;
            warpedFingertipLeft.localPosition = remoteFingertipRight.localPosition;            

            
            warpedHMD.localRotation = remoteHMD.localRotation;
            warpedHandRight.localRotation = remoteLeft.localRotation;
            warpedHandLeft.localRotation = remoteRight.localRotation;           
            warpedFingertipRight.localRotation = remoteFingertipLeft.localRotation;            
            warpedFingertipLeft.localRotation = remoteFingertipRight.localRotation;

            //Calculate the local tip from the remote tip coordinates

            targetRight.localPosition = new Vector3(-warpedFingertipLeft.localPosition.x, warpedFingertipLeft.localPosition.y, -warpedFingertipLeft.localPosition.z); //local right fingertip from the left warped hand
            targetLeft.localPosition = new Vector3(-warpedFingertipRight.localPosition.x, warpedFingertipRight.localPosition.y, -warpedFingertipRight.localPosition.z); //local left fingertip from the right warped hand

            //transformHandVector is the transform vector from the remote to the local tip

            transformRightHandVector = targetLeft.position - warpedFingertipRight.position;
            transformLeftHandVector = targetRight.position - warpedFingertipLeft.position;


            //1st Update of the warped hands position with the transform vector 1

            warpedHandLeft.position = warpedHandLeft.position + transformLeftHandVector; 
            warpedHandRight.position = warpedHandRight.position + transformRightHandVector;

            /* //Arm Stretching

             differenceAvatarWristToWarpedCLeft = new Vector3 (0, 0.1f, 0.1f);


             if (!Input.GetKeyDown(KeyCode.Space)) //!OVRInput.GetDown(OVRInput.Button.One)
             {
                 differenceAvatarWristToWarpedCLeft = Vector3.zero;
                 differenceAvatarWristToWarpedCRight = Vector3.zero;
                 //Debug.Log("PRESSED NOTHING");

             }            

             if (Input.GetKeyDown(KeyCode.Space)) //Press a button to let the system know it is allright to start taking measures
             {
                 differenceAvatarWristToWarpedCLeft = (warpedHandLeft.position - wristLeft.position);
                 differenceAvatarWristToWarpedCRight = (warpedHandRight.position - wristRight.position);
                 Debug.Log("PRESSED A");
             }

             if ((warpedHandLeft.position - wristLeft.position).magnitude > differenceAvatarWristToWarpedCLeft.magnitude)
             {
                 differenceAvatarWristToWarpedCLeft = (warpedHandLeft.position - wristLeft.position);
                 Debug.Log("Increased the stretching on the Left");
             }

             if ((warpedHandRight.position - wristRight.position).magnitude > differenceAvatarWristToWarpedCRight.magnitude)
             {
                 differenceAvatarWristToWarpedCRight = (warpedHandRight.position - wristRight.position);
                 Debug.Log("Increased the stretching on the Right");
             }

             // Debug.DrawLine(warpedHandLeft.position, warpedHandLeft.position + differenceAvatarWristToWarpedCLeft, Color.green);


             shoulderLeft.localPosition = shoulderLeft.localPosition + new Vector3 (0, 0.5f*differenceAvatarWristToWarpedCLeft.z, 0);    //Porque só podemos mexer em z para alongar       
             elbowLeft.localPosition = elbowLeft.localPosition + new Vector3(0, 0.5f * differenceAvatarWristToWarpedCLeft.z, 0);

             //shoulderRight.localPosition = shoulderRight.localPosition + new Vector3(0, 0.5f * differenceAvatarWristToWarpedCRight.z, 0);            
            // elbowRight.localPosition = elbowRight.localPosition + new Vector3(0, 0.5f * differenceAvatarWristToWarpedCRight.z, 0);

             //2nd Update of the warped hands position with the transform vector 2

             warpedHandLeft.position = warpedHandLeft.position + differenceAvatarWristToWarpedCLeft;
             //warpedHandRight.position = warpedHandRight.position + differenceAvatarWristToWarpedCRight;
             */
        }




        if (evaluation.condition == ConditionType.Veridical || evaluation.condition == ConditionType.SideToSide)
        {
            //Warped hands are the same as the remote hands received through the network

            warpedHMD.localPosition = remoteHMD.localPosition;
            warpedHMD.localRotation = remoteHMD.localRotation;

            warpedHandLeft.localPosition = remoteLeft.localPosition; 
            warpedHandLeft.localRotation = remoteLeft.localRotation;

            warpedHandRight.localPosition = remoteRight.localPosition; 
            warpedHandRight.localRotation = remoteRight.localRotation;         
            
        }
    }
    
}
