using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    private float startTime;
    void Start()
    {
        startTime = Time.time;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > 30f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyWeapon")
            Destroy(gameObject);
    }
}
