using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectBossRoom : MonoBehaviour
{
    private RoomTemplates templates;
    public GameObject eneSP;
    public bool playerDetected;

    public GameObject bossDoor;

    void Start()
    {
        playerDetected = false;
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        bossDoor.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag ("Player") && (playerDetected == false)) 
        {
            bossDoor.SetActive(true);
            Debug.Log("Player pass through");
            playerDetected = true;
            Invoke("Spawn", 1f);
        }
        

    }

    void Spawn() {
        foreach (Transform child in eneSP.transform)
        {
            // var pos = child.position;
            // rand = UnityEngine.Random.Range(0, enemyTemp.enemies.Length);
            // Instantiate(enemyTemp.enemies[0],new Vector3(pos.x,pos.y, -1f),enemyTemp.enemies[0].transform.rotation);
            Debug.Log("Boss Spawned");
            // templates.numEnemies += 1;
            // templates.trans = child.position;
        }
    }
}
