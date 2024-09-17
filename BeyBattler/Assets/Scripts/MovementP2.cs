using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using static UnityEngine.GraphicsBuffer;

public class MovementP2: MonoBehaviour
{
    public Rigidbody2D MyRigidBody;
    private float angVel;
    private bool Disable;
    private float Timer;
    private bool Loser;
    private GameObject P1;
    private bool GameStart;
    private bool paused;
    private bool StrtFlag;

    // Start is called before the first frame update
    void Start()
    {
        angVel = 30;
        Disable = false;
        Loser = false;
        paused = false;
        StrtFlag = false;
        P1 = GameObject.Find("Player1");
    }

    // Update is called once per frame
    void Update()
    {
        paused = P1.GetComponent<MovementP1>().GetPaused();
        if (StrtFlag == false)
        {
            GameStart = P1.GetComponent<MovementP1>().GetGameStart();
            if (GameStart == false)
            {
                MyRigidBody.AddForce(Vector2.right * UnityEngine.Random.Range(500,800));
                StrtFlag = true;
            }
        }
        var vectCenter = Vector2.zero - MyRigidBody.position;
        if (Disable == false && paused == false)
        {
            if (Input.GetKey(KeyCode.Keypad8) && MyRigidBody.velocity.y <= 10)
            {
                MyRigidBody.AddForce(Vector2.up *7);
            }
            if (Input.GetKey(KeyCode.Keypad5) && MyRigidBody.velocity.y >= -10)
            {
                MyRigidBody.AddForce(Vector2.down *7);
            }
            if (Input.GetKey(KeyCode.Keypad4) && MyRigidBody.velocity.x >= -10)
            {
                MyRigidBody.AddForce(Vector2.left * 7);
            }
            if (Input.GetKey(KeyCode.Keypad6) && MyRigidBody.velocity.x <= 10)
            {
                MyRigidBody.AddForce(Vector2.right * 7);
            }
        }
        if(paused == false && GameStart == false)
        {
            MyRigidBody.SetRotation(MyRigidBody.rotation + angVel);
            MyRigidBody.AddForce(vectCenter * 0.5f);
        }    
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else if (Timer <= 0)
        {
            Disable = false;
        }
        if(Loser == true)
        {
            Disable = true;
        }
        if (Loser == false && angVel > 0)
        {
            if (paused == false)
            {
                angVel -= 0.001f;
            }
        }
        else
        {
            angVel = 0;
            Loser = true;
        }
    }

    public float GetAngVel()
    {
        return angVel;
    }

    public void SetAngVel(float newAngVel)
    {
        if (newAngVel >= 0 && Loser == false)
        {
            angVel = newAngVel;
        }
        else
        {
            angVel = 0;
            Loser = true;
        }
    }

    public bool GetLoser()
    {
        return Loser;
    }
    public void tempDisable(float setTimer)
    { 
        Disable = true;
        Timer = setTimer;
    }
}