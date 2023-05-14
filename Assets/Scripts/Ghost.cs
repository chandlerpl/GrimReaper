using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Sprite following;
    public Sprite notFollowing; 
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start() {
        startPos = transform.position;
        gameObject.GetComponent<SpriteRenderer>().sprite = notFollowing;
    }

    public void ResetPosition() {
        transform.position = startPos;
    }
}
