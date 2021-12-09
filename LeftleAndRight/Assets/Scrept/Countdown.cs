using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Countdown : MonoBehaviour
{
    public Transform pointer;
    public int target, sec;
    public float temp;
    public bool start;
    public UnityEvent startEvent,endEvent;

    void Start()
    {
        start = false;
    }

    [ContextMenu("Restart")]
    public void StartGame(int target) 
    {
        this.target = target;
        gameObject.SetActive(true);
        sec = 0;
        temp = 0;
        pointer.parent.rotation = Quaternion.Euler(0, 0, 0);
        Play();
    }

    public void Play() 
    {
        start = true;
        startEvent?.Invoke();
    }

    public void GameOver() 
    {
        start = false;
        endEvent?.Invoke();
    }

    void Update()
    {
        if (start)
        {
            temp += Time.deltaTime;
            if (temp > 1)
            {
                temp = 0;
                pointer.parent.RotateAround(pointer.parent.position, -Vector3.forward, 360/ target);
                sec++;
                if (sec >= target)
                {
                    GameMain.start = false;
                    temp = 0;
                    GameOver();
                }
            }
        }
    }
}
