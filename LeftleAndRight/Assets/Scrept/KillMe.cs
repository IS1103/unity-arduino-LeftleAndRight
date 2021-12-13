using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMe : MonoBehaviour
{
    public GameObject go;
    void Start()
    {
        
    }

    Vector3 v3_1 = new Vector3(-0.01f, 0.01f, 1);
    Vector3 v3_2 = new Vector3(0.01f, 0.01f, 1);
    void Update()
    {
        if (go.transform.localScale.x <0)
        {
            transform.localScale = v3_1;
        }
        else
        {
            transform.localScale = v3_2;
        }
    }

    public void KillMyself() {
        ActorController.ScoreTXTEffectPool.Reales(this.gameObject);
    }
}
