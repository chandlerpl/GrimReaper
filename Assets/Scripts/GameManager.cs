using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PiedReaper reaper;
    public PlayerDetector playerDetector;
    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

 
}
