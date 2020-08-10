using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsShooter : ArmsClass
{

    public int amplifierBoosterMultiplier = 2;
    public int projectileThrust = 800;
    private bool isMoving;
    private int chargeupCounter = 0;
    public Rigidbody2D shooterProjectile;
    public Transform projectileSpawnPosition;
    public Animator animator;
    public override void Attack()
    {
        isMoving = true;
    }

    public override void Move()
    {
        animator.SetBool("attacking", false);
        if (chargeupCounter >= 100)
        {
            animator.SetBool("attacking", true);
            Vector2 boosterForce = (Vector2)(transform.rotation * Quaternion.Euler(0, 0, -90) * Vector2.right);
            rb.AddForce(boosterForce * amplifierBoosterMultiplier);
            Rigidbody2D projectile = Instantiate(shooterProjectile, projectileSpawnPosition.position, projectileSpawnPosition.rotation) as Rigidbody2D;
            projectile.AddForce(projectileSpawnPosition.up * projectileThrust);
            chargeupCounter = 0;

        }
    }

    public void FixedUpdate()
    {
        if (isMoving)
        {
            chargeupCounter += 10;
        }

        isMoving = false;
    }


}
