using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    bool is2D = false;
    bool bCanHide = false;
    public GameObject cam3D, cam2D; // cameras
    public GameObject canHideVolume; // can hide volume

    // Start is called before the first frame update
    void Start()
    {
        // Disable one camera
        cam2D.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            // if can hide is true
            if(canHideVolume.gameObject.GetComponent<HideVolume>().GetCanHide())
            {
                is2D = !is2D; // flip it
                
                // switch camera
                SwitchCamera();
            }
            else
            {
                Debug.Log("No Hiding Space!");
            }
        }

        // check for collisions
        // if (canHideVolume.GetComponent<BoxCollider>().OnCollisionEnter())
        // {}
        // Debug.Log("CanHideVolume : Collision");

        if(is2D) { Debug.Log("2D View Active"); }
        else     { Debug.Log("3D View Active"); }
    }

    void SwitchCamera()
    {
        // Flip each camera's active state
        cam3D.gameObject.SetActive(!cam3D.gameObject.activeSelf);
        cam2D.gameObject.SetActive(!cam2D.gameObject.activeSelf);
    }

    public bool GetIs2D()
    {
        return is2D;
    }

    bool CheckCanHide()
    {
        // if blocker close behind
        return false;
    }
}
