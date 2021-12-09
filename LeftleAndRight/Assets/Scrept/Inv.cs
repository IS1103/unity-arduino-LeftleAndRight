using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Inv : MonoBehaviour
{
    public Text totalScore;
    public Item[] items;
    public UnityEvent unityEvent;
    void Start()
    {

    }

    public void DisableItems() {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].gameObject.SetActive(false);
        }
    }

    public void SetCounter(int[][] boughtItems) {
        StartCoroutine(PlayCounterAni(boughtItems));
    }

    private IEnumerator PlayCounterAni(int[][] boughtItems)
    {

        for (int i = 0; i < boughtItems.Length; i++)
        {
            if (boughtItems[i][0] > 0)
            {
                items[i].GetComponentInChildren<Text>().text = "X " + boughtItems[i][0].ToString();
                items[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
            }
        }

        totalScore.text = GameMain.score.ToString();
        yield return new WaitForSeconds(1);
        unityEvent?.Invoke();
    }
}
