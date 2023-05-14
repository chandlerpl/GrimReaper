using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    bool is2D = false;
    public GameObject cam3D, cam2D;

    private ThirdPersonController controller;
    // Start is called before the first frame update
    void Start()
    {
        // Disable one camera
        cam2D.gameObject.SetActive(false);
        controller = GetComponent<ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            is2D = !is2D; // flip it

            if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), Vector3.forward, 1f, LayerMask.GetMask("Building"))) {
                cam2D.transform.localEulerAngles = new Vector3 (0, 180, 0);
            } else if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), -Vector3.forward, 1f, LayerMask.GetMask("Building"))) {
                cam2D.transform.localEulerAngles = new Vector3(0, 0, 0);
            } else if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), Vector3.right, 1f, LayerMask.GetMask("Building"))) {
                cam2D.transform.localEulerAngles = new Vector3(0, -90, 0);
            } else {
                is2D = !is2D;
            }

            // switch camera
            SwitchCamera();
        }

        if(is2D) {
            GameManager.Instance.playerDetector.ToggleHunting(false);
            controller.enabled = false;
            Debug.Log("2D View Active");
        } else {
            GameManager.Instance.playerDetector.ToggleHunting(true);
            controller.enabled = true;
            Debug.Log("3D View Active");
        }
    }

    void SwitchCamera()
    {
        // Flip each camera's active state
        cam3D.gameObject.SetActive(!is2D);
        cam2D.gameObject.SetActive(is2D);
    }

    public bool GetIs2D()
    {
        return is2D;
    }
}
