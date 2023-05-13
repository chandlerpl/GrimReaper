using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PiedReaper : MonoBehaviour
{
    public GameObject[] followers;  // Array of follower objects
    public float lerpDuration = 1f;  // Duration of the lerping animation
    public float delayPerFollower = 1f;  // Delay per follower in the array
    public float baseMinDistance = 2.0f;  // Base minimum distance for the first follower
    public float maxSpeed = 5f;  // Maximum speed of the followers
    public float lerpSpeed = 1f;  // Speed of the lerping movement

    private Transform playerTransform;  // Reference to the player's transform
    private Vector3[] initialPositions;  // Initial positions of the followers
    private float[] delays;  // Delays for each follower
    private float[] minDistances;  // Minimum distances for each follower

    private void Start()
    {
        playerTransform = transform;  // Assign the player's transform

        // Initialize arrays
        initialPositions = new Vector3[followers.Length];
        delays = new float[followers.Length];
        minDistances = new float[followers.Length];

        // Calculate the total delay for all followers
        float totalDelay = delayPerFollower * (followers.Length - 1);

        // Calculate the minimum distance for each follower
        for (int i = 0; i < followers.Length; i++)
        {
            minDistances[i] = baseMinDistance + i;  // Increase the base minimum distance by 1.0 for each follower
        }

        // Store initial positions and calculate delays
        for (int i = 0; i < followers.Length; i++)
        {
            initialPositions[i] = followers[i].transform.position;
            delays[i] = i * delayPerFollower - totalDelay;
        }
    }

    private void Update()
    {
        for (int i = 0; i < followers.Length; i++)
        {
            float t = Mathf.Clamp01((Time.time - delays[i]) / lerpDuration);  // Calculate the interpolation parameter

            // Calculate the distance between the follower and the player
            float distance = Vector3.Distance(followers[i].transform.position, playerTransform.position);

            // Calculate the speed based on the distance
            float speed = maxSpeed;

            Vector3 targetPosition;

            if (distance < minDistances[i])
            {
                speed = 0f;
                targetPosition = followers[i].transform.position; // Set current position as the target
            }
            else
            {
                targetPosition = playerTransform.position; // Set player's position as the target
            }

            // Lerp the follower towards the target position with the adjusted speed
            followers[i].transform.position = Vector3.Lerp(followers[i].transform.position, targetPosition, t * lerpSpeed * speed);
        }
    }



    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(playerTransform.position, 1f);


        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Ghost"))
            {
                GameObject ghostObject = collider.gameObject;
                if (!Array.Exists(followers, follower => follower == ghostObject))
                {
                    // Add the ghost object to the followers array
                    List<GameObject> newFollowers = new List<GameObject>(followers);
                    newFollowers.Add(ghostObject);
                    followers = newFollowers.ToArray();

                    // Add initial position and delay for the new follower
                    Vector3[] newInitialPositions = new Vector3[initialPositions.Length + 1];
                    initialPositions.CopyTo(newInitialPositions, 0);
                    newInitialPositions[initialPositions.Length] = ghostObject.transform.position;
                    initialPositions = newInitialPositions;

                    float[] newDelays = new float[delays.Length + 1];
                    delays.CopyTo(newDelays, 0);
                    newDelays[delays.Length] = (delays.Length - 1) * delayPerFollower;
                    delays = newDelays;

                    float[] newMinDistances = new float[minDistances.Length + 1];
                    minDistances.CopyTo(newMinDistances, 0);
                    newMinDistances[minDistances.Length] = baseMinDistance + (minDistances.Length - 1);
                    minDistances = newMinDistances;

                    Debug.Log("Added new follower: " + ghostObject.name + " (Index: " + (followers.Length - 1) + ")");
                }
            }


            else if (collider.CompareTag("Body"))
            {
                GameObject bodyObject = collider.gameObject;

                if (bodyObject.name == "RedBody" && followers.Length > 0 && followers[0].name == "RedGhost")
                {
                    GameObject removedFollower = followers[0];
                    followers = followers.Skip(1).ToArray();

                    Debug.Log("Removed follower: " + removedFollower.name);

                    removedFollower.SetActive(false);  // Disable the follower object
                    bodyObject.SetActive(false);  // Disable the body object

                    Debug.Log("Destroyed body object: " + bodyObject.name);
                }

                if (bodyObject.name == "BlueBody" && followers.Length > 0 && followers[0].name == "BlueGhost")
                {
                    GameObject removedFollower = followers[0];
                    followers = followers.Skip(1).ToArray();

                    Debug.Log("Removed follower: " + removedFollower.name);

                    removedFollower.SetActive(false);  // Disable the follower object
                    bodyObject.SetActive(false);  // Disable the body object

                    Debug.Log("Destroyed body object: " + bodyObject.name);
                }

                if (bodyObject.name == "GreenBody" && followers.Length > 0 && followers[0].name == "GreenGhost")
                {
                    GameObject removedFollower = followers[0];
                    followers = followers.Skip(1).ToArray();

                    Debug.Log("Removed follower: " + removedFollower.name);

                    removedFollower.SetActive(false);  // Disable the follower object
                    bodyObject.SetActive(false);  // Disable the body object

                    Debug.Log("Destroyed body object: " + bodyObject.name);
                }
            }
        }
    }
}
