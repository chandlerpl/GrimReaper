using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private Ghost[] ghostObjects;
    [SerializeField]
    private Vector2 rotationMinMax = new Vector2(1, 180);
    [SerializeField]
    private float rotationSpeed = 1.0f;

    private bool isHunting;
    private bool back;

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
            Vector3 targetDir = playerObject.transform.position - transform.position;
            targetDir = targetDir.normalized;

            float dot = Vector3.Dot(targetDir, transform.forward);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (angle < 20) {
                if (Physics.Raycast(transform.position, targetDir, out RaycastHit hitInfo, 2000)) {
                    if (hitInfo.collider.gameObject == playerObject) {
                        Debug.Log("Player detected"); // Do something
                    }
                }

                foreach (Ghost ghost in ghostObjects) {
                    if (ghost != null) {
                        Vector3 ghostDir = ghost.transform.position - transform.position;
                        if (Physics.Raycast(transform.position, ghostDir.normalized, out RaycastHit hitInfo2, 2000)) {
                            if (hitInfo2.collider.gameObject == ghost.gameObject) {
                                Debug.Log("Ghost detected"); // Do something else
                                ghost.ResetPosition();
                            }
                        }
                    }
                    else {
                        Debug.Log("Missing gameObject in array.");
                    }
                }
            }
        }

        float y = transform.localEulerAngles.y;
        y += back ? -rotationSpeed : rotationSpeed;
/*
        if(y > rotationMinMax.y) {
            back = true;
        } else if(y < rotationMinMax.x) {
            back = false;
        }*/
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, y, transform.localEulerAngles.z);
    }

    public void ToggleHunting(bool toggle) {
        isHunting = toggle;
    }
}
