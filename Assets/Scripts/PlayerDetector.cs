using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private Ghost[] ghostObjects;

    private bool isHunting;

    // Start is called before the first frame update
    void Start()
    {
        isHunting = true;

        if(playerObject == null) {
            Debug.LogError("The playerObject is null but is required for hunting to begin.");
        }

        if(ghostObjects == null || ghostObjects.Length == 0) {
            Debug.Log("There are no ghost objects listed to be checked.");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isHunting) {
            Vector3 dir = playerObject.transform.position - transform.position;
            if(Physics.Raycast(transform.position, dir.normalized, out RaycastHit hitInfo, 200)) {
                if(hitInfo.collider.gameObject == playerObject) {
                    Debug.Log("Player detected"); // Do something
                }
            }

            foreach(Ghost ghost in ghostObjects) {
                if(ghost != null) {
                    Vector3 ghostDir = ghost.transform.position - transform.position;
                    if (Physics.Raycast(transform.position, ghostDir.normalized, out RaycastHit hitInfo2, 200)) {
                        if (hitInfo2.collider.gameObject == ghost) {
                            Debug.Log("Ghost detected"); // Do something else
                            ghost.ResetPosition();
                        }
                    }
                } else {
                    Debug.Log("Missing gameObject in array.");
                }
            }
        }
    }

    public void ToggleHunting(bool toggle) {
        isHunting = toggle;
    }
}
