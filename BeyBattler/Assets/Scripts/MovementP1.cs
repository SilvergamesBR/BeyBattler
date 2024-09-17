using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class MovementP1 : MonoBehaviour
{
    
    public Rigidbody2D MyRigidBody;
    private float angVel;
    private bool Disable;
    private float Timer;
    private bool Loser;
    private bool paused;
    public GameObject PauseScrnBttns;
    public Text Text;
    public Text StrtText;
    public Camera mainCmr;
    private PostProcessVolume ppVol;
    private AudioSource AudSrc;
    private AudioSource AudMsc;
    public AudioClip LetItRipSnd;
    private bool GameStart;
    private float StrtTimer;

    // Start is called before the first frame update
    void Start()
    {
        angVel = 30;
        Disable = false;
        Loser = false;
        paused = false;
        ppVol = mainCmr.GetComponent<PostProcessVolume>();
        ppVol.enabled = false;
        AudMsc = mainCmr.GetComponent<AudioSource>();
        AudSrc = gameObject.GetComponent<AudioSource>();
        AudSrc.clip = LetItRipSnd;
        AudSrc.Play();
        StrtTimer = 9.8f;
        GameStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStart == true) {
            if (StrtTimer > 0)
            {
                StrtTimer -= Time.unscaledDeltaTime;
                paused = true;
                if (StrtTimer <= 8.7 && StrtTimer > 7.7)
                {
                    StrtText.text = "Ready...";
                }
                else if (StrtTimer <= 7.7 && StrtTimer > 5.7)
                {
                    StrtText.text = "Set...";
                }
                else if (StrtTimer <= 5.7 && StrtTimer > 4.6)
                {
                    StrtText.text = "3...";
                }
                else if (StrtTimer <= 4.6 && StrtTimer > 3.6)
                {
                    StrtText.text = "2...";
                }
                else if (StrtTimer <= 3.6 && StrtTimer > 2.6)
                {
                    StrtText.text = "1...";
                }
                else if (StrtTimer <= 2.6 && StrtTimer > 0)
                {
                    StrtText.text = "Let it Rip !";
                }
            }
            else
            {
                StrtText.text = "";
                StrtText.enabled = false;
                GameStart = false;
                paused = false;
                Disable = false;
                MyRigidBody.AddForce(Vector2.left * UnityEngine.Random.Range(500, 800));
                AudMsc.Play();
            }
        }
        if (GameStart == false)
        {
            MyRigidBody.SetRotation(MyRigidBody.rotation + angVel);
        }
        var vectCenter = Vector2.zero - MyRigidBody.position;
        if (Disable == false && paused == false)
        {
            if (Input.GetKey(KeyCode.W) && MyRigidBody.velocity.y <= 10)
            {
                MyRigidBody.AddForce(Vector2.up * 7);
            }
            if (Input.GetKey(KeyCode.S) && MyRigidBody.velocity.y >= -10)
            {
                MyRigidBody.AddForce(Vector2.down * 7);
            }
            if (Input.GetKey(KeyCode.A) && MyRigidBody.velocity.x >= -10)
            {
                MyRigidBody.AddForce(Vector2.left * 7);
            }
            if (Input.GetKey(KeyCode.D) && MyRigidBody.velocity.x <= 10)
            {
                MyRigidBody.AddForce(Vector2.right * 7);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            Time.timeScale = 0;
            paused = true;
            PauseScrnBttns.gameObject.SetActive(true);
            Text.text = "Paused";
            ppVol.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            Time.timeScale = 1;
            paused = false;
            Text.text = "";
            PauseScrnBttns.gameObject.SetActive(false);
            ppVol.enabled = false;
        }
        if (paused == false)
        {
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

    public bool GetGameStart()
    {
        return GameStart;
    }

    public void SetAngVel(float newAngVel)
    {
        if(newAngVel >= 0 && Loser == false)
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

    public bool GetPaused()
    {
        return paused;
    }
    public void gameFinished()
    {
        paused = true;
        Time.timeScale = 0;
    }
    public void gameRestarting()
    {
        paused = false;
        Time.timeScale = 1;
    }
    public void tempDisable(float setTimer)
    {
        Disable = true;
        Timer = setTimer;
    }
}