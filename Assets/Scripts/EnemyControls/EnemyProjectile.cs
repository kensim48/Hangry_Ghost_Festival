using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyBase
{
    // Shooting
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    private float timeBtwShots;
    public float startTimeBtwShots;

    public void Start()
    {
        base.Start();
        chasePatrolState = 1; // Patrol
        timeBtwShots = startTimeBtwShots;
    }
    public void Shoot()
    {
        // Create bullet at fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
        bulletrb.velocity = rb2d.velocity + (new Vector2(transform.right.x, transform.right.y) * bulletForce);
    }

    void Update()
    {
        base.Update();
        if (isAttack)
        {
            if (timeBtwShots <= 0)
            {
                Shoot();
                RotateBody();
                // Instantiate(projectile,transform.position , Quaternion.identity);// at the enemies position
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                RotateBody();
                timeBtwShots -= Time.deltaTime; // like a count down, once zero, spawn the projectile
            }
        }


    }

}
