using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectionDoor : MonoBehaviour
{
    public bool playerDetected;

    private RoomTemplates templates;

    void Start()
    {
        playerDetected = false;
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag ("Player")) 
        {
            Debug.Log("Player pass through");
            playerDetected = true;
            foreach (var obj in templates.doors)
                obj.SetActive(true);
        }

    }
}
