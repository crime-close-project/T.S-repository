using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
// this is player movement orientation is a empty game object that keeps track of direction 
public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5f;
    public bool smoothStop = false;            // toggle for smooth stopping
    public float stopLerpSpeed = 10f;          // only used when smoothStop == true

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 MoveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError($"Rigidbody not found on {gameObject.name}. Attach a Rigidbody.");
            enabled = false;
            return;
        }

        if (orientation == null)
        {
            Debug.LogError("Orientation Transform not assigned on PlayerMovement.");
            enabled = false;
            return;
        }

        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput(); 
    }

    private void MyInput()
    {
        // horizontal and vertical input 
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // if theres no input the player immediatley stops moving 
        if (Mathf.Approximately(horizontalInput, 0f) && Mathf.Approximately(verticalInput, 0f))
        {
            StopMovement();
        }
        else
        {
            MovePlayer();
        }

        SpeedControl();
    }

    private void MovePlayer()
    {
        // moves the player (shocker i know :O)
        MoveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(MoveDirection.normalized * playerSpeed * 10f, ForceMode.Force);
    }

    private void StopMovement()
    {
        // Keep vertical (Y) velocity (gravity/jumps) but clear horizontal plane velocity.
        if (smoothStop)
        {
            // Smoothly lerp the horizontal velocity to zero.
            Vector3 vel = rb.linearVelocity;
            Vector3 flatVel = new Vector3(vel.x, 0f, vel.z);
            Vector3 targetFlat = Vector3.Lerp(flatVel, Vector3.zero, stopLerpSpeed * Time.fixedDeltaTime);
            rb.linearVelocity = new Vector3(targetFlat.x, vel.y, targetFlat.z);
        }
        else
        {
            // Immediate stop on XZ plane
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            // Also clear any residual forces if desired:
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void SpeedControl()
    {// helps to stop slipping 
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVel.magnitude > playerSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * playerSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
}
