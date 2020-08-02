using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableArmsBooster : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    public int thrust;
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(thrust * transform.up);
    }
}
