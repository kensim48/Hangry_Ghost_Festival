using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTemplates : MonoBehaviour
{

    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;
    public List<GameObject> doors;

    public List<int> roomCleared;

    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;
    private int count = 0;
    void FixedUpdate()
    {
        if (rooms.Count>15)
        {
            Debug.Log("reset due to >15");
            SceneManager.LoadScene("MapGeneration");
            waitTime = 4f;
        }
        if (waitTime<=2 && rooms.Count<10)
        {
            Debug.Log("reset due to <10");
            SceneManager.LoadScene("MapGeneration");
            waitTime = 4f;
        }
        if (waitTime <= 0 && spawnedBoss == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    foreach (Transform child1 in rooms[i].transform)
                    {
                        foreach (Transform child in child1)
                            if(child.CompareTag ("Door"))
                            {
                                var pos = rooms[i].transform.position;
                                var pos1 = child.position;
                                var pos2 = pos1 - pos; 
                                Instantiate(boss, new Vector3(pos1.x+pos2.x,pos1.y+pos2.y, -0.1f), Quaternion.identity);
                            }
                    }
                    spawnedBoss = true;
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    void Start()
    {
        detectionDoor.notifyRoomEnter += updatePlayerDeath;
        roomCleared.Add(0);
        roomCleared.Add(1);
    }

    void updatePlayerDeath(){
        Debug.Log("Event death recieved");
    }
}
