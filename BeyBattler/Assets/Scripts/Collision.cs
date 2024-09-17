using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public Rigidbody2D MyRigidBody;
    public ParticleSystem pSys;
    private AudioSource AudSrc;
    public AudioClip CollisionSnd;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player2")
        {
            var energy = MyRigidBody.velocity * MyRigidBody.mass;
            var energyTotP1 = Math.Abs(energy[0])+Math.Abs(energy[1]);
            var colRigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
            energy = colRigidbody2D.velocity * colRigidbody2D.mass;
            var energyTotP2 = Math.Abs(energy[0]) + Math.Abs(energy[1]);
            var MoveP1 = gameObject.GetComponent<MovementP1>();
            var MoveP2 = collision.gameObject.GetComponent<MovementP2>();
            var posPart = ((colRigidbody2D.position - MyRigidBody.position)/2)+ MyRigidBody.position;
            pSys.transform.SetPositionAndRotation(posPart,Quaternion.identity);
            pSys.Play();
            AudSrc.clip = CollisionSnd;
            AudSrc.Play();
            MoveP1.tempDisable(UnityEngine.Random.Range(0f, 1f));
            MoveP2.tempDisable(UnityEngine.Random.Range(0f, 1f));
            if (energyTotP1 > energyTotP2)
            {
                var impactVect = (colRigidbody2D.position - MyRigidBody.position) * energyTotP1;
                MoveP2.SetAngVel(MoveP2.GetAngVel()+(energyTotP2 - energyTotP1));
                if(energyTotP1 > 15)
                {
                    colRigidbody2D.AddForce(impactVect * UnityEngine.Random.Range(7f, 16f));
                    MyRigidBody.AddForce(impactVect * -UnityEngine.Random.Range(9f, 18f));
                    pSys.Play();
                    pSys.Play();
                    Debug.Log("Mega porrada !!");
                }
                colRigidbody2D.AddForce(impactVect* UnityEngine.Random.Range(5f, 11f));
                MyRigidBody.AddForce(impactVect* -UnityEngine.Random.Range(7f, 13f));

            }
            else
            {
                var impactVect = (MyRigidBody.position - colRigidbody2D.position) * energyTotP2;
                MoveP1.SetAngVel(MoveP1.GetAngVel()+(energyTotP1-energyTotP2));
                if(energyTotP2 > 15)
                {
                    MyRigidBody.AddForce(impactVect * UnityEngine.Random.Range(7f, 16f));
                    colRigidbody2D.AddForce(impactVect * -UnityEngine.Random.Range(9f, 18f));
                    pSys.Play();
                    pSys.Play();
                    Debug.Log("Mega porrada !!");
                }
                MyRigidBody.AddForce(impactVect*UnityEngine.Random.Range(5f, 11f));
                colRigidbody2D.AddForce(impactVect * -UnityEngine.Random.Range(7f, 13f));
            }


        }
    }
    // Start is called before the first frame update
    void Start()
    {
        AudSrc = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
