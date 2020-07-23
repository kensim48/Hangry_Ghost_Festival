using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    ControllerScheme controls;
    Vector2 leftMove;
    Vector2 rightMove;
    private Rigidbody2D rb;
    private Rigidbody2D rbLeftArm;
    private ArmsClass leftArm;
    private ArmsClass rightArm;
    private GameObject leftBooster;
    private GameObject rightBooster;
    GameObject boosterTrail;
    GameObject amplifierLine;
    bool leftBoosterForce;
    bool rightBoosterForce;
    bool leftRotationActive;
    bool rightRotationActive;
    float leftSwingSpeed;
    float rightSwingSpeed;
    Quaternion leftAim;
    Quaternion rightAim;
    public float rotateSpeed = 10;
    bool lockdownToggle = false;
    public ArmsClass[] armsList;
    public int arm1;
    private int arm1Last;
    public int arm2;
    private int arm2Last;
    public float maxSpeed;//Replace with your max speed

    private Quaternion lastRotation;

    void Awake()
    {
        lastRotation = transform.rotation;
        arm1Last = arm1;
        arm2Last = arm2;
        leftArm = Instantiate(armsList[arm1]);
        leftArm.transform.parent = transform;
        rightArm = Instantiate(armsList[arm2]);
        rightArm.transform.parent = transform;
        controls = new ControllerScheme();
        rb = GetComponent<Rigidbody2D>();
        controls.Gameplay.LeftTrigger.performed += ctx =>
        {
            leftBoosterForce = true;
            leftSwingSpeed = 0.0f;
        };
        controls.Gameplay.LeftTrigger.canceled += ctx => leftBoosterForce = false;
        controls.Gameplay.RightTrigger.performed += ctx =>
        {
            rightBoosterForce = true;
            rightSwingSpeed = 0.0f;
        };
        controls.Gameplay.RightTrigger.canceled += ctx => rightBoosterForce = false;
        controls.Gameplay.RightShoulder.performed += ctx => lockdownToggle = !lockdownToggle;
        // controls.Gameplay.RightShoulder.performed += ctx => ;
        controls.Gameplay.LeftArmMovement.performed += ctx =>
        {
            leftMove = ctx.ReadValue<Vector2>();
            leftRotationActive = true;
            leftSwingSpeed = 0.0f;
        };
        controls.Gameplay.LeftArmMovement.canceled += ctx => leftRotationActive = false;
        controls.Gameplay.RightArmMovement.performed += ctx =>
        {
            rightMove = ctx.ReadValue<Vector2>();
            rightRotationActive = true;
            rightSwingSpeed = 0.0f;
        };
        controls.Gameplay.RightArmMovement.canceled += ctx => rightRotationActive = false;
    }

    void Update()
    {
        transform.rotation = lastRotation;
    }
    void FixedUpdate()
    {
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
        leftAim = Quaternion.Euler(Vector3.forward * GetAngle(new Vector3(-leftMove.x, leftMove.y, 0)));
        rightAim = Quaternion.Euler(Vector3.forward * GetAngle(new Vector3(-rightMove.x, rightMove.y, 0)));
        if (rightRotationActive) rightArm.transform.rotation = Quaternion.RotateTowards(rightArm.transform.rotation, rightAim, rotateSpeed);
        if (leftRotationActive) leftArm.transform.rotation = Quaternion.RotateTowards(leftArm.transform.rotation, leftAim, rotateSpeed);
        if (leftBoosterForce)
        {
            leftArm.Attack();
            // if notlocked
            if (!lockdownToggle) leftArm.Move();
        }
        if (rightBoosterForce)
        {
            rightArm.Attack();
            // if notlocked
            if (!lockdownToggle) rightArm.Move();
        }
        if (lockdownToggle) rb.velocity = Vector2.zero;
        if (rb.velocity.magnitude > maxSpeed)
        {
            print("toofast");
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

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
}
