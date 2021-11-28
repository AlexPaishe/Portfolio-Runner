using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRagdoll : MonoBehaviour
{
    public SphereCollider Head;
    public CapsuleCollider LeftArm;
    public CapsuleCollider LeftElbow;
    public CapsuleCollider LeftKnee;
    public CapsuleCollider LeftFoot;
    public CapsuleCollider RightArm;
    public CapsuleCollider RightElbow;
    public CapsuleCollider RightKnee;
    public CapsuleCollider RightFoot;
    public BoxCollider Pelvis;
    public BoxCollider Spine;
    public PlayerController Player;

    void Start()
    {
        TriggerOn();
    }

    void Update()
    {
        if(Player.go == false)
        {
            TriggerOff();
        }
    }

    private void TriggerOff()
    {
       Head.isTrigger = false;
       LeftArm.isTrigger = false;
       LeftElbow.isTrigger = false;
       LeftKnee.isTrigger = false;
       LeftFoot.isTrigger = false;
       RightArm.isTrigger = false;
       RightElbow.isTrigger = false;
       RightKnee.isTrigger = false;
       RightFoot.isTrigger = false;
       Spine.isTrigger = false;
       Pelvis.isTrigger = false;
    }

    private void TriggerOn()
    {
        Head.isTrigger = true;
        LeftArm.isTrigger = true;
        LeftElbow.isTrigger = true;
        LeftKnee.isTrigger = true;
        LeftFoot.isTrigger = true;
        RightArm.isTrigger = true;
        RightElbow.isTrigger = true;
        RightKnee.isTrigger = true;
        RightFoot.isTrigger = true;
        Spine.isTrigger = true;
        Pelvis.isTrigger = true;
    }
}
