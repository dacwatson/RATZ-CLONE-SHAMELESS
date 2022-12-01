using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTutorial : MonoBehaviour
{
        // these two variables are for tracking mouse movement
    private float yaw = 0.0f, pitch = 0.0f;
        // this variable simply creates a reference to the rigidbody in our code
    private Rigidbody rb; 

        // serialize field makes variable visible in editor
        // the N.Nf notation allows the number to not be typed as int or dbl
    [SerializeField] float walkSpeed = 5.0f, sensitivity = 2.0f;


        // Start() is called when the script is initialized
    void Start()
    {
            // this locks the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
            // this "gets a reference to our rigidbody component" (meaning?)
        rb = gameObject.GetComponent<Rigidbody>();
    }


    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && Physics.Raycast(rb.transform.position, Vector3.down, 1 + 0.001f))
            rb.velocity = new Vector3(rb.velocity.x, 5.0f, rb.velocity.z);
        Look();
    }


    private void FixedUpdate()
    {
        Movement();
    }

        // created a function look and added it to looping "update" function
    void Look()
    {
            // mouse Y axis generates positive float on upward movement, but is
            // inverted in unity so subtract from pitch.
        pitch -= Input.GetAxisRaw("Mouse Y") * sensitivity;
            // prevents player from doing 360's (playtest removing this? GTTOD)
        pitch = Mathf.Clamp(pitch, -90.0f, 90.0f);

            // nothing fancy needed, just get X movement
        yaw += Input.GetAxisRaw("Mouse X") * sensitivity;

            // apply the pitch and yaw transformations we just calculated and apply
            // them as transforms to camera. Internally Unity uses "Quaternions"
            // to represent rotations so that's how we apply it.
            // https://docs.unity3d.com/ScriptReference/Quaternion.html
            // https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
            //    v transform script                           v plug in values
        Camera.main.transform.localRotation = Quaternion.Euler(pitch, yaw, 0);
    }

    void Movement()
    {
    // https://learn.unity.com/tutorial/getaxis-o#5c8a647fedbc2a0020d980a4
    // Above video explains the differences between getting button presses and GetAxis.
    // GetAxis allows to tweak how movement feels using gravity, etc.

        // a Vector2 is a 2D vector.
            // define horizontal and vertical axes and multiply by walkspeed
            // Input.GetAxis not raw is used so unity auto smooths input
        Vector2 axis = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * walkSpeed;

        // Vector 3 is 3D vector, aka X Y Z
            // forward vector is taken from right (90 deg) vector so player doesn't fly when looking up
            // also we manually move camera object NOT rb as rb translation causes jitteriness
                                    //        x                        y                   z
        Vector3 forward = new Vector3(-Camera.main.transform.right.z, 0.0f, Camera.main.transform.right.x);
        
        // axis.x and axis.y referencing first and second element of 'axis'
        // combination of (pressing w/s and 'forward' vector) + (pressing a/d and camera right vector) and we add camera's y velocity. 
        Vector3 wishDirection = (forward * axis.x + Camera.main.transform.right * axis.y + Vector3.up * rb.velocity.y);
        rb.velocity = wishDirection;
    }


    // void Boost()
    // {
    //     rb.velocity -= Camera.localRotation;
    // }

}
