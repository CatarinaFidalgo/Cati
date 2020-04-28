using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;
using UnityEngine.SceneManagement;

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
    public Transform targetLeftTip; // targetTips are the tips of the local avatar hands calculated from the remote hands
    public Transform targetRightTip;
    [Space(8)]
    public Transform rigWristLeft;
    public Transform rigWristRight;
    

    //values for internal use
    private Vector3 transformLeftHandVector;
    private Vector3 transformRightHandVector;
    private Vector3 differenceAvatarWristToWarpedCRight;
    private Vector3 differenceAvatarWristToWarpedCLeft;

    public float offset1;
    private float offset_left;
    public float offset_right;
    public Transform pointingHandTip;
    //private float lerpRatio;
    private float angle_cos;
    private float magnitude_right;
    private float magnitude_left;
    private bool start_offset;
    private float m;
    private float b;

    private void Start()
    {
        offset1 = 0.0f;
        offset_right = 0.0f;
        offset_left = 0.0f;
        start_offset = false;
        m = 0.823529412f;
        b = -0.26f;


    }

    void Update()
    {
        //OVRInput.Update();

        if (Input.GetKeyDown(KeyCode.Space)) //Press a button to let the system know it is allright to start taking measures
        {
            start_offset = true;
            Debug.Log("PRESSED SPACE");
        }

        inWorkspace = volumeCollider.bounds.Contains(remoteFingertipRight.position) || volumeCollider.bounds.Contains(remoteFingertipLeft.position);
        
        if (evaluation.handedness == Handedness.Right_Handed)
        {
            pointingHandTip.position = targetRightTip.position;
            pointingHandTip.rotation = targetRightTip.rotation;
        }
        else if (evaluation.handedness == Handedness.Left_Handed)
        {
            pointingHandTip.position = targetLeftTip.position;
            pointingHandTip.rotation = targetLeftTip.rotation;
        }

        //Debug.Log((float)OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger));
        //Debug.Log((float)OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger));
        //Debug.Log(OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch));
        //Debug.Log(Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger"));

        if (evaluation.condition == ConditionType.Approach)
        {
            warpedSpace.localScale = new Vector3(-1, 1, 1); //Mirror workspace

            /*************** Calculate warped positions of head and hands****************/

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

            /*************** Make remote hands point to the same place as local avatar ****************/
            /**************       (Works only in specific zone: needs more update)     ****************/

            //Calculate the local tips from the remote tip coordinates
            targetRightTip.localPosition = new Vector3(-warpedFingertipLeft.localPosition.x, warpedFingertipLeft.localPosition.y, -warpedFingertipLeft.localPosition.z); //local right fingertip from the left warped hand
            targetLeftTip.localPosition = new Vector3(-warpedFingertipRight.localPosition.x, warpedFingertipRight.localPosition.y, -warpedFingertipRight.localPosition.z); //local left fingertip from the right warped hand

            //transformHandVector is the transform vector from the remote to the local tip
            transformRightHandVector = targetLeftTip.position - warpedFingertipRight.position;
            transformLeftHandVector = targetRightTip.position - warpedFingertipLeft.position;

            //Update of the warped hands position with the transform vector
            warpedHandLeft.position += transformLeftHandVector;
            warpedHandRight.position += transformRightHandVector;

            // (*1*)
            /*****************************    Head position update     **************************/
            if (start_offset == true)
            {
                differenceAvatarWristToWarpedCLeft = warpedHandLeft.position - rigWristLeft.position;
                differenceAvatarWristToWarpedCRight = warpedHandRight.position - rigWristRight.position;

                magnitude_left = differenceAvatarWristToWarpedCLeft.magnitude;
                magnitude_right = differenceAvatarWristToWarpedCRight.magnitude;

                angle_cos = Mathf.Cos(Vector3.Angle(differenceAvatarWristToWarpedCLeft, transform.forward));
                Debug.Log("cos left" + angle_cos);
                if (angle_cos < 0)
                {
                    magnitude_left = -magnitude_left;
                }

                angle_cos = Mathf.Cos(Vector3.Angle(differenceAvatarWristToWarpedCRight, transform.forward));
                Debug.Log("cos right" + angle_cos);
                if (angle_cos < 0)
                {
                    magnitude_right = -magnitude_right;
                }

                offset_left += magnitude_left;
                offset_right += magnitude_right;

                Debug.Log("offset left" + offset_left);
                Debug.Log("offset right" + offset_right);

                warpedHMD.localPosition = remoteHMD.localPosition;
                //**warpedHMD.localPosition = new Vector3(remoteHMD.localPosition.x, remoteHMD.localPosition.y, remoteHMD.localPosition.z + offset_left);
                //warpedHMD.localPosition = new Vector3(remoteHMD.localPosition.x, remoteHMD.localPosition.y, remoteHMD.localPosition.z + offset1);


                //warpedHMD.localPosition = Vector3.Lerp(remoteHMD.localPosition, new Vector3(remoteHMD.localPosition.x, remoteHMD.localPosition.y, -remoteHMD.localPosition.z - offset), lerpRatio);
                //**************warpedHMD.localPosition = new Vector3(remoteHMD.localPosition.x, remoteHMD.localPosition.y, -pointingHandTip.position.z8 + offset_1);
                //warpedHMD.localPosition = new Vector3(remoteHMD.localPosition.x, remoteHMD.localPosition.y, -remoteHMD.localPosition.z - offset);
                //warpedHMD.localPosition = new Vector3(remoteHMD.localPosition.x, remoteHMD.localPosition.y, remoteHMD.localPosition.z - offset_1);


            }

            else if (start_offset == false)
            {
                //warpedHMD.localPosition = remoteHMD.localPosition;
                //warpedHMD.localPosition = new Vector3(remoteHMD.localPosition.x, remoteHMD.localPosition.y, -pointingHandTip.position.z - offset1);
                warpedHMD.localPosition = new Vector3(remoteHMD.localPosition.x, remoteHMD.localPosition.y, m * (-pointingHandTip.position.z) + b);
                Debug.Log(warpedHMD.localPosition.z);
            }


            /*if (!inWorkspace)
            {
                warpedSpace.localScale = new Vector3(-1, 1, 1);

                //Mirror hands and put them in the right place

                //warpedHMD.localPosition = Vector3.Lerp( new Vector3(remoteHMD.localPosition.x, remoteHMD.localPosition.y, -remoteHMD.localPosition.z - offset), remoteHMD.localPosition, lerpRatio);
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
            }*/

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

