using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Match the VR target to the headset etc


public class VRRig2 : MonoBehaviour
{
    [Header("Head")]   
    public Transform headVrTarget;
    public Transform headRigTarget;

    [Space(15)]
    [Header("Right Hand")] 
    public Transform rightHandVrTarget;
    public Transform rightHandRigTarget;

    [Space(15)]
    [Header("Left Hand")]
    public Transform leftHandVrTarget;
    public Transform leftHandRigTarget;

    [Space(15)]
    [Header("Head Constraint")]   
    public Transform headConstraint;

    [Space(15)]
    [Header("Offsets")]    
    public Vector3 headPositionOffset;   
    public Vector3 headBodyOffset;

    [Space(15)]
    public bool remote = false;
    public Evaluation evaluation;

    [Space(15)]
    public bool active;

    public float ti;

    void Start()
    {
        //initial diference in position between the head and the body
        headBodyOffset = transform.position - headConstraint.position;
    }

    void Update()
    {
        ti = 1.0f;

        //update da posiçao do avatar
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.ProjectOnPlane(headVrTarget.forward, Vector3.up).normalized;

        //Match entre o target no esqueleto (RigTarget) e os dados vindos dos controladores (VrTarget)

        headRigTarget.position = headVrTarget.TransformPoint(headPositionOffset);
        headRigTarget.rotation = headVrTarget.rotation;

        rightHandRigTarget.position = rightHandVrTarget.position;
        rightHandRigTarget.LookAt(rightHandRigTarget.position - rightHandVrTarget.right, rightHandVrTarget.forward);

        leftHandRigTarget.position = leftHandVrTarget.position;
        leftHandRigTarget.LookAt(leftHandRigTarget.position - (-leftHandVrTarget.right), leftHandVrTarget.forward);
    }

}

