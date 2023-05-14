using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class PiedReaper : MonoBehaviour
{
    public List<Ghost> followers;  // Array of follower objects
    public float lerpDuration = 1f;  // Duration of the lerping animation
    public float delayPerFollower = 1f;  // Delay per follower in the array
    public float baseMinDistance = 2.0f;  // Base minimum distance for the first follower
    public float maxSpeed = 5f;  // Maximum speed of the followers
    public float lerpSpeed = 1f;  // Speed of the lerping movement

    public GameObject[] objectsArray; // Array of objects
    public Text uiText; // Reference to the UI Text component
	private HashSet<GameObject> disabledObjects; // Set of disabled objects


    private void UpdateUI()
    {
        uiText.text = "Ghosts Saved: " + disabledObjects.Count;
    }

    private void Start()
    {
        disabledObjects = new HashSet<GameObject>();

        UpdateUI();
    }

    private void Update()
    {

        foreach (GameObject obj in objectsArray)
        {
            if (!obj.activeSelf && !disabledObjects.Contains(obj))
            {
                disabledObjects.Add(obj);
            }
        }

        UpdateUI();



        for (int i = 0; i < followers.Count; i++)
        {
            // Calculate the distance between the follower and the player
            float distance = Vector3.Distance(followers[i].transform.position, transform.position);

            // Calculate the speed based on the distance
            float speed = maxSpeed;

            if (distance > baseMinDistance + (i * delayPerFollower))
            {
                followers[i].transform.position = Vector3.Lerp(followers[i].transform.position, transform.position, lerpDuration);
            }
            else {

            }
        }
    }



    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);


        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Ghost"))
            {
                GameObject ghostObject = collider.gameObject;
                Ghost ghost = ghostObject.GetComponent<Ghost>();
                AddGhost(ghost);
            }


            else if (collider.CompareTag("Body"))
            {
                GameObject bodyObject = collider.gameObject;

                if (bodyObject.name == "RedBody" && followers.Count > 0 && followers[0].name == "RedGhost")
                {
                    GameObject removedFollower = followers[0].gameObject;
                    followers.RemoveAt(0);

                    Debug.Log("Removed follower: " + removedFollower.name);

                    removedFollower.SetActive(false);  // Disable the follower object
                    bodyObject.SetActive(false);  // Disable the body object

                    Debug.Log("Destroyed body object: " + bodyObject.name);
                }

                if (bodyObject.name == "BlueBody" && followers.Count > 0 && followers[0].name == "BlueGhost")
                {
                    GameObject removedFollower = followers[0].gameObject;
                    followers.RemoveAt(0);

                    Debug.Log("Removed follower: " + removedFollower.name);

                    removedFollower.SetActive(false);  // Disable the follower object
                    bodyObject.SetActive(false);  // Disable the body object

                    Debug.Log("Destroyed body object: " + bodyObject.name);
                }

                if (bodyObject.name == "GreenBody" && followers.Count > 0 && followers[0].name == "GreenGhost")
                {
                    GameObject removedFollower = followers[0].gameObject;
                    followers.RemoveAt(0);

                    Debug.Log("Removed follower: " + removedFollower.name);

                    removedFollower.SetActive(false);  // Disable the follower object
                    bodyObject.SetActive(false);  // Disable the body object

                    Debug.Log("Destroyed body object: " + bodyObject.name);
                }
            }
        }
    }


    public void AddGhost(Ghost ghost) {
        if (!followers.Contains(ghost)) {
            followers.Add(ghost);

            Debug.Log("Added new follower: " + ghost.gameObject.name + " (Index: " + (followers.Count - 1) + ")");
        }
    }

    public void RemoveGhost(Ghost ghost) {
        followers.Remove(ghost);

        ghost.ResetPosition();
    }
}
