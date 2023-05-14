using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public GameObject _reaper;
    public Sprite dead;
    public Sprite alive;

    private PiedReaper _piedReaper;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = dead;
        _piedReaper = _reaper.GetComponent<PiedReaper>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (_piedReaper.GreenGhostReturned && gameObject.name == "GreenBody")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = alive;
        }
        if (_piedReaper.RedGhostReturned && gameObject.name == "RedBody")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = alive;
        }
        if (_piedReaper.BlueGhostReturned && gameObject.name == "BlueBody")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = alive;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = dead;
        }
    }
}
