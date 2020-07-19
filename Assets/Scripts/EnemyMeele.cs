using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeele : MonoBehaviour
{
    public float speed;
    public float stoppingDistance; // The higher the value, the further away it will stop 
    public float retreatDistance; // When enmy will back away from target
    public Transform player;



    // Shooting
    public GameObject SwordPrefab;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //equal to the position of object named player
    }

    // Update is called once per frame
    void Update()
    {
        // check distance (enemies position, players position) > stopping distance
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            //move towards player - MOvTowards is sim to Lerp but has maxDistanceDelta (if -ve, pushes away from target)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime); //The speed*Time.delta time prevents faster computer from having faster enemies
            RotateBody();

        }
        else if (Vector2.Distance(transform.position, player.transform.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
            RotateBody();
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            //if enemy is too close
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            RotateBody();
        }


    }

    

    void RotateBody()
    {
        Vector2 direction = player.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
