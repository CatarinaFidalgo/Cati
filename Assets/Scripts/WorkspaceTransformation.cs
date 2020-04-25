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

    //values for internal use
    private Vector3 transformLeftHandVector;
    private Vector3 transformRightHandVector;
    private float offset;
    private float lerpRatio;

    private void Start()
    {
        offset = 0.60f;
        lerpRatio = 1.0f;
    }

    void Update()
    {

        inWorkspace = volumeCollider.bounds.Contains(remoteFingertipRight.position) || volumeCollider.bounds.Contains(remoteFingertipLeft.position);


        if (evaluation.condition == ConditionType.Approach)
        {
            
            if (true)
            {             
                    
                warpedSpace.localScale = new Vector3(-1, 1, 1);

                //Mirror hands and put them in the right place            

                // public static Vector3 Lerp(Vector3 a, Vector3 b, float t);

                //warpedHMD.localPosition = Vector3.Lerp(remoteHMD.localPosition, new Vector3(remoteHMD.localPosition.x, remoteHMD.localPosition.y, -remoteHMD.localPosition.z - offset), lerpRatio);

                warpedHMD.localPosition = new Vector3(remoteHMD.localPosition.x, remoteHMD.localPosition.y, -remoteHMD.localPosition.z - offset);
                //warpedHMD.localPosition = remoteHMD.localPosition;
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

                warpedHandLeft.position += transformLeftHandVector;
                warpedHandRight.position += transformRightHandVector;
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

