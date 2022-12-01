using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// https://catlikecoding.com/unity/tutorials/movement/

// left off on "no wall jumping"


public class rigidbodyPhysicsTutorial : MonoBehaviour
{

    [SerializeField, Range(0f, 100f)]
        float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)]
        float maxAcceleration = 10f;
    [SerializeField, Range(0f, 10f)]
        float jumpHeight = 2f;
    private Vector2 playerInput;
    private Vector3 velocity, desiredVelocity;
    bool desiredJump;
    bool onGround;
    Rigidbody body;


    public void OnMove(InputAction.CallbackContext context)
    {
        playerInput = context.ReadValue<Vector2>();
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        desiredJump |= true;
    }



    public void InputCheck()
    {
        desiredVelocity = 
            new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        desiredJump |= desiredJump;
    }
    public void Move()
    {
        velocity = body.velocity;

        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        body.velocity = velocity;
    }
    public void Jump()
    {
        if (desiredJump) {
            desiredJump = false;

            if (onGround) {
            velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            }
        }
        body.velocity = velocity;
        onGround = false;
    }
    void OnCollisionEnter () {
        onGround = true;
    }

    void OnCollisionStay () {
        onGround = true;
    }


    void Awake() 
    {
        body = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        InputCheck();
    }
    void FixedUpdate()
    {
        Move();
        Jump();
    }
}
