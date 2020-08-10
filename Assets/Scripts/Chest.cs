using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_Animator;
    SpriteRenderer m_Sprite;
    float fade = 1f;
    private bool isFading = false;
    public GameObject weaponSpawner;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_Sprite = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isFading)
        {
            m_Sprite.color = new Color(1f, 1f, 1f, fade);
            fade -= 0.01f;
        }
        if (fade <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_Animator.SetTrigger("isExplode");
        }
    }

    public void CreatePickup()
    {
        print("Create pickup");
        isFading = true;
        Instantiate(weaponSpawner, gameObject.transform.position, gameObject.transform.rotation);
    }
}
