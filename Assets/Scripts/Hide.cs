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

        if(Is2D) { Debug.Log("2D"); }
        else     { Debug.Log("3D"); }
    }

    bool GetIs2D()
    {
        return Is2D;
    }

    void SwitchCamera()
    {
        if (!Is2D)
        {
            // 2D to 3D cam 
            Cam3D.GetComponent<Camera>().enabled = true; // Enable 3D
            Cam2D.GetComponent<Camera>().enabled = false; // Disable 2D
        }
        else
        {
            // 3D to 2D cam 
            Cam2D.GetComponent<Camera>().enabled = true; // Enable 3D
            Cam3D.GetComponent<Camera>().enabled = false; // Disable 2D
        }
    }
}
