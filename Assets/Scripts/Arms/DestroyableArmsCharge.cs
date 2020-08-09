using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableArmsCharge : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    public Rigidbody2D laserObject;

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
            // Rigidbody2D bombs = Instantiate(laserObject, transform.position, transform.rotation) as Rigidbody2D;
            Destroy(gameObject);
        }
    }
}
