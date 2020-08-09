using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    // Note: Due to some changes, arm1 and leftarm are used interchangeably, same for arm2 and right arm
    // Control scheme variables
    ControllerScheme controls;
    Vector2 leftMove;
    Vector2 rightMove;
    bool leftBoosterForce;
    bool rightBoosterForce;
    bool leftRotationActive;
    bool rightRotationActive;
    Quaternion leftAim;
    Quaternion rightAim;
    // Rigidbody of player
    private Rigidbody2D rb;
    // IDK what variable is this, figure out later
    public float rotateSpeed = 10;
    // List of available arms
    public ArmsClass[] armsList;
    // List of available destroyable arm variants, len(armsList) == len(destroyableArmsList)
    public GameObject[] destroyableArmsList;
    // Next 4 varables to handle numbering of arms(not arm objects), to assist in swapping of arm numbers
    public int arm1;
    private int arm1Last;
    public int arm2;
    private int arm2Last;
    // left and right arm objects, these are the actual arms
    private ArmsClass leftArm;
    private ArmsClass rightArm;
    // Maximum speed of the player
    public float maxSpeed;
    // Toggles explosive arm mode
    private bool arm1ExplosiveArmed = false;
    private bool arm2ExplosiveArmed = false;
    // Additional object references to handle tilting of camera
    public GameObject mainBase;
    public GameObject cameraAngle;
    // Assist in locking of player body's rotation
    private Quaternion lastRotation;
    public GameObject inventoryBarSlots;
    public GameObject inventoryBarSlotSelector1;
    public GameObject inventoryBarSlotSelector2;
    public int[] weaponInventory;
    public int weaponSlot1;
    public int weaponSlot2;
    public int maxWeaponSlot = 8;
    private bool isLeftSelected = false;
    private bool isRightSelected = false;
    private int weaponSelectionCounter = 0;
    private int weaponSelectionCountereMax = 7;
    public GameObject weaponSpriteLayer;
    public Sprite[] weaponSpriteList;
    void Awake()
    {

        // Tilting of player body to be that of camera angle   
        mainBase.transform.rotation = cameraAngle.transform.rotation;
        // Locking of player's body rotation
        lastRotation = transform.rotation;
        // Creation and association of arms
        arm1Last = arm1;
        arm2Last = arm2;
        leftArm = Instantiate(armsList[arm1]);
        leftArm.transform.parent = transform;
        leftArm.transform.position = transform.position;
        rightArm = Instantiate(armsList[arm2]);
        rightArm.transform.parent = transform;
        rightArm.transform.position = transform.position;
        // Initial inventory UI setup
        inventoryBarSlotSelector1.transform.position = inventoryBarSlots.transform.GetChild(weaponSlot1).position;
        inventoryBarSlotSelector2.transform.position = inventoryBarSlots.transform.GetChild(weaponSlot2).position;

        refreshWeaponSprites();

        // Getting rigidbody of Player
        rb = GetComponent<Rigidbody2D>();

        // Control scheme input system
        controls = new ControllerScheme();
        controls.Gameplay.LeftTrigger.performed += ctx =>
        {
            leftBoosterForce = true;
        };
        controls.Gameplay.LeftTrigger.canceled += ctx => leftBoosterForce = false;
        controls.Gameplay.RightTrigger.performed += ctx =>
        {
            rightBoosterForce = true;
        };
        controls.Gameplay.RightTrigger.canceled += ctx => rightBoosterForce = false;
        controls.Gameplay.LeftArmMovement.performed += ctx =>
        {
            leftMove = ctx.ReadValue<Vector2>();
            leftRotationActive = true;
        };
        controls.Gameplay.LeftArmMovement.canceled += ctx => leftRotationActive = false;
        controls.Gameplay.RightArmMovement.performed += ctx =>
        {
            rightMove = ctx.ReadValue<Vector2>();
            rightRotationActive = true;
        };
        controls.Gameplay.RightArmMovement.canceled += ctx => rightRotationActive = false;
        controls.Gameplay.LeftPrime.performed += ctx => arm1ExplosiveArmed = !arm1ExplosiveArmed;
        controls.Gameplay.WeaponSwapLeft.performed += ctx => isLeftSelected = true;
        controls.Gameplay.WeaponSwapLeft.canceled += ctx => isLeftSelected = false;
        controls.Gameplay.WeaponSwapRight.performed += ctx => isRightSelected = true;
        controls.Gameplay.WeaponSwapRight.canceled += ctx => isRightSelected = false;
    }

    void Update()
    {
        // Locking of player's body rotation
        transform.rotation = lastRotation;
    }
    void FixedUpdate()
    {
        // Getting aim of left and right analogue sticks
        leftAim = Quaternion.Euler(Vector3.forward * GetAngle(new Vector3(-leftMove.x, leftMove.y, 0)));
        rightAim = Quaternion.Euler(Vector3.forward * GetAngle(new Vector3(-rightMove.x, rightMove.y, 0)));
        // Checking for changes in arms
        if (arm1 != arm1Last)
        {
            Destroy(leftArm.gameObject);
            leftArm = Instantiate(armsList[arm1]);
            leftArm.transform.position = transform.position;
            leftArm.transform.parent = transform;
            arm1Last = arm1;
        }
        if (arm2 != arm2Last)
        {
            Destroy(rightArm.gameObject);
            rightArm = Instantiate(armsList[arm2]);
            rightArm.transform.position = transform.position;
            rightArm.transform.parent = transform;
            arm2Last = arm2;
        }
        // Inventory code
        if ((isLeftSelected || isRightSelected) && (Mathf.Abs(rightMove.x) > 0.5f || Mathf.Abs(rightMove.y) > 0.5f))
        {
            if (weaponSelectionCounter > weaponSelectionCountereMax)
            {
                if (isLeftSelected)
                {
                    int orig_weaponSlot1 = weaponSlot1;
                    if (rightMove.x > 0.5f)
                    {
                        weaponSlot1 += weaponSlot1 + 1 == weaponSlot2 ? 2 : 1;
                        weaponSlot1 = weaponSlot1 < maxWeaponSlot ? weaponSlot1 : orig_weaponSlot1;
                    }
                    else if (rightMove.x < -0.5f)
                    {
                        weaponSlot1 -= weaponSlot1 - 1 == weaponSlot2 ? 2 : 1;
                        weaponSlot1 = weaponSlot1 >= 0 ? weaponSlot1 : orig_weaponSlot1;
                    }
                }
                else if (isRightSelected)
                {
                    int orig_weaponSlot2 = weaponSlot2;
                    if (rightMove.x > 0.5f)
                    {
                        weaponSlot2 += weaponSlot2 + 1 == weaponSlot1 ? 2 : 1;
                        weaponSlot2 = weaponSlot2 < maxWeaponSlot ? weaponSlot2 : orig_weaponSlot2;
                    }
                    else if (rightMove.x < -0.5f)
                    {
                        weaponSlot2 -= weaponSlot2 - 1 == weaponSlot1 ? 2 : 1;
                        weaponSlot2 = weaponSlot2 >= 0 ? weaponSlot2 : orig_weaponSlot2;
                    }
                }
                inventoryBarSlotSelector1.transform.position = inventoryBarSlots.transform.GetChild(weaponSlot1).position;
                inventoryBarSlotSelector2.transform.position = inventoryBarSlots.transform.GetChild(weaponSlot2).position;
                arm1 = weaponInventory[weaponSlot1];
                arm2 = weaponInventory[weaponSlot2];
                weaponSelectionCounter = 0;
            }
            weaponSelectionCounter++;
        }
        else
        {
            weaponSelectionCounter = weaponSelectionCountereMax;
            // Only rotate if analogue stick is actively pressed
            if (rightRotationActive)
            {
                // Rotation of arm to be that of analogue
                rightArm.transform.rotation = Quaternion.RotateTowards(rightArm.transform.rotation, rightAim, rotateSpeed);
                // Modification of rotation to compensate for camera tilt
                rightArm.transform.eulerAngles = new Vector3(
         rightArm.transform.eulerAngles.x + cameraAngle.transform.eulerAngles.x,
         rightArm.transform.eulerAngles.y,
         rightArm.transform.eulerAngles.z
        );
            }
            if (leftRotationActive)
            {
                // Rotation of arm to be that of analogue
                leftArm.transform.rotation = Quaternion.RotateTowards(leftArm.transform.rotation, leftAim, rotateSpeed);
                // Modification of rotation to compensate for camera tilt
                leftArm.transform.eulerAngles = new Vector3(
    leftArm.transform.eulerAngles.x + cameraAngle.transform.eulerAngles.x,
    leftArm.transform.eulerAngles.y,
    leftArm.transform.eulerAngles.z
    );
            }
            // Run this part if the explosive toggle is set (clicking of sticks)
            if (leftBoosterForce && arm1ExplosiveArmed)
            {
                // Create a new object of the Destroyable arm section, and place it in the exact postion of the current arm
                GameObject destroyableArm = Instantiate(destroyableArmsList[arm1]);
                destroyableArm.transform.position = transform.position;
                destroyableArm.transform.rotation = leftArm.transform.rotation;
                // Destroy current arm
                arm1 = 0;
                arm1ExplosiveArmed = false;
            }
            if (rightBoosterForce && arm2ExplosiveArmed)
            {
                GameObject destroyableArm = Instantiate(destroyableArmsList[arm2]);
                destroyableArm.transform.position = transform.position;
                destroyableArm.transform.rotation = rightArm.transform.rotation;
                arm2 = 0;
                arm2ExplosiveArmed = false;
            }

            // Runs attack and move if trigger is clicked in
            if (leftBoosterForce)
            {
                leftArm.Attack();
                leftArm.Move();
            }
            if (rightBoosterForce)
            {
                rightArm.Attack();
                rightArm.Move();
            }
            // Speed limiter
            if (rb.velocity.magnitude > maxSpeed)
            {
                print("Player is too fast, limiting speed");
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }

    }

    // Control scheme functions
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Function for handling analogue to character arms conversion
    public static float GetAngle(Vector2 p_vector2)
    {
        if (p_vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        }
    }
    public void refreshWeaponSprites()
    {
        for (int i = 0; i < 8; i++)
            weaponSpriteLayer.transform.GetChild(i).GetComponent<Image>().sprite = weaponSpriteList[weaponInventory[i]];
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin_1"))
        {
            Debug.Log("Coin touch");
            Destroy(other.gameObject);
            GameObject.FindGameObjectWithTag("Score").GetComponent<PlayerStats>().playerScore += 10;
		}
	}
}
