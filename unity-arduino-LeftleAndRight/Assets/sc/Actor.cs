using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class Actor : MonoBehaviour
{
    public float speed;

    private SerialPort arduinoStream;
    public string port;
    private Thread readThread; // 宣告執行緒
    static public string readMessage; //L、R

    void Start()
    {
        if (!string.IsNullOrEmpty(port))
        {
            arduinoStream = new SerialPort(port, 9600);
            try
            {
                arduinoStream.Open(); //開啟SerialPort連線
                readThread = new Thread(new ThreadStart(ArduinoRead)); //實例化執行緒與指派呼叫函式
                readThread.Start(); //開啟執行緒
                Debug.Log("SerialPort開啟連接");
            }
            catch (Exception ex)
            {
                Debug.Log("SerialPort連接失敗");
            }
        }
    }

    private void ArduinoRead()
    {
        while (arduinoStream.IsOpen)
        {
            try
            {
                readMessage = arduinoStream.ReadLine(); // 讀取SerialPort資料並裝入readMessage

                string[] sArray = readMessage.Split('/');

                if (sArray.Length==2)
                {
                    Debug.Log("----->"+sArray[0]);
                    readMessage = sArray[0];
                }

                Debug.Log(readMessage);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }

    void Update()
    {
        if (GameFrame.playing)
        {
            if (Input.GetKey("left") || readMessage == "L") {
                if (transform.localPosition.x >-10)
                {
                    transform.Translate(-1 * speed, 0, 0);
                }
            }
            if (Input.GetKey("right") || readMessage == "R") {
                if (transform.localPosition.x < 10)
                {
                    transform.Translate(1 * speed, 0, 0);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "obstacle")
        {
            GameFrame.onTriggerObstacle?.Invoke();
        }
        else if (other.tag == "end")
        {
            GameFrame.onTriggerFinish?.Invoke();
        }
    }

    void OnApplicationQuit()
    {
        if (arduinoStream != null)
        {
            if (arduinoStream.IsOpen)
            {
                arduinoStream.Close();
            }
        }
    }
}
