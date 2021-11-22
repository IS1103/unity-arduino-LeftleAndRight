using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject startPanel;
    void Start()
    {
        
    }

    public void Open() 
    {
        gameObject.SetActive(true);
    }

    internal void StartGame()
    {
        startPanel.SetActive(false);
    }
}
