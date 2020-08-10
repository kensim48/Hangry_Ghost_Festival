using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    SpriteRenderer m_Sprite;
    public Sprite[] spriteList;
    public string[] tagList;
    public int listSize;
    // Start is called before the first frame update
    void Start()
    {
        int randomValue = Random.Range(0, listSize);
        m_Sprite = gameObject.GetComponent<SpriteRenderer>();
        m_Sprite.sprite = spriteList[randomValue];
        gameObject.tag = tagList[randomValue];
    }
}
