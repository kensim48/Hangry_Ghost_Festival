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
    public float moveToCornerSpeed;
    private bool firstcheck = true;
    private Transform nearestAnchor;

    public int whiteHealth;
    public int blackHealth;

    public GameObject player;
    public Rigidbody2D playerRb2d;

    public GameObject spikeParent;
    private int numberOfSpikes;
    public int showNumberOfSpikes = 3;
    private bool isSpikesGenerated = false;
    private Vector3 fanDirection;
    public float fanthrust = 0.3f;

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
        SetSpikesInvisible(spikeParent.transform);

    }

    // Update is called once per frame
    void Update()
    {
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
                while (firstcheck)
                {
                    whiteEnd = Time.time + whiteDuration;
                    nearestAnchor = GetNearestAnchorPoint(blackboss);
                    firstcheck = false;
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

                    // Random generator to select child index
                    // int randomchild = Random.Range(0, numberOfSpikes);
                    // Transform childspike = spikeParent.transform.GetChild(randomchild);
                    // print("Child Spike: " + childspike);
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


                #region Checks if the time is up for the phase
                if (Time.time >= whiteEnd)
                {
                    // Go to idle phase
                    currentPhase = (int)BossPhase.idle;
                    idleEnd = Time.time + idleDuration;
                    previousAttackPhase = (int)BossPhase.white;
                    firstcheck = true;
                    isSpikesGenerated = false;
                }
                #endregion

                break;

            case (int)BossPhase.black:

                break;

            case (int)BossPhase.enraged:
                break;
        }
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
}
