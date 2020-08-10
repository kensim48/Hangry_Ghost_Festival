using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableArmsBooster : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    public int thrust;
    private float startTime;
    private float zRotation;
    private float zSpeed = 0;
    public float zAccel;
    public float zMaxSpeed;
    public GameObject whip;
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        startTime = Time.time;
        zRotation = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime < 1f)
            rb.AddForce(thrust * transform.up);
        else if (Time.time - startTime < 20f)
        {
            // rb.velocity = Vector3.zero;
            transform.rotation = Quaternion.Euler(-30f, 0, zRotation);
            zRotation += zSpeed;
            whip.SetActive(true);
            if (zSpeed < zMaxSpeed)
                zSpeed += zAccel;
        }
        else
            Destroy(gameObject);
    }
}
