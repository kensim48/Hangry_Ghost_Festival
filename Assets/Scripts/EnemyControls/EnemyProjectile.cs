﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyBase
{
    // Shooting
    public GameObject firePoint;
    public GameObject fireParent;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    private float timeBtwShots;
    public float startTimeBtwShots;
    private Animator m_Animator;


    public void Start()
    {
        base.Start();
        chasePatrolState = 1; // Patrol
        timeBtwShots = startTimeBtwShots;
        m_Animator = gameObject.transform.GetChild(1).GetComponent<Animator>();
    }
    public void Shoot()
    {
        m_Animator.SetBool("attacking", true);
        // Create bullet at fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
        bulletrb.velocity = rb2d.velocity + (new Vector2(firePoint.transform.right.x, firePoint.transform.right.y) * bulletForce);
    }

    void Update()
    {
        base.Update();
        RotateBody();
        if (isAttack)
        {
            if (timeBtwShots <= 0)
            {
                Shoot();

                // Instantiate(projectile,transform.position , Quaternion.identity);// at the enemies position
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                // RotateBody();
                timeBtwShots -= Time.deltaTime; // like a count down, once zero, spawn the projectile
            }
            //m_Animator.SetBool("attacking", false);
        }


    }

    public void RotateBody()
    {
        Vector3 direction = player.position - fireParent.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        fireParent.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // print(other.gameObject.tag);
        if (other.gameObject.tag == "PlayerWeapon")
        {
            currentState = 1; // Hit state
            isHit = true;
            print("On Collision with projectile");
            m_Animator.SetTrigger("isHit");
        }
    }

}
