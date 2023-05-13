using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour     // This controls all menus/UI
{
    [SerializeField] private GameObject _winMenu;

    private bool won = false;

    [SerializeField] private GameObject _pauseMenu;
    public GameObject _hudMenu;

    public bool IsPaused;
    //public bool InSettings;

    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        PauseMode(false);

    }

    void Update()
    {
        OnWin();
        CheckIfPaused();
        HudControl();
    }

    private void OnWin()
    {
        if (won == true)                // Retrieve win status from the goal script "Victory"
        {
            _hudMenu.SetActive(false);
            Cursor.visible = true;
            _winMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;

            
        }

        else
        {
            _hudMenu.SetActive(true);
            //Cursor.visible = false;
            _winMenu.SetActive(false);
            //Time.timeScale = 1;                           // Uncommenting the rest of these breaks the pause menu
            //Cursor.lockState = CursorLockMode.Locked;     // If it breaks again then dont bother with mouse, just use text
            // saying 'Enter to progress' and add the KeyDown input
        }

    }

    public void NextLevel()
    {
       // Debug.Log("Next Level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Next level is the current +1
    }

    private void CheckIfPaused()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   // Esc to pause
        {
            TogglePause();
            PauseMode(IsPaused);
        }
    }

    public void TogglePause()
    {
        if (IsPaused)
        {
            IsPaused = false;
        }
        else
        {
            IsPaused = true;
        }
    }

    private void ChangeCursorMode(bool unlocked)
    {
        if (unlocked)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void PauseMode(bool paused)
    {
        IsPaused = paused;
        OnPause(paused);
        ChangeCursorMode(paused);
        // Comment Following If There's No Settings Menu
        //if (!paused)
        //{
        //    SetSettingsMode(false);
        //}
    }

    private void OnPause(bool paused)       // Pausing activates pause menu and stops game time
    {
        if (paused)
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void HudControl()                   // If paused, no HUD
    {
        if (IsPaused)
        {
            _hudMenu.SetActive(false);
        }
        else
        {
            _hudMenu.SetActive(true);
        }
    }

    /*
    public void ToggleSettings()
    {
        if (InSettings)
        {
            _pauseMenu.SetActive(true);
            SetSettingsMode(false);
        }
        else
        {
            _pauseMenu.SetActive(false);
            SetSettingsMode(true);
        }
    }

    private void SetSettingsMode(bool status)
    {
        InSettings = status;
        _settingsMenu.SetActive(status);
    }
    */

    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1f;                // On resume time moves again and the other UI elements are set inactive
        _pauseMenu.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //Application.Quit();
        Debug.Log("Changing scene to main menu");
        SceneManager.LoadScene(0);        // On exit send them back to the main menu
#endif
    }
}