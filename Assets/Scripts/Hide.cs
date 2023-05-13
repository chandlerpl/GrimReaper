using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    bool Is2D = false;
    public GameObject Cam3D,Cam2D;

    // Start is called before the first frame update
    void Start()
    {
        // Disable one camera
        Cam2D.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Is2D = !Is2D; // flip it
            
            // switch camera
            SwitchCamera();
        }

        if(Is2D) { Debug.Log("2D View Active"); }
        else     { Debug.Log("3D View Active"); }
    }

    bool GetIs2D()
    {
        return Is2D;
    }

    void SwitchCamera()
    {
        // Flip each camera's active state
        Cam3D.gameObject.SetActive(!Cam3D.gameObject.activeSelf);
        Cam2D.gameObject.SetActive(!Cam2D.gameObject.activeSelf);
    }
}
