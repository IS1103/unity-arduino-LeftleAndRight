using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GameMain : MonoBehaviour
{
    public static Action<GameObject> OnItemDropOnFloor;
    public static Action<GameObject> OnItemTriggerActor;
    public static GOPoolMono dropToFloorEffectPool;
    public static string readMessage;
    public static bool start;
    public static AudioSource ShortAudio, BgAudio;
    public static int score;

    public int targetSec;
    public float actorSpeed;
    public Item[] items;
    public Transform[] locates;
    public Transform dropMoneyLocate;
    public GameObject getItemEffect;
    public int[][] boughtItems; 

    public ActorController actorController;
    public GameUI gameUI;
    public Countdown countdown;
    public ArduinoConnector arduinoConnector;
    public Cashier cashier;
    public Transform itemParent;
    public Inv inv;
    public UnityEvent OnNoItemInScreen;
    public AudioSource shortAudio, bgAudio;

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

        ShortAudio = shortAudio;
        BgAudio = bgAudio;

        boughtItems = new int[16][];
        for (int i = 0; i < boughtItems.Length; i++)
            boughtItems[i] = new int[1];
    }

    public void OnGameOver()
    {
        start = false;
        actorController.GameOver();
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
            var boughtItem = obj.gameObject.GetComponent<Item>();
            boughtItems[boughtItem.id][0]++;
            score += boughtItem.score;
            cashier.AddScore(score);
            actorController.ShowGetScore();
            Destroy(obj);
        }
    }

    [ContextMenu("PressStart")]
    public void StartGame()
    {
        for (int i = 0; i < boughtItems.Length; i++)
            boughtItems[i][0] = 0;
        score = 0;
        gameUI.StartGame();
        actorController.StartGame();
        countdown.StartGame(targetSec);
        dropDelay = 0;
        start = true;
        inv.totalScore.text = "";
    }

    public void NoItemInScreen() 
    {
        OnNoItemInScreen?.Invoke();
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
                itemTemp.transform.SetParent(itemParent);
                dropDelay = Random.Range(1f, 1.5f);
                temp = 0;
            }
        }
    }

    void Restart()
    {
        
    }

    public void SetCounter() 
    {
        inv.SetCounter(boughtItems);
    }
}