using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyBoss : MonoBehaviour
{
    /*
        Boss Player Phases
        1. Idle -> happens for a second after an attack phase or
           when the other boss is in attack phase
        2. White Boss Attack -> black boss idle animation
        3. Black Boss Attack -> white boss idle anomation
        4. enraged phase -> When one of the dies
        To have health bar

        // Idle Phase (Need to have a public timer variable)
        1. Happens when after phases for a few seconds
        2. Happens when the other enemy is attacked
    
        // Black Boss Attack
        1. Chase the player and attacks with the sign

        // White Boss Attack (Fan) 
        Charge up animation: will move to the corner and start spinning
        1. Create a probability generator that is a public variable (not needed if sequential)
        2. Create random value generator and see if the probability is met (not needed if sequential)
        3. if it is met, to move to corner of the room and start fanning animation
        4. else, continue in {some other phase}
        5. Spikes will appear on The ground (can use placeholders first)
        6. the player will be pushed in the direction of the fanning animation
        7. after the attack time (to make public variable)
        8. continues to the next phase

        // Constant Passive Attack
        The player is not allowed touch them or he will die

        // Enraged mode
        TBC
         */

    public Transform anchorpoint;
    public Transform toprightanchorpoint;
    public Transform topleftanchorpoint;
    public Transform bottomrigtanchorpoint;
    public Transform bottomleftanchorpoint;
    public Transform blackboss;
    public Transform whiteboss;
    public int currentPhase = 0;
    private int previousAttackPhase;
    public float idleDuration;
    public float whiteDuration;
    public float blackDuration;

    private float idleEnd;
    private float whiteEnd;
    private float blackEnd;
    public float moveToCornerSpeed;
    private bool firstcheckwhite = true;
    private bool firstcheckblack = true;
    private Transform nearestAnchor;

    public GameObject player;
    public Rigidbody2D playerRb2d;
    public Rigidbody2D blackbossRb2d;

    public GameObject spikeParent;
    private int numberOfSpikes;
    public int showNumberOfSpikes = 3;
    private bool isSpikesGenerated = false;
    private Vector3 fanDirection;
    public float fanthrust = 0.3f;

    public float stoppingDistance; // The higher the value, the further away it will stop 
    public float speed; // Speed of the black boss

    public bool isBlackDead;
    public bool isWhiteDead;

    private int BossDeathCount;

    enum BossPhase : int
    {
        idle,
        white,
        black,
        enraged
    }


    void Start()
    {
        blackboss = GameObject.FindGameObjectWithTag("BlackBoss").transform;
        whiteboss = GameObject.FindGameObjectWithTag("WhiteBoss").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb2d = player.GetComponent<Rigidbody2D>();
        // blackbossRb2d = GameObject.FindGameObjectWithTag("BlackBoss").GetComponent<Rigidbody2D>();
        SetSpikesInvisible(spikeParent.transform);
        EnemyBossHealth.notifyBossDeath += updateBossDeath;

    }

    // Update is called once per frame
    void Update()
    {
        // Check if it is end game
        if (BossDeathCount >= 2){
            // Insert Game End scene or smt
        }

        //The White Boss to follow and mirror the position of the black box.
        float distFromAnchor = anchorpoint.position.x - blackboss.position.x;
        float newX = blackboss.position.x + 2 * (distFromAnchor);
        whiteboss.position = new Vector3(newX, blackboss.position.y, -1);

        // print("WhiteBoss: x " +  transform.eulerAngles.x);
        // print("WhiteBoss: y" +  transform.eulerAngles.y);
        // print("WhiteBoss: z" +  transform.eulerAngles.z);
        // transform.eulerAngles = new Vector3( transform.eulerAngles.x, yRotation, transform.eulerAngles.z );

        switch (currentPhase)
        {
            case (int)BossPhase.idle: //idle
                                      // To call animation here
                if (Time.time == idleEnd)
                {
                    if (previousAttackPhase == (int)BossPhase.white)
                    {
                        currentPhase = (int)BossPhase.black;
                    }
                    else if (previousAttackPhase == (int)BossPhase.black)
                    {
                        currentPhase = (int)BossPhase.white;
                    }
                }
                break;

            case (int)BossPhase.white: //white

                #region 1. Charge Up animation by moving to the corner
                while (firstcheckwhite)
                {
                    whiteEnd = Time.time + whiteDuration;
                    nearestAnchor = GetNearestAnchorPoint(blackboss);
                    firstcheckwhite = false;
                }
                // print("Boss Move: " + blackboss.position);
                float step = moveToCornerSpeed * Time.deltaTime;
                blackboss.position = Vector3.MoveTowards(blackboss.position, nearestAnchor.position, step);
                #endregion

                #region 2. Spawn Spikes
                // Get number of child Spikes
                while (!isSpikesGenerated)
                {
                    numberOfSpikes = spikeParent.transform.childCount;
                    // print("Spike After: " + spikeParent.transform.childCount);
                    for (int i = 0; i < showNumberOfSpikes; i++)
                    {
                        int randomchild = Random.Range(0, numberOfSpikes);
                        // gameObject childspike = spikeParent.transform.GetChild(randomchild).gameObject;
                        while (spikeParent.transform.GetChild(randomchild).gameObject.activeSelf == true)
                        {
                            // Keep getting new random int
                            randomchild = Random.Range(0, numberOfSpikes);
                        }
                        spikeParent.transform.GetChild(randomchild).gameObject.SetActive(true);
                    }
                    isSpikesGenerated = true;
                }

                #endregion

                #region 3. Move player
                // Check enemy is at the corner
                print("blackboss posiion: " + blackboss.position);
                print("anchorpoint position: " + nearestAnchor.position);
                if (blackboss.position == nearestAnchor.position)
                {
                    // Start moving player
                    fanDirection = player.transform.position - whiteboss.position;
                    playerRb2d.AddForce(fanDirection * fanthrust);
                }
                #endregion

                #region 4. Checks if the time is up for the phase
                if (Time.time >= whiteEnd)
                {
                    // Go to idle phase
                    currentPhase = (int)BossPhase.idle;
                    idleEnd = Time.time + idleDuration;
                    previousAttackPhase = (int)BossPhase.white;
                    firstcheckwhite = true;
                    isSpikesGenerated = false;
                    SetSpikesInvisible(spikeParent.transform);
                }
                #endregion

                break;

            case (int)BossPhase.black:
                // @HUAN XUAN To start the sword animation for the Sign here
                #region 1. Set up Phase timer
                while (firstcheckblack)
                {
                    blackEnd = Time.time + blackDuration;
                    firstcheckblack = false;
                }
                #endregion

                #region 2. Boss to chase player, as long as the sword hit, it deducts life
                moveBoss();
                #endregion

                #region Checks if the time is up for the phase
                // Remember to end the animation here
                if (Time.time >= blackEnd)
                {
                    // Go to idle phase
                    currentPhase = (int)BossPhase.idle;
                    idleEnd = Time.time + idleDuration;
                    previousAttackPhase = (int)BossPhase.black;
                    firstcheckblack = true;
                }
                #endregion
                break;

            case (int)BossPhase.enraged:
                print("In Enraged Mode");
                // If Black Boss is alive
                if (!isBlackDead){

                }


                // If White Boss alive
                else if (!isWhiteDead){
                    
                }


                break;
        }
    }

    // PLayer controller will deal with this
    // First case is touching of either bosses 
    // Following should be placed in the player controller
    // state is when Black Boss hits player with sword
    //state is when player hits the spike
    public void updateBossDeath(string message)
    {
        if (message.Contains("BlackBoss")){
            isBlackDead = true;
        } else if (message.Contains("WhiteBoss")){
            isWhiteDead = true;
        }
        currentPhase = (int)BossPhase.enraged;
        BossDeathCount ++;
    }
    public Transform GetNearestAnchorPoint(Transform current)
    {
        // Grab which is the nearest anchor point
        float trdistance = Vector3.Distance(toprightanchorpoint.position, current.position);
        float tldistance = Vector3.Distance(topleftanchorpoint.position, current.position);
        float brdistance = Vector3.Distance(bottomrigtanchorpoint.position, current.position);
        float bldistance = Vector3.Distance(bottomleftanchorpoint.position, current.position);

        float[] anArray = { trdistance, tldistance, brdistance, bldistance };

        // Finding max
        float m = anArray.Max();
        // Positioning max
        float p = System.Array.IndexOf(anArray, m);
        print("Position of max: " + p);
        if (p == 0)
        {
            return toprightanchorpoint;
        }
        else if (p == 1)
        {
            return topleftanchorpoint;
        }
        else if (p == 2)
        {
            return bottomrigtanchorpoint;
        }
        else if (p == 3)
        {
            return bottomleftanchorpoint;
        }
        else
        {
            print("Error with anchorpoints");
            return anchorpoint;
        }

    }

    public void SetSpikesInvisible(Transform parent)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void moveBoss()
    {
        print("Stopping Distance: " + stoppingDistance);
        if (Vector3.Distance(blackboss.position, player.transform.position) > stoppingDistance)
        {
            blackbossRb2d.velocity = (player.transform.position - blackboss.position) * speed * Time.deltaTime;
        }
        else if (Vector3.Distance(blackboss.position, player.transform.position) < stoppingDistance)
        {
            blackbossRb2d.velocity = Vector2.zero;
        }

    }
}
