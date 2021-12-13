using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckHaveItem : MonoBehaviour
{
    public bool isCheck;
    public UnityEvent events;
    void Start()
    {

    }

    public void StartCheck()
    {
        enabled = true;
        isCheck = true;
    }


    Item[] _;
    void Update()
    {
        if (isCheck)
        {
            _ = GetComponentsInChildren<Item>();
            if (_.Length < 1)
            {
                isCheck = false;
                events?.Invoke();
                enabled = false;
                _ = null;
                GameMain.BgAudio.pitch = 1f;
            }
        }
    }
}