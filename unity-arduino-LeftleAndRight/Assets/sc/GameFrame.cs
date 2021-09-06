using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFrame : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject ui;
    public Text title, content;
    public GameObject panel;
    public Text num;
    public float speed;
    public GameObject actor;
    static public Action onTriggerObstacle, onTriggerFinish;
    public AudioClip start,win,lose;
    void Start()
    {
        onTriggerObstacle = OnTriggerObstacle;
        onTriggerFinish = OnTriggerFinish;
    }

    private void OnTriggerFinish()
    {
        Debug.Log("贏了");

        ui.SetActive(true);
        title.text = "你贏了！";
        content.text = "呀呼！！";
        audioSource.Stop();
    }

    private void OnTriggerObstacle()
    {
        Debug.Log("撞牆");
        playing = false;

        ui.SetActive(true);
        title.text = "你撞牆了！得愣！";
        content.text = "總共花了：" + num.text+" 秒";
        audioSource.Stop();
    }

    int sec=0;
    static public bool playing = false;
    private void Update()
    {
        if (playing)
        {
            DropPanel();
        }
    }

    public void ClickStrat() {
        sec = 0;
        ui.SetActive(false);
        playing = true;
        panel.transform.localPosition = new Vector2(0,29);
        actor.transform.localPosition = new Vector2(0,-19);
        Actor.readMessage = string.Empty;
        
        audioSource.PlayOneShot(start);
    }

    float timeTemp;
    private void DropPanel()
    {
        panel.transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);

        timeTemp += Time.deltaTime;
        if (timeTemp>1)
        {
            sec++;
            timeTemp = 0;
            num.text = sec.ToString();
        }
    }
}
