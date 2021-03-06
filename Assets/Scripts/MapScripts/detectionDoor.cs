﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class detectionDoor : MonoBehaviour
{
    public bool playerDetected;

    public int roomNum;

    private int rand;

    private RoomTemplates templates;

    private EnemyTemplates enemyTemp;

    private AddRoom addroom;

    public GameObject eneSP;

    public delegate void NotifyRoomEnter();
    public static event NotifyRoomEnter notifyRoomEnter;

    void Start()
    {
        playerDetected = false;
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        enemyTemp = GameObject.FindGameObjectWithTag("EnemyTemplate").GetComponent<EnemyTemplates>();
        addroom = transform.parent.parent.parent.GetComponent<AddRoom>(); //reference to addroom script at base room
        // try {
        //     eneSP = transform.parent.parent.parent.GetChild(3).gameObject;
        // }
        // catch (Exception e) {
        //     print("error");
        // }   
        // eneSP = transform.parent.parent.parent.GetChild(3).gameObject;
        roomNum = addroom.roomNumber;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag ("Player") && (templates.roomCleared.Contains(roomNum) == false)) 
        {
            templates.roomCleared.Add(roomNum);
            Debug.Log("Player pass through");
            playerDetected = true;
            foreach (var obj in templates.doors)
                obj.SetActive(true);
            if (notifyRoomEnter != null) 
            {
                notifyRoomEnter();
                Debug.Log("Event death sent");
            }
            
            Invoke("Spawn", 1f);
        }

    }

    void Spawn() {
        foreach (Transform child in eneSP.transform)
        {
            var pos = child.position;
            rand = UnityEngine.Random.Range(0, enemyTemp.enemies.Length);
            Instantiate(enemyTemp.enemies[rand],new Vector3(pos.x,pos.y, -1f),enemyTemp.enemies[rand].transform.rotation);
            Debug.Log("Enemies Spawned");
            templates.numEnemies += 1;
            templates.trans = child.position;
        }
    }
}
