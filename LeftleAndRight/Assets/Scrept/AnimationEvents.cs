using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    [Serializable]
    public struct Event
    {
        public string name;
        public UnityEvent @event;
    }

    public Event[] events;

    int i;
    public void Invoke(string name)
    {
        for (i = 0; i < events.Length; i++)
        {
            if (events[i].name == name)
            {
                events[i].@event?.Invoke();
            }
        }
    }
}
