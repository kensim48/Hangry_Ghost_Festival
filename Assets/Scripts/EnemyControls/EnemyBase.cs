﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    #region Movement
    public float speed;
    public Vector3 change;
    public bool facingLeft = false;
    public Animator animator;
    public float stoppingDistance; // The higher the value, the further away it will stop 
    public float retreatDistance; // When enmy will back away from target
    public float patrolDistance; //Used for enemies who need to patrol, min distance required to chase
    public float shootingDistance; //The minimum distance required for shooting distance
    public Transform player;
    public Rigidbody2D rb2d;
    #endregion

    public int currentState = 0; //current state in state machine
    public int chasePatrolState;

    #region Health
    public bool isDead = false;
    public int health;
    public bool isHit = false;
    public bool isAttack = false;
    public float startTime;
    public float waitTime = 1f;
    public GameObject coin;
    private Quaternion lockedRotation;

    #endregion

    public delegate void NotifyEnemyDeath();
    public static event NotifyEnemyDeath notifyDeath;


    Animator m_Animator;


    enum ChasePatrolStates
    {
        Chase,
        Patrol
    }
    enum EnemyStates
    {
        ChasePatrolStates,
        Hit,
        Retreat,
        Attack,
        Death,
    }

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //equal to the position of object named player
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();

        startTime = Time.time;
        lockedRotation = transform.rotation;
    }

    // Update is called once per frame
    public void Update()
    {
        transform.rotation = lockedRotation;
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        // Based on the switch case key based on EnemyStates enum
        switch (currentState)
        {
            case 0: //ChasePatrolStates
                switch (chasePatrolState)
                {
                    case 0: // Chase
                        moveEnemy();
                        break;

                    case 1: // Patrol
                        // For Patrol logic, if player is not within the range, will stay  & withn distance to shoot
                        if (Vector3.Distance(transform.position, player.position) < patrolDistance && Vector3.Distance(transform.position, player.position) < shootingDistance)
                        {
                            moveEnemy();
                            // Enter Attack States
                            currentState = 3;
                            isAttack = true;
                        }
                        else if (Vector3.Distance(transform.position, player.position) < patrolDistance)
                        {
                            isAttack = false;
                            moveEnemy();
                        }

                        break;
                }

                break;

            case 1: //if Hit by food
                // TODO: if collision detected, to enter this state
                if (Time.time - startTime > waitTime)
                {
                    startTime = Time.time;
                    health--;
                    if (health <= 1)
                    {
                        // Check if player is death first, if yes, set to Death state
                        dyingFunction();
                    }
                    print("On Collision: " + health.ToString());

                }
                else
                {
                    isHit = false;
                    currentState = 0;
                }

                break;

            case 2: //Retreat
                break;

            case 3: //Attack
                    //Check if Player is beyond shooting distance, stop attacking
                if (Vector3.Distance(transform.position, player.position) > shootingDistance)
                {
                    isAttack = false;
                    currentState = 0;
                }
                break;

        }
    }

    private void dyingFunction()
    {
        isDead = true;
        Instantiate(coin, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
        Destroy(gameObject);
        if (notifyDeath != null)
        {
            notifyDeath();
        }
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


    public virtual void moveEnemy()
    {
        float distanceFromPlayer = Vector3.Distance(transform.position, player.position);
        // check distance (enemies position, players position) > stopping distance
        if (distanceFromPlayer > stoppingDistance)
        {
            rb2d.velocity = (player.position - transform.position) * speed * Time.deltaTime * 10;
            CheckDirection();

        }
        else if (distanceFromPlayer < stoppingDistance && distanceFromPlayer > retreatDistance)
        {
            rb2d.velocity = Vector2.zero;
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

    public void CheckDirection()
    {
        change = transform.position - player.position;
        if (change.x < 0)
        {
            animator.transform.Rotate(0, 180, 0);
        }
        if (change.x > 0)
        {
            animator.transform.Rotate(0, 0, 0);
        }
    }




}
