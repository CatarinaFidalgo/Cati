using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;
using UnityEngine.SceneManagement;

public class WorkspaceTransformation : MonoBehaviour
{
    //private bool inWorkspace;
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
    public Transform targetLeftTip; // targetTips are the tips of the local avatar hands calculated from the remote hands
    public Transform targetRightTip;    
    [Space(8)]
    public Transform pointingHandTip;

    //values for internal use
    private Vector3 transformLeftHandVector;
    private Vector3 transformRightHandVector;
    private float m;
    private float b;
    public bool isPointingRight;
    public bool isPointingLeft;
    private float offset_positionZ;

    private void Start()
    {
        m = 0.823529412f;
        b = -0.26f;
    }

    void Update()
    {           
        //inWorkspace = volumeCollider.bounds.Contains(remoteFingertipRight.position) || volumeCollider.bounds.Contains(remoteFingertipLeft.position);
         
        if (evaluation.condition == ConditionType.Approach)
        {
            //Mirror workspace
            warpedSpace.localScale = new Vector3(-1, 1, 1);

            /*************** Selection of the pointing hand ****************/

            //Assumes the pointing hand is the one that is further ahead
            if (targetRightTip.position.z >= targetLeftTip.position.z) //pointing hand is the RIGHT hand
            {
                pointingHandTip.position = targetRightTip.position;
                pointingHandTip.rotation = targetRightTip.rotation;
                isPointingRight = true;
                isPointingLeft = false;
            }
            else if (targetRightTip.position.z < targetLeftTip.position.z) //pointing hand is the LEFT hand
            {
                pointingHandTip.position = targetLeftTip.position;
                pointingHandTip.rotation = targetLeftTip.rotation;
                isPointingRight = false;
                isPointingLeft = true;
            }           

            /*************** Calculate warped positions and orientations of head and hands ****************/

            //warpedHMD.localPosition = remoteHMD.localPosition;  //Update head position in (*1*)
            warpedHandRight.localPosition = remoteLeft.localPosition;
            warpedHandLeft.localPosition = remoteRight.localPosition;
            warpedFingertipRight.localPosition = remoteFingertipLeft.localPosition;
            warpedFingertipLeft.localPosition = remoteFingertipRight.localPosition;


            warpedHMD.localRotation = remoteHMD.localRotation;
            warpedHandRight.localRotation = remoteLeft.localRotation;
            warpedHandLeft.localRotation = remoteRight.localRotation;
            warpedFingertipRight.localRotation = remoteFingertipLeft.localRotation;
            warpedFingertipLeft.localRotation = remoteFingertipRight.localRotation;

            // (*1*)
            /*****************************    Head position update     **************************/

            // Moves head according to the position of the local hand tip along the equation:
            //           hmd_z = m * (-pointingHandTip.position.z) + b

            warpedHMD.localPosition = new Vector3(remoteHMD.localPosition.x, remoteHMD.localPosition.y, m * (-pointingHandTip.position.z) + b);
            offset_positionZ = remoteHMD.localPosition.z - warpedHMD.localPosition.z;
            //Debug.Log(offset_positionZ);

            /*************** Make remote hands point to the same place as local avatar ****************/
            /**************       (Only the pointing hands suffers this update)     ****************/

            //Calculate the local tips from the remote tip coordinates
            targetRightTip.localPosition = new Vector3(-warpedFingertipLeft.localPosition.x, warpedFingertipLeft.localPosition.y, -warpedFingertipLeft.localPosition.z); //local right fingertip from the left warped hand
            targetLeftTip.localPosition = new Vector3(-warpedFingertipRight.localPosition.x, warpedFingertipRight.localPosition.y, -warpedFingertipRight.localPosition.z); //local left fingertip from the right warped hand

            //transformHandVector is the transform vector from the remote to the local tip
            transformRightHandVector = targetLeftTip.position - warpedFingertipRight.position;
            transformLeftHandVector = targetRightTip.position - warpedFingertipLeft.position;

            //Update of the pointing warped hand position with the transform vector
            if (isPointingRight == true) 
            {
                warpedHandLeft.position += transformLeftHandVector;
                warpedHandRight.position = new Vector3 (warpedHandRight.position.x, warpedHandRight.position.y, warpedHandRight.position.z + offset_positionZ);
            }

            if (isPointingLeft == true)
            {
                warpedHandRight.position += transformRightHandVector;
                warpedHandLeft.position = new Vector3(warpedHandLeft.position.x, warpedHandLeft.position.y, warpedHandLeft.position.z + offset_positionZ);
            }  
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

