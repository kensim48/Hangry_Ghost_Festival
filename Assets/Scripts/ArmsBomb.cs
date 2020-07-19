using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsBomb : ArmsClass
{

    private float amplifierBoosterMultiplier = 0.5f;
    private bool isMoving;
    Animator m_Animator;
    public GameObject[] indicators;
    private float chargeLevel = 0;
    public float chargeRate = 3f;
    public float maxCharge = 120;
    private int chargeState = 0;
    public Rigidbody2D bombSingle;
    public Transform[] spawnPoints;
    public float bombThrust = 800f;
    void Awake()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        for (int i = 3; i > 0; i--)
        {
            indicators[i - 1].SetActive(false);
        }
    }
    public override void Attack()
    {
        // Vector2 boosterForce = (Vector2)(transform.rotation * Quaternion.Euler(0, 0, -90) * Vector2.right);
        // rb.AddForce(boosterForce * amplifierBoosterMultiplier);
        isMoving = true;
        if (chargeLevel <= maxCharge) chargeLevel += chargeRate;
        chargeState = (int)Mathf.Floor(chargeLevel / (maxCharge / 3));
        print(chargeState);
        if (chargeState > 0) indicators[chargeState - 1].SetActive(true);
    }

    public override void Move()
    {
        Vector2 boosterForce = (Vector2)(transform.rotation * Quaternion.Euler(0, 0, -90) * Vector2.right);
        rb.AddForce(boosterForce * amplifierBoosterMultiplier);
        m_Animator.ResetTrigger("moveOff");
        m_Animator.SetTrigger("moveOn");

    }

    public void FixedUpdate()
    {
        if (!isMoving)
        {
            m_Animator.ResetTrigger("moveOn");
            m_Animator.SetTrigger("moveOff");
            for (; chargeState > 0; chargeState--)
            {
                indicators[chargeState - 1].SetActive(false);
                Rigidbody2D bomb = Instantiate(bombSingle, spawnPoints[chargeState - 1].position, Quaternion.identity) as Rigidbody2D;
                bomb.AddForce(spawnPoints[chargeState - 1].up * bombThrust);
            }
            chargeLevel = 0;
        }
        isMoving = false;
    }


}
