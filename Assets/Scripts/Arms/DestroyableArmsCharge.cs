using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableArmsCharge : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    public int thrust;
    private float startTime;
    public GameObject noodleParent;
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
        else if (Time.time - startTime < 1.5f)
            noodleParent.SetActive(true);
        else
            Destroy(gameObject);
    }
}
