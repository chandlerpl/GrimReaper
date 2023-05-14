using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeFill : MonoBehaviour
{
    public GameObject _reaper;
    public GameObject [] counter;
    private PiedReaper _piedReaper;
    public Sprite fill;
    
    // Start is called before the first frame update
    void Start()
    {
        _piedReaper = _reaper.GetComponent<PiedReaper>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_piedReaper.BlueGhostReturned)
        {
            counter[0].GetComponent<Image>().sprite = fill;
        }
        if (_piedReaper.RedGhostReturned)
        {
            counter[1].GetComponent<Image>().sprite = fill;
        }
        if (_piedReaper.GreenGhostReturned)
        {
            counter[2].GetComponent<Image>().sprite = fill;
        }
        
    }
}
