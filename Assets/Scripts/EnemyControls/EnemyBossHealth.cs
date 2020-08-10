using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossHealth : MonoBehaviour
{
    // Create death event that will trigger the enaged mode
    public int health = 5;
    private string BossName;
    public delegate void NotifyBossEnemyDeath(string message);
    public static event NotifyBossEnemyDeath notifyBossDeath;

    // Start is called before the first frame update
    void Start()
    {
        BossName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        // This sends a message to register a change in the event
        if (health <= 0)
        {
            print("Boss health < 0");
            RaiseBossDeathEvent(BossName);
            gameObject.SetActive(false);
        }



    }

    public void RaiseBossDeathEvent(string message){ 
        if (notifyBossDeath != null)
            {
                notifyBossDeath(message);
            }

    }



    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player_projectile")
        {
            print("On collision with player's projectile");
        }

        // add another case for collision with player's sword
    }
}
