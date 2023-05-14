using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MainMenu
{
    public void ResumeGame()
    {
        gameObject.SetActive(false);
    }
}
