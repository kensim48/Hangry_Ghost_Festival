using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeele : EnemyBase
{

    // Shooting
    public GameObject SwordPrefab;
    private float timeBtwAtks;
    public float startTimeBtwAtks;
    public float timeBreakMovement;
    private float lastTimeMovement;


    public void Start()
    {
        base.Start();
        chasePatrolState = 0; // chase
        lastTimeMovement = Time.time;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.tag == "PlayerWeapon")
        {
            currentState = 1; // Hit state
            isHit = true;
            print("On Collision with projectile");
        }

    }
    public void OnTriggerExit2D(Collider2D other)
    {
        animator.SetBool("attacking", false);
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();
        if (isAttack)
        {
            if (timeBtwAtks <= 0)
            {
                //RotateBody();
                // Instantiate(projectile,transform.position , Quaternion.identity);// at the enemies position
                timeBtwAtks = startTimeBtwAtks;
            }
            else
            {
                //RotateBody();
                timeBtwAtks -= Time.deltaTime; // like a count down, once zero, spawn the projectile
            }
        }

    }

    public void Shoot()
    {

    }




}
