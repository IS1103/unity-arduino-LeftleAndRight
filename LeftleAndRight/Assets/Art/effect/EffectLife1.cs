using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLife1 : MonoBehaviour
{
    private ParticleSystem particle;
    void Start()
    {
        particle = this.GetComponent<ParticleSystem>();
        var _ = particle.main;
        _.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        GameMain.dropToFloorEffectPool.Reales(this.gameObject);
    }
}
