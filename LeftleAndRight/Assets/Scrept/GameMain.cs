using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameMain : MonoBehaviour
{
    public static Action<GameObject> OnItemDropOnFloor;
    public static Action<GameObject> OnItemTriggerActor;
    public static GOPoolMono dropToFloorEffectPool;
    public static string readMessage;
    public static bool start;

    public int targetSec;
    public float actorSpeed;
    public int score;
    public Item[] items;
    public Transform[] locates;
    public Transform dropMoneyLocate;
    public GameObject getItemEffect;

    public ActorController actorController;
    public GameUI gameUI;
    public Countdown countdown;
    public ArduinoConnector arduinoConnector;

    GameObject itemTemp;
    float dropDelay;

    void Start()
    {
        start = false;
        gameUI.Open();
        actorController.Open();

        dropToFloorEffectPool = GetComponent<GOPoolMono>();
        arduinoConnector.receiveEvent = ArduinoReceive;
        OnItemDropOnFloor = this.OnItemDropOnFloorMethod;
        OnItemTriggerActor = this.OnItemTriggerActorEvent;
        countdown.onArrivalTarget = this.OnGameOver;
        countdown.target = targetSec;
    }

    private void OnGameOver()
    {
        start = false;
        actorController.GameOver();
        countdown.GameOver();
    }

    private void ArduinoReceive(string msg)
    {
        string[] sArray = msg.Split('/');
        if (sArray.Length == 2)
        {
            //Debug.Log(sArray[1] + "----->" + sArray[0]);
            readMessage = sArray[0];
            actorSpeed = int.Parse(sArray[1]) * 0.1f;
            actorController.runSpeed = actorSpeed;
        }
    }

    async void OnItemDropOnFloorMethod(GameObject obj)
    {
        var go = dropToFloorEffectPool.Spawn();
        go.transform.position = obj.transform.position;
        go.transform.position += new Vector3(0, -1.2f);
        await Task.Delay(150);
        Destroy(obj);
    }

    void OnItemTriggerActorEvent(GameObject obj)
    {
        if (start)
        {
            score += obj.gameObject.GetComponent<Item>().score;
            actorController.ShowGetScore();
            Destroy(obj);
        }
    }

    [ContextMenu("PressStart")]
    public void PressStart()
    {
        score = 0;

        gameUI.StartGame();
        actorController.StartGame();

        countdown.Restart();

        dropDelay = 0;
        start = true;
    }

    float temp;
    private void Update()
    {
        if (start)
        {
            temp += Time.deltaTime;
            if (temp >= dropDelay)
            {
                itemTemp = Instantiate(
                    items[Random.Range(0, items.Length)].gameObject,
                    locates[Random.Range(0, locates.Length)].position,
                    Quaternion.identity);
                itemTemp.SetActive(true);
                dropDelay = Random.Range(1f, 1.5f);
                temp = 0;
            }
        }
    }

    void Restart()
    {
        
    }
}