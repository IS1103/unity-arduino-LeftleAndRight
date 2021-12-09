using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cashier : MonoBehaviour
{
    public Animator anim;
    public Text score;
    public SkeletonGraphic skeleton;
    void Start()
    {
    }

    public void AddScore(int score)
    {
        anim.Play("buy");
        this.score.text = score.ToString();
    }

    public void PlayAniIsLoop(string stateName) 
    {
        skeleton.AnimationState.SetAnimation(0, stateName, true);
    }

    public void PlayAni(string stateName)
    {
        skeleton.AnimationState.SetAnimation(0, stateName, false);
    }
}
