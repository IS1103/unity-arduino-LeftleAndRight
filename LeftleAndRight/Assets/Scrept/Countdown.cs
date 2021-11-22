using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public Transform pointer;
    public Action onArrivalTarget;
    public int target, sec;
    public float temp;

    void Start()
    {
        
    }

    [ContextMenu("Restart")]
    public void Restart() 
    {
        gameObject.SetActive(true);
        sec = 0;
        temp = 0;
        pointer.parent.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void GameOver() 
    {
        //gameObject.SetActive(false);
    }

    void Update()
    {
        if (GameMain.start)
        {
            temp += Time.deltaTime;
            if (temp > 1)
            {
                temp = 0;
                pointer.parent.RotateAround(pointer.parent.position, -Vector3.forward, 360/ target);
                
                if (sec >= target)
                {
                    GameMain.start = false;
                    temp = 0;
                    onArrivalTarget?.Invoke();
                }
                sec++;
            }
        }
    }
}
