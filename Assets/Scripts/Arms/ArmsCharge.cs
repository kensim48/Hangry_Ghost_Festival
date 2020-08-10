using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsCharge : ArmsClass
{

    public int amplifierBoosterMultiplier = 500;
    public GameObject chargeMeter;
    public GameObject noodle;
    public GameObject noodleSpawn;
    private bool isMoving;
    private int chargeTotal = 0;
    private int chargeRate = 2;
    public float timeDestroyLaser;
    private float laserShotTime;
    private bool noodleDestroyed = true;
    private GameObject noodlePoint;


    public override void Attack()
    {
        chargeTotal += chargeRate;
        if (chargeTotal >= 100)
        {
            noodlePoint = Instantiate(noodle, noodleSpawn.transform.position, noodleSpawn.transform.rotation);
            laserShotTime = Time.time;
        }
        isMoving = true;
    }

    public override void Move()
    {
        Vector2 boosterForce = (Vector2)(transform.rotation * Quaternion.Euler(0, 0, -90) * Vector2.right);
        if (chargeTotal >= 100)
        {
            rb.AddForce(boosterForce * amplifierBoosterMultiplier);
            noodleDestroyed = false;
        }
    }

    public void FixedUpdate()
    {
        chargeMeter.transform.localScale = new Vector3((float)chargeTotal / 100, chargeMeter.transform.localScale.y, chargeMeter.transform.localScale.z);
        if (!isMoving || chargeTotal >= 100) chargeTotal = 0;
        isMoving = false;
        if (Time.time - laserShotTime > timeDestroyLaser && !noodleDestroyed)
        {
            Destroy(noodlePoint);
            noodleDestroyed = true;
        }

    }


}
