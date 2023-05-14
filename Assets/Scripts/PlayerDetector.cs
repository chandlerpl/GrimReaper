using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField]
    private PiedReaper playerObject;
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
                    if (hitInfo.collider.gameObject == playerObject.gameObject) {
                        Debug.Log("Player detected"); // Do something
                    }
                }

                for(int i = 0; i < playerObject.followers.Count; i++) {
                    Ghost ghost = playerObject.followers[i];

                    if (ghost != null) {
                        Vector3 ghostDir = ghost.transform.position - transform.position;
                        if (Physics.Raycast(transform.position, ghostDir.normalized, out RaycastHit hitInfo2, 2000)) {
                            if (hitInfo2.collider.gameObject == ghost.gameObject) {
                                Debug.Log("Ghost detected"); // Do something else
                                GameManager.Instance.reaper.RemoveGhost(ghost);
                                --i;
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
