using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public enum ActorState
    {
        Idle,
        run
    }

    public AnimationHandler animationHandler;
    public GOPoolMono scoreEffectPool;

    public float runSpeed = 7f;
    public Vector3 input = default(Vector3);
    public ActorState previousState, currentState;
    public Vector3 oriPos;

    float dt;

    private void Start()
    {
        currentState = ActorState.Idle;
        previousState = currentState;
        animationHandler.PlayAni("idle");
    }

    public void StartGame() {

        gameObject.SetActive(true);
        oriPos = transform.position;
        transform.localPosition = oriPos;
    }

    public void GameOver() {
        animationHandler.PlayAni("idle");
    }

    public void StopGame() { 
    
    }

    void Update()
    {
        if (GameMain.start)
        {
            if (Input.GetKey("left") || GameMain.readMessage == "L")
            {
                input.x = -1;
                animationHandler.SetFlip(input.x);
                currentState = ActorState.run;
                animationHandler.SetFlip(-1);
            }
            else if (Input.GetKey("right") || GameMain.readMessage == "R")
            {
                input.x = 1;
                animationHandler.SetFlip(input.x);
                currentState = ActorState.run;
                animationHandler.SetFlip(1);
            }
            else
            {
                input.x = 0;
                currentState = ActorState.Idle;
            }

            if (previousState != currentState)
                ChangeState();
            previousState = currentState;

            if (transform.localPosition.x > 8 && input.x > 0
                || transform.localPosition.x < -8 && input.x < 0)
                return;

            dt = Time.deltaTime;
            transform.Translate(input * dt * runSpeed);
        }
    }

    internal void Open()
    {
        gameObject.SetActive(false);
    }

    private void ChangeState()
    {
        switch (currentState)
        {
            case ActorState.Idle:
                animationHandler.PlayAni("idle");
                break;
            case ActorState.run:
                animationHandler.PlayAni("run");
                break;
            default:
                break;
        }
    }

    internal void ShowGetScore()
    {
        var _ = scoreEffectPool.Spawn();
        _.transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameMain.OnItemTriggerActor?.Invoke(collision.gameObject);
    }
}
