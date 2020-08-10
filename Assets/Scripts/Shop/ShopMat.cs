using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMat : MonoBehaviour
{
    public bool isSelected = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other){
        // print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            isSelected = true;
            print("Player on Mat");
        }
    }
}
