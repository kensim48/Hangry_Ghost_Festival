using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFollow : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform player;

    private float speed = 0f;
    public float acceleration;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //equal to the position of object named player
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime); //The speed*Time.delta time prevents faster computer from having faster enemies
    }

    void FixedUpdate()
    {
        speed += acceleration;
    }
}
