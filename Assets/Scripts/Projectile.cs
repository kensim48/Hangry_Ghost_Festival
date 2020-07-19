using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float velocityX = 5f;
    public float velocityY = 4f;
    public float speed;
    private Transform player;
    private Vector2 target;

    private Rigidbody2D projectile;
    void Start()
    {
        player  = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.transform.position.x, player.transform.position.y);
        projectile = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = Vector2.MoveTowards(transform.position, target, speed*Time.deltaTime);  
        // Check if projectile hit the right destination
        //  if (transform.position.x == target.x && transform.position.y == target.y){
        //      DestroyProjectile();
        //  }
        // projectile.AddForce(target*speed);
        // projectile.velocity = new Vector2(target.x * velocityX,target.y * velocityY);

    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            DestroyProjectile();
        }
    }
    void OnBecameInvisible() {
         DestroyProjectile();
     }

    void DestroyProjectile(){
        Destroy(gameObject);
    }
}
