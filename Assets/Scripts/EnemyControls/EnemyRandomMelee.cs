using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomMelee : EnemyBase
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
        if (other.CompareTag("Player"))
        {
            Debug.Log("hitting player");
            animator.SetBool("attacking", true);
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

    public override void moveEnemy(){
                float distanceFromPlayer = Vector3.Distance(transform.position, player.position);
        // check distance (enemies position, players position) > stopping distance
        if (distanceFromPlayer > stoppingDistance)
        {
            rb2d.AddForce((player.position - transform.position) * speed * Time.deltaTime);
            CheckDirection();
        }
        else if (distanceFromPlayer < stoppingDistance && distanceFromPlayer > retreatDistance)
        {
            // rb2d.velocity = Vector2.zero;
            //RotateBody();
            CheckDirection();
        }
        else if (distanceFromPlayer < retreatDistance)
        {
            //if enemy is too close
            // transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            rb2d.velocity = -(player.position - transform.position) * speed * Time.deltaTime * 10;
            //RotateBody();
            CheckDirection();
        }
    }

    public void Shoot()
    {

    }




}
