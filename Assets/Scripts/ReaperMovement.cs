using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

[RequireComponent(typeof(Rigidbody))]
public class ReaperMovement : MonoBehaviour
{

    private Transform playerTransform;  // Reference to the player's transform

    public float maxSpeed = 5f;  // Maximum speed of the player
    public float shimmyMod;

    private Rigidbody body;

    private void Start()
    {
        playerTransform = transform;  // Assign the player's transform
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // determine movement
        Vector3 movement;
        if (!gameObject.GetComponent<Hide>().GetIs2D())
        // 3D move
        {
            movement = GetMovement3D();
        }
        else
        // 2D move
        {
            movement = GetMovement2D();
        }

        body.AddForce(movement); 
        // implement movement
        //playerTransform.position += movement;
    }

    // Move the player based on input - 3D
    private Vector3 GetMovement3D()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        return ((transform.forward * verticalInput) + (transform.right * horizontalInput)) * maxSpeed * Time.deltaTime;
    }

    // Move the player based on input - 2D
    private Vector3 GetMovement2D()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        return new Vector3(horizontalInput, 0f, 0f) * maxSpeed * Time.deltaTime * shimmyMod;
    }

}
