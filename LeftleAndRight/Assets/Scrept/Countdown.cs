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
    public UnityEvent startEvent, endEvent,onOverH;
    public Quaternion vector;

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
        overH = false;
        start = true;
        startEvent?.Invoke();
    }

    public void GameOver()
    {
        start = false;
        endEvent?.Invoke();
    }

    bool overH;
    void Update()
    {
        if (start)
        {
            temp += Time.deltaTime;
            if (temp > 1)
            {
                vector = Quaternion.Euler(0, 0, (360 / target) * -(sec + temp * 2));
                temp = 0;
                pointer.parent.localRotation = vector;

                sec++;
                if (sec >= target)
                {
                    pointer.parent.localRotation = new Quaternion(0, 0, 0, 0);
                    GameMain.start = false;
                    temp = 0;
                    GameOver();
                }

                if (sec > (target / 3 * 2) && !overH)
                {
                    overH = true;
                    GameMain.BgAudio.pitch = 1.09f;
                    onOverH?.Invoke();
                }
            }
        }
    }
}
