using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    #region Movement
    public float speed;
    public float stoppingDistance; // The higher the value, the further away it will stop 
    public float retreatDistance; // When enmy will back away from target
    public Transform player;
    private Vector3 _originalPosition;
    public Rigidbody2D rb2d;
    #endregion

    public int currentState = 0; //current state in state machine
    public int chasePatrolState = 0;

    #region Health
    public bool isDead = false;
    public int health;
    public bool isHit = false;

    #endregion

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
    }

    // Update is called once per frame
    public void Update()
    {
        _originalPosition = transform.position;
        // Based on the switch case key based on EnemyStates enum
        switch (currentState)
        {
            case 0: //ChasePatrolStates
                switch (chasePatrolState)
                {
                    case 0: // Chase
                            // check distance (enemies position, players position) > stopping distance
                        if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
                        {
                            //move towards player - MOvTowards is sim to Lerp but has maxDistanceDelta (if -ve, pushes away from target)
                            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime); //The speed*Time.delta time prevents faster computer from having faster enemies
                            RotateBody();

                        }
                        else if (Vector3.Distance(transform.position, player.transform.position) < stoppingDistance && Vector3.Distance(transform.position, player.position) > retreatDistance)
                        {
                            transform.position = this.transform.position;
                            RotateBody();
                        }
                        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
                        {
                            //if enemy is too close
                            transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                            RotateBody();
                        }
                        break;

                    case 1: // Patrol
                        break;
                }

                break;

            case 1: //Hit
                break;

            case 2: //Retreat
                break;

            case 3: //Attack
                break;

            case 4: //Death
                break;


        }




    }


    public void RotateBody()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }



}
