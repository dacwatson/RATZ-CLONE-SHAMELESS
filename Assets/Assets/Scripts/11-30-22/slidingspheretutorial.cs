using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// https://catlikecoding.com/unity/tutorials/movement/


public class slidingspheretutorial : MonoBehaviour
{

    private Vector2 playerInput;
    [SerializeField, Range(0f, 100f)]
        float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)]
        float maxAcceleration = 10f;
    private Vector3 velocity;


    public void OnMove(InputAction.CallbackContext context)
    {
        playerInput = context.ReadValue<Vector2>();
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
    }
    public void Move()
    {
        // Vector3 acceleration = 
        //     new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        // velocity += acceleration * Time.deltaTime;
        // Vector3 displacement = velocity * Time.deltaTime;
        // transform.localPosition += displacement;

        Vector3 desiredVelocity = 
            new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        Vector3 displacement = velocity * Time.deltaTime;
        transform.localPosition += displacement;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
