using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Transform actorSpine;
    public SkeletonAnimation skeletonAnimation;

    void Start()
    {

    }

    public void PlayAni(string stateName, int start = 0)
    {
        skeletonAnimation.AnimationState.SetAnimation(start, stateName, true);
    }

    public Vector3 vecLef = new Vector3(-1, 1, 1), vecRig = new Vector3(1, 1, 1);
    public void SetFlip(float horizontal)
    {
        actorSpine.transform.localScale = horizontal > 0 ? vecRig : vecLef;
    }
}