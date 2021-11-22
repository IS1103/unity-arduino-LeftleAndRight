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
                readThread = new Thread(new ThreadStart(ArduinoRead)); //��Ҥư�����P�����I�s�禡
                readThread.Start(); //�}�Ұ����
                Debug.Log("SerialPort�}�ҳs��");
            }
            catch (Exception ex)
            {
                Debug.Log("SerialPort�s������:" + ex.Message);
            }
        }
    }

    private void ArduinoRead()
    {
        while (arduinoStream.IsOpen)
        {
            try
            {
                string readMessage = arduinoStream.ReadLine(); // Ū��SerialPort��ƨøˤJreadMessage
                queue.Enqueue(readMessage);
                //Debug.Log(readMessage);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("���o��ƥ���:" + ex.Message);
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
            Debug.Log("�ǰe��Ƶ� arduino ����:" + ex.Message);
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