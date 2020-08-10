using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

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

    public int numEnemies;

    public float waitTime;
    private bool spawnedBoss=false;

    private bool allowResume=false;
    public GameObject boss;

    public GameObject bossstairs;
    private int count = 0;

    private int i=0;

    public GameObject item;

    public Vector3 trans;

    public int test =1;
    public GameObject overlay;

    public GameObject playerObject;

    public GameObject playbutt;

    public GameObject pauseMenu;

    public GameObject gameoverMenu;

    public GameObject winMenu;

    public Text winMenuText;

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
            playerObject.transform.position = new Vector3(0,0,-1f);
            for (int i = rooms.Count-1; i >= 0; i--)
            {
                playerObject.transform.position = new Vector3(0,0,-1f);
                if (i < 6 && spawnedBoss == false)
                {
                    Debug.Log("reset due to boss room too close");
                    SceneManager.LoadScene("MapGeneration");
                    waitTime = 4f;
                }
                if ((rooms[i].gameObject.name =="T(Clone)" || rooms[i].gameObject.name =="B(Clone)" || rooms[i].gameObject.name =="L(Clone)" || rooms[i].gameObject.name =="R(Clone)" )&& spawnedBoss == false)
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
                                Instantiate(bossstairs, new Vector3(pos1.x,pos1.y, -0.1f), Quaternion.identity);
                            }
                    }
                    spawnedBoss = true;
                    EnemyBase.notifyDeath += updateEnemyDeath;
                    PlayerController.notifyPlayerDeath += updatePlayerDeath1;
                    // PlayerController.notifyPlayerDeath += updateWinGame; //to update to wingame event
                    Debug.Log(i);
                    // overlay.SetActive (false);
                    playerObject.transform.position = new Vector3(0,0,-1f);
                    Time.timeScale = 0f;
                    playbutt.SetActive(true);
                }
                // Debug.Log(rooms[i].gameObject.name);
            }
            
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
        if (Input.GetKey("escape") && spawnedBoss == true && allowResume == true)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("escape pressed");
        }
    }

    void Start()
    {
        // EnemyBase.notifyDeath += updatePlayerDeath;
        roomCleared.Add(0);
        roomCleared.Add(1);
        playerObject.SetActive(false);
        playbutt.SetActive(false);
        spawnedBoss=false;
        allowResume=false;
    }

    void updateEnemyDeath(){
        Debug.Log("Event death recieved");
        numEnemies -= 1;
        if(numEnemies == 0){
            foreach (var obj in doors)
                {
                    Debug.Log("door" + i.ToString());
                    try{
                        obj.SetActive(false);
                    }
                    catch (Exception e) {
                        // print(obj);
                        print("Door error");
                    }
                    i++;
                }
            Instantiate(item, new Vector3(trans.x, trans.y, -1), transform.rotation);
        }
    }

    void updatePlayerDeath1(){
        gameoverMenu.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Player death");
    }

    void updateWinGame(){
        winMenu.SetActive(true);
        winMenuText.text = "Beelzebucks: " + GameObject.FindGameObjectWithTag("Score").GetComponent<PlayerStats>().playerScore.ToString();
        Time.timeScale = 0f;
        Debug.Log("Game Won");
    }

    public void playButton()
    {
        if(spawnedBoss == true)
        {
            overlay.SetActive (false);
            Time.timeScale = 1f;
            playerObject.SetActive(true);
            allowResume = true;
        }
    }
    public void resumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void restartButton()
    {
        Debug.Log("reset due to restartbutton");
        EnemyBase.notifyDeath -= updateEnemyDeath;
        PlayerController.notifyPlayerDeath -= updatePlayerDeath1;
        // PlayerController.notifyPlayerDeath -= updateWinGame; //to update to wingame event
        SceneManager.LoadScene("MapGeneration");
        waitTime = 4f;
        Time.timeScale = 1f;

    }
    public void quitButton()
    {
        Application.Quit();
    }
}
