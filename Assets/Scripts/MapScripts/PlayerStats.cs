using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text scoreText;

    public int playerScore = 0;

    // public Text winMenuText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Beelzebucks: " + playerScore.ToString();
        // winMenuText.text = "Hell Dollar: " + playerScore.ToString();
    }
}
