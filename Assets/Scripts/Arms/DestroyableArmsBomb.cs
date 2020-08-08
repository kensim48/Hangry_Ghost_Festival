using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableArmsBomb : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    public Rigidbody2D bombSingle;

    public int thrust;
    private float startTime;
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime < 1f)
            rb.AddForce(thrust * transform.up);
        else
        {
            for (int i = 0; i < 20; i++)
            {
                Rigidbody2D bombs = Instantiate(bombSingle, transform.position, transform.rotation) as Rigidbody2D;
                bombs.AddForce(new Vector3(Random.Range(-5000.0f, 5000.0f), Random.Range(-5000.0f, 5000.0f), 0));
            }
            Destroy(gameObject);
        }
    }
}
