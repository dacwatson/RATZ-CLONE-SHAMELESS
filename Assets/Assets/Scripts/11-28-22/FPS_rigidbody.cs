using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



// https://www.youtube.com/watch?v=_QajrabyTJc&t=964s
// https://www.youtube.com/watch?v=1LtePgzeqjQ&t=1337s


public class FPS_rigidbody : MonoBehaviour
{

    // variables
    public Rigidbody rb;
    public GameObject camHolder;
    [SerializeField] float walkSpeed = 5.0f, sensitivity = 2.0f, maxForce = 99.0f, jumpForce = 5.0f;
    private Vector2 move, look;
    private float lookRotation;
    public bool grounded;


    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        Look();
    }



    private void FixedUpdate() 
    {
        Move();
    } 

    void Look()
    {
        // turn player
        transform.Rotate(Vector3.up * look.x * sensitivity);

        // look
        lookRotation += (-look.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);
        camHolder.transform.eulerAngles = new Vector3(lookRotation, camHolder.transform.eulerAngles.y, camHolder.transform.eulerAngles.z);
    }
    void Move()
    {
        // find target velocity
        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = 
            new Vector3(move.x, 0, move.y) * walkSpeed;

        // align direction
        targetVelocity = transform.TransformDirection(targetVelocity);

        // forces
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        // limit force
        Vector3.ClampMagnitude(velocityChange, maxForce);


        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }
    void Jump()
    {
        Vector3 jumpForces = Vector3.zero;

        if(grounded)
        {
            jumpForces = Vector3.up * jumpForce;
        }

        rb.AddForce(jumpForces, ForceMode.VelocityChange);
    }
    public void SetGrounded(bool state)
    {
        grounded = state;
    }

}
