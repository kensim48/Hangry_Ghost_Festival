using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableArmsShooter : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    public Rigidbody2D shooterProjectile;

    public int thrust;
    private float startTime;
    private float lastShootTime;
    private float timeBetweenShoot;
    private float zRotation;
    private float zSpeed = 0;
    public float zAccel;
    public float zMaxSpeed;
    public Transform shootingPoint;
    public int shootThrust;
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
        else if (Time.time - startTime < 20f)
        {
            // rb.velocity = Vector3.zero;
            transform.rotation = Quaternion.Euler(-30f, 0, zRotation);
            zRotation += zSpeed;
            if (zSpeed < zMaxSpeed)
                zSpeed += zAccel;
            if (Time.time - lastShootTime > timeBetweenShoot)
            {
                Rigidbody2D projectile = Instantiate(shooterProjectile, shootingPoint.position, shootingPoint.rotation) as Rigidbody2D;
                projectile.AddForce(shootingPoint.forward * shootThrust);
            }
        }
        else
            Destroy(gameObject);
    }
}
