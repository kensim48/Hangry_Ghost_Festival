using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectBossRoom : MonoBehaviour
{
    private RoomTemplates templates;
    public GameObject eneSP;
    public bool playerDetected;

    public GameObject bossDoor;

    public GameObject bossManager;

    private EnemyTemplates enemyTemp;

    private int i;

    void Start()
    {
        playerDetected = false;
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        enemyTemp = GameObject.FindGameObjectWithTag("EnemyTemplate").GetComponent<EnemyTemplates>();
        bossDoor.SetActive(false);
        i=0;
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
            var pos = child.position;
            // rand = UnityEngine.Random.Range(0, enemyTemp.enemies.Length);
            Instantiate(enemyTemp.boss[i],new Vector3(pos.x,pos.y, -1f),enemyTemp.boss[i].transform.rotation);
            Debug.Log("Boss Spawned");
            i+=1;
            // templates.numEnemies += 1;
            // templates.trans = child.position;
        }
        bossManager.SetActive(true);
        bossManager.GetComponent<EnemyBoss>().blackboss = GameObject.FindGameObjectWithTag("BlackBoss").transform;
        bossManager.GetComponent<EnemyBoss>().whiteboss = GameObject.FindGameObjectWithTag("WhiteBoss").transform;
        bossManager.GetComponent<EnemyBoss>().blackbossRb2d = GameObject.FindGameObjectWithTag("BlackBoss").GetComponent<Rigidbody2D>();
    }
}
