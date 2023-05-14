using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public Sprite dead;

    public Sprite alive;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = dead;
    }


    private void OnTriggerEnter(Collider other)
    {
        
        gameObject.GetComponent<SpriteRenderer>().sprite = alive;
    }
}
