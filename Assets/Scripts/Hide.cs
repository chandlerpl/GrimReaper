using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    bool is2D = false;
    public GameObject cam3D, cam2D;

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
            is2D = !is2D; // flip it
            
            // switch camera
            SwitchCamera();
        }

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
}
