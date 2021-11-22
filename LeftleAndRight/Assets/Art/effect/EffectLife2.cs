using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EffectLife2 : MonoBehaviour
{
    private ParticleSystem particle;
    void Start()
    {
        particle = this.GetComponent<ParticleSystem>();
        var _ = particle.main;
        _.stopAction = ParticleSystemStopAction.Destroy;
    }
}
