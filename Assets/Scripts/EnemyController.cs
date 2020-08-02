using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float stoppingDistance; // The higher the value, the further away it will stop 
    public float retreatDistance; // When enmy will back away from target
    public Transform player;

    // public Vector3 offset = new Vector3(5, 5, 0);

    // Shooting
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    private float timeBtwShots;
    public float startTimeBtwShots;
    // public GameObject projectile;
    private Vector2 _originalPosition;
    Rigidbody2D rb2d;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //equal to the position of object named player
        timeBtwShots = startTimeBtwShots;
        rb2d = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        _originalPosition = transform.position;
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

    void Shoot()
    {
        // Create bullet at fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
        bulletrb.velocity = rb2d.velocity + (new Vector2(transform.right.x, transform.right.y) * bulletForce);
        // bulletrb.AddForce(transform.right * bulletForce);

        // bulletrb.AddForce(new Vector2(bulletForce,0), ForceMode2D.Impulse);

    }

    void RotateBody()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}

