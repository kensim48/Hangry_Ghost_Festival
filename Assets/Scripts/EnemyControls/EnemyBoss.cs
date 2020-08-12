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
        
         */

    #region State Handling
    public int currentPhase = (int)BossPhase.idle;
    private int previousAttackPhase;
    public int enragedPhase = (int)EnragedPhase.normal;
    public int currentEnragePhase;
    enum EnragedPhase : int
    {
        normal,
        enraged

    }
    enum BossPhase : int
    {
        idle,
        white,
        black,
        enraged
    }
    enum EnragedCurrentPhase : int
    {
        franticfan,
        frantichit,
        youneedjesus,
        idle
    }

    #endregion

    #region variables for Normal Boss Mode
    public Transform anchorpoint;
    public Transform toprightanchorpoint;
    public Transform topleftanchorpoint;
    public Transform bottomrigtanchorpoint;
    public Transform bottomleftanchorpoint;
    public Transform blackboss;
    public Transform whiteboss;

    public float idleDuration;
    public float whiteDuration;
    public float blackDuration;

    private float idleEnd;
    private float whiteEnd;
    private float blackEnd;
    public float moveToCornerSpeed;
    private bool firstcheckwhite = true;
    private bool firstcheckblack = true;
    private bool firstcheckidle = true;
    private Transform nearestAnchor;

    public GameObject player;
    public Rigidbody2D playerRb2d;
    public Rigidbody2D blackbossRb2d;
    public Rigidbody2D whitebossRb2d;

    public GameObject spikeParent;
    private int numberOfSpikes;
    public int showNumberOfSpikes = 3;
    private bool isSpikesGenerated = false;
    private Vector3 fanDirection;
    public float fanthrust = 0.3f;

    public float stoppingDistance; // The higher the value, the further away it will stop 
    public float chaseSpeed; // Speed of the black boss

    public bool isBlackDead;
    public bool isWhiteDead;

    private int BossDeathCount;
    #endregion

    #region Variables for Enraged Boss Mode
    private bool firstEnragedEnter = true;
    private Transform enragedboss;
    private Rigidbody2D enragedbossrb2d;

    // for fantic phase
    public float franticfanduration;
    private float franticfanEnd;
    private bool firstfranticfan = true;
    private bool isSpikesGenerated_enraged = false;
    public GameObject spikeParent_enraged;
    public float franticfanWaitDuration;
    private float franticfanMoveEnd;
    private bool isfranticfanmove = false;
    private Transform currentPosition;
    public float franticfanMoveSpeed;
    public float franticfanthrust;

    private bool firstfrantichit = true;
    public float enragedspeed;
    public float frantichitDuration;
    private float frantichitEnd;

    private bool firstcheckidle_enraged = true;
    private float enragedidleEnd;
    public float enragedidleDuration;

    private bool facingLeft = false;
    public Animator heiheiAnimator;
    public Animator heiweapAnimator;
    public Animator baibaiAnimator;
    public Animator baiweapAnimator;


    #endregion

    void Start()
    {
        // blackboss = GameObject.FindGameObjectWithTag("BlackBoss").transform;
        // whiteboss = GameObject.FindGameObjectWithTag("WhiteBoss").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb2d = player.GetComponent<Rigidbody2D>();
        // blackbossRb2d = GameObject.FindGameObjectWithTag("BlackBoss").GetComponent<Rigidbody2D>();
        SetSpikesInvisible(spikeParent.transform);
        EnemyBossHealth.notifyBossDeath += updateBossDeath;
        heiheiAnimator = GameObject.FindGameObjectWithTag("BlackBoss").GetComponent<Animator>();
        heiweapAnimator = GameObject.Find("HeiheiWeapon").GetComponent<Animator>();

        baibaiAnimator = GameObject.FindGameObjectWithTag("WhiteBoss").GetComponent<Animator>();
        baiweapAnimator = GameObject.Find("BaibaiWeapon").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Check if it is end game
        if (BossDeathCount >= 2)
        {
            // Insert Game End scene or smt
        }

        switch (enragedPhase)
        {
            case (int)EnragedPhase.normal:
                //The White Boss to follow and mirror the position of the black box.
                float distFromAnchor = anchorpoint.position.x - blackboss.position.x;
                float newX = blackboss.position.x + 2 * (distFromAnchor);
                whiteboss.position = new Vector3(newX, blackboss.position.y, -1);
                switch (currentPhase)
                {
                    case (int)BossPhase.idle: //idle
                        #region Set Up Idle phase Timer
                        if (firstcheckidle)
                        {
                            idleEnd = Time.time + idleDuration;
                            firstcheckidle = false;
                        }
                        #endregion

                        #region Set to move to the next phase when timer is up
                        if (Time.time >= idleEnd)
                        {
                            if (previousAttackPhase == (int)BossPhase.white)
                            {
                                currentPhase = (int)BossPhase.black;
                            }
                            else if (previousAttackPhase == (int)BossPhase.black)
                            {
                                currentPhase = (int)BossPhase.white;
                            }
                            else
                            {
                                currentPhase = (int)BossPhase.white;
                            }
                            firstcheckidle = true;
                        }
                        #endregion
                        break;


                    case (int)BossPhase.white: //white

                        #region 1. Set up timer + Charge Up animation by moving to the corner
                        if (firstcheckwhite)
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
                        GenerateSpikes();
                        #endregion

                        #region 3. Move player
                        // Check enemy is at the corner
                        // print("blackboss posiion: " + blackboss.position);
                        // print("anchorpoint position: " + nearestAnchor.position);
                        if (blackboss.position == nearestAnchor.position)
                        {
                            // Start moving player
                            fanDirection = player.transform.position - whiteboss.position;
                            playerRb2d.AddForce(fanDirection * fanthrust * 0.2f);
                            baibaiAnimator.SetBool("attacking", true);
                            baiweapAnimator.SetBool("attacking", true);
                        }
                        #endregion

                        #region 4. Checks if the time is up for the phase
                        if (Time.time >= whiteEnd)
                        {
                            // Go to idle phase
                            currentPhase = (int)BossPhase.idle;
                            // idleEnd = Time.time + idleDuration;
                            previousAttackPhase = (int)BossPhase.white;
                            firstcheckwhite = true;
                            isSpikesGenerated = false;
                            SetSpikesInvisible(spikeParent.transform);
                            baibaiAnimator.SetBool("attacking", false);
                            baiweapAnimator.SetBool("attacking", false);
                        }
                        #endregion

                        break;

                    case (int)BossPhase.black:
                        // TODO: @HUAN XUAN To start the sword animation for the Sign here
                        #region 1. Set up Phase timer
                        if (firstcheckblack)
                        {
                            blackEnd = Time.time + blackDuration;
                            firstcheckblack = false;
                            // print("IN Black phase");
                        }
                        #endregion

                        #region 2. Boss to chase player, as long as the sword hit, it deducts life
                        if (heiweapAnimator != null && heiweapAnimator.isActiveAndEnabled)
                        {
                            CheckDirection(blackboss, heiheiAnimator);
                        }
                        moveBoss(blackboss, blackbossRb2d, chaseSpeed);


                        #endregion

                        #region 3. Checks if the time is up for the phase
                        // Remember to end the animation here
                        if (Time.time >= blackEnd)
                        {
                            // Go to idle phase
                            currentPhase = (int)BossPhase.idle;
                            // idleEnd = Time.time + idleDuration;
                            previousAttackPhase = (int)BossPhase.black;
                            firstcheckblack = true;
                            heiweapAnimator.SetBool("attacking", false);
                            heiheiAnimator.SetBool("attacking", false);
                        }
                        #endregion
                        break;
                }
                break;

            case (int)EnragedPhase.enraged:
                // Sets the remaining boss as the enraged boss
                if (firstEnragedEnter)
                {
                    print("In Enraged Mode");


                    if (isBlackDead && !isWhiteDead)
                    {
                        print("White Boss Alive, Black Boss Dead");
                        enragedboss = whiteboss;
                        enragedbossrb2d = whitebossRb2d;
                        baibaiAnimator.SetBool("enraged", true);
                    }
                    else if (isWhiteDead && !isBlackDead)
                    {
                        print("Black Boss Alive, white Boss Dead");
                        enragedboss = blackboss;
                        enragedbossrb2d = blackbossRb2d;
                        heiheiAnimator.SetBool("enraged", true);
                    }
                    else
                    {
                        print("Unable to set enraged boss");
                    }
                    firstEnragedEnter = false;
                }

                switch (currentEnragePhase)
                {
                    case (int)EnragedCurrentPhase.franticfan:
                        if (firstfranticfan)
                        {
                            print("Enter Frantic Fan Mode");
                            franticfanEnd = Time.time + franticfanduration;
                            firstfranticfan = false;
                            isSpikesGenerated = false;
                        }

                        #region 1. Spawn Spikes
                        // TODO: Set a value for the number of spikes to generate here
                        GenerateSpikes();

                        #endregion

                        #region 2. Randomly move Boss
                        // Set eight points anchor points for the enemy to go to
                        if (!isfranticfanmove)
                        {
                            franticfanMoveEnd = Time.time + franticfanWaitDuration;
                            isfranticfanmove = true;
                            currentPosition = ChooseNextPosition();
                        }

                        if (Time.time >= franticfanMoveEnd)
                        {
                            isfranticfanmove = false;
                        }

                        float step = franticfanMoveSpeed * Time.deltaTime;
                        enragedboss.position = Vector3.MoveTowards(enragedboss.position, currentPosition.position, step);

                        #endregion

                        #region 3. Move player accordingly
                        // get the boss position and add force to the player
                        playerRb2d.AddForce((player.transform.position - enragedboss.position) * franticfanthrust * 0.2f);

                        #endregion

                        #region 4. Check if timer for phase is up
                        if (Time.time >= franticfanEnd)
                        {
                            // Go to the frantic hit phase
                            currentEnragePhase = (int)EnragedCurrentPhase.frantichit;
                            firstfranticfan = true;
                            isSpikesGenerated = false;
                            SetSpikesInvisible(spikeParent.transform);
                        }

                        #endregion
                        break;

                    case (int)EnragedCurrentPhase.frantichit:
                        if (firstfrantichit)
                        {
                            print("Enter Frantic HIT Mode");
                            frantichitEnd = Time.time + frantichitDuration;
                            firstfrantichit = false;
                        }

                        #region 1. Chase after player
                        // Chase after player
                        moveBoss(enragedboss, enragedbossrb2d, enragedspeed);
                        #endregion

                        #region 2. Check if timer for phase is up
                        if (Time.time >= frantichitEnd)
                        {
                            // Go to idle phases
                            currentEnragePhase = (int)EnragedCurrentPhase.idle;
                            firstfrantichit = true;
                        }
                        #endregion
                        break;

                    // case (int)EnragedCurrentPhase.youneedjesus:
                    //     break;

                    case (int)EnragedCurrentPhase.idle:
                        if (firstcheckidle_enraged)
                        {
                            print("Enter Enraged Idle Mode");
                            enragedidleEnd = Time.time + enragedidleDuration;
                            firstcheckidle_enraged = false;
                        }

                        if (Time.time >= enragedidleEnd)
                        {
                            //Go to the frantic fan stage
                            currentEnragePhase = (int)EnragedCurrentPhase.franticfan;
                            firstcheckidle_enraged = true;
                        }
                        break;
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
        print("Recieved Boss Death Event: " + message);
        if (message.Contains("BlackBoss"))
        {
            print("recieve black boss dead");
            isBlackDead = true;
            isWhiteDead = false;
        }
        else if (message.Contains("WhiteBoss"))
        {
            print("recieve white boss dead");
            isWhiteDead = true;
            isBlackDead = false;
        }
        enragedPhase = (int)EnragedPhase.enraged;
        BossDeathCount++;
    }

    public Transform ChooseNextPosition()
    {
        int numberOfEnemyPositions = spikeParent_enraged.transform.childCount;
        int randompos = Random.Range(0, numberOfEnemyPositions);
        return spikeParent_enraged.transform.GetChild(randompos);
    }
    public void GenerateSpikes()
    {
        while (!isSpikesGenerated)
        {
            print("Spikes Spawning");
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
    public void moveBoss(Transform boss, Rigidbody2D bossrb2d, float speed)
    {
        // print("Stopping Distance: " + stoppingDistance);
        if (Vector3.Distance(boss.position, player.transform.position) > stoppingDistance)
        {
            bossrb2d.velocity = (player.transform.position - boss.position) * speed * Time.deltaTime;
            //print("velocity: " + bossrb2d.velocity);
            if (heiweapAnimator != null && heiweapAnimator.isActiveAndEnabled && heiheiAnimator != null && heiweapAnimator.isActiveAndEnabled)
            {
                // CheckDirection(blackboss, heiheiAnimator);
                heiweapAnimator.SetBool("attacking", true);
                heiheiAnimator.SetBool("attacking", true);

            }

        }
        else if (Vector3.Distance(boss.position, player.transform.position) < stoppingDistance)
        {
            // if (heiweapAnimator != null && heiweapAnimator.isActiveAndEnabled)
            // {
            //     CheckDirection(blackboss, heiheiAnimator);
            // }
            bossrb2d.velocity = Vector2.zero;
        }

    }

    public void CheckDirection(Transform boss, Animator animator)
    {
        Vector3 change = boss.position - player.transform.position;
        if (change.x < 0 && !facingLeft)
        {
            facingLeft = true;
            animator.transform.Rotate(0, 180, 0);
        }
        if (change.x > 0 && facingLeft)
        {
            facingLeft = false;
            animator.transform.Rotate(0, 0, 0);
        }
    }
}
