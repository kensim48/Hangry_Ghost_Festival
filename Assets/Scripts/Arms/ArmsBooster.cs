using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsBooster : ArmsClass
{

    private int amplifierBoosterMultiplier = 2;
    public GameObject boosterTrail;
    private bool isMoving;
    public Animator animator;

    public override void Attack()
    {
        // Vector2 boosterForce = (Vector2)(transform.rotation * Quaternion.Euler(0, 0, -90) * Vector2.right);
        // rb.AddForce(boosterForce * amplifierBoosterMultiplier);
        isMoving = true;
        animator.SetBool("attacking", true);
    }

    public override void Move()
    {
        Vector2 boosterForce = (Vector2)(transform.rotation * Quaternion.Euler(0, 0, -90) * Vector2.right);
        rb.AddForce(boosterForce * amplifierBoosterMultiplier);

    }

    public void FixedUpdate()
    {
        if (!isMoving)
        {
            animator.SetBool("attacking", false);
        }
        boosterTrail.SetActive(isMoving);
        isMoving = false;

    }


}
