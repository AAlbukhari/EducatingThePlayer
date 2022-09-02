using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // How fast the player moves, and at what speed the player's velocity (speed) caps out
    [SerializeField] private float speed, speedCap;
    [SerializeField] private Transform cameraReference;

    // The player's Rigidbody
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Moves the player forward and backwards using WASD
        Vector3 movement = new Vector3((Input.GetAxis("Horizontal") * speed), 0, (Input.GetAxis("Vertical") * speed));

        // Bases the player's directional movement on which way the camera is facing
        movement = cameraReference.TransformDirection(movement);

        //Two methods of movement: Velocity and AddForce. Choose one or the other
        //rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        rb.AddForce(movement * speed);
    }

    private void FixedUpdate()
    {
        // If the player's velocity reach higher than the speed cap, then stop the player from gaining anymore speed
        if (rb.velocity.magnitude > speedCap)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, speedCap);
        }

        // Locks our camera reference's Transform to 0 
        cameraReference.eulerAngles = new Vector3(0, cameraReference.eulerAngles.y, cameraReference.eulerAngles.z);
    }
}