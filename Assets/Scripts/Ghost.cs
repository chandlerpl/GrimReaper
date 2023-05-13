using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start() {
        startPos = transform.position;
    }

    public void ResetPosition() {
        transform.position = startPos;
    }
}
