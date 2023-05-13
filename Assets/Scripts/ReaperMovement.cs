using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ReaperMovement : MonoBehaviour
{

    private Transform playerTransform;  // Reference to the player's transform

    public float maxSpeed = 5f;  // Maximum speed of the player
    public float shimmyMod;

    private void Start()
    {
        playerTransform = transform;  // Assign the player's transform
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
        // implement movement
        playerTransform.position += movement;
    }

    // Move the player based on input - 3D
    private Vector3 GetMovement3D()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        return new Vector3(horizontalInput, 0f, verticalInput) * maxSpeed * Time.deltaTime;
    }

    // Move the player based on input - 2D
    private Vector3 GetMovement2D()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        return new Vector3(horizontalInput, 0f, 0f) * maxSpeed * Time.deltaTime * shimmyMod;
    }

}
