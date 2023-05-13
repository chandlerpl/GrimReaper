using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideVolume : MonoBehaviour
{
    bool bCanHide = false;

    void OnTriggerEnter(Collider other)
    {
        // flip boolean - player can hide
        bCanHide = true;
        Debug.Log("Player can hide");
    }
    void OnTriggerExit(Collider other)
    {
        bCanHide = false;
        Debug.Log("Player CAN'T hide");
    }
}
