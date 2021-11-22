using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class ArduinoConnector : MonoBehaviour
{
    public int baud;
    public string portName;

    public Action<string> receiveEvent, writeEvent;

    private SerialPort arduinoStream;
    private Thread readThread;
    private Queue<string> queue;

    void Start()
    {
        if (!string.IsNullOrEmpty(portName) && baud != 0)
        {
            writeEvent = WriteEvent;
            arduinoStream = new SerialPort(portName, baud);
            queue = new Queue<string>();
            try
            {
                arduinoStream.Open();
                readThread = new Thread(new ThreadStart(ArduinoRead)); //實例化執行緒與指派呼叫函式
                readThread.Start(); //開啟執行緒
                Debug.Log("SerialPort開啟連接");
            }
            catch (Exception ex)
            {
                Debug.Log("SerialPort連接失敗:" + ex.Message);
            }
        }
    }

    private void ArduinoRead()
    {
        while (arduinoStream.IsOpen)
        {
            try
            {
                string readMessage = arduinoStream.ReadLine(); // 讀取SerialPort資料並裝入readMessage
                queue.Enqueue(readMessage);
                //Debug.Log(readMessage);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("取得資料失敗:" + ex.Message);
            }
        }
    }

    private void Update()
    {
        if (queue != null && queue.Count > 0)
        {
            receiveEvent?.Invoke(queue.Dequeue());
        }
    }

    private void WriteEvent(string msg)
    {
        Debug.Log(msg);
        try
        {
            arduinoStream.Write(msg);
        }
        catch (Exception ex)
        {
            Debug.Log("傳送資料給 arduino 失敗:" + ex.Message);
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