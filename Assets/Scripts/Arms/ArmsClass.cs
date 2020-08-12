using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ArmsClass : MonoBehaviour
{
    public Rigidbody2D rb;
    private AudioSource audioSource;
    public void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public abstract void Attack();

    public abstract void Move();
}
