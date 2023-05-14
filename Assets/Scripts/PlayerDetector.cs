using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField]
    private PiedReaper playerObject;
    [SerializeField]
    private Vector2 rotationMinMax = new Vector2(1, 180);
    [SerializeField]
    private float rotationSpeed = 1.0f;
    [SerializeField]
    private TextMeshProUGUI healthText;

    private bool isHunting;
    private bool back;
    private int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        isHunting = false;

        if(playerObject == null) {
            Debug.LogError("The playerObject is null but is required for hunting to begin.");
        }
        healthText.text = health + "";
    }

    private float time;

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
                        if(Time.time > time) {
                            if(--health <= 0) {
                                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                            } else {
                                time = Time.time + 5;
                                healthText.text = health + "";
                            }
                        }
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
