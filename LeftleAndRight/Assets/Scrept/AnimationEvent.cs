using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    public UnityEvent events;
    void Start()
    {
        
    }

    public void PlayEvent() {
        events?.Invoke();
    }
}
