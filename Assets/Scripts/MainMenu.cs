using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;

    void Start()
    {
        optionsMenu.SetActive(false);
        UserSettings.Load();
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            // if (optionsMenu.GetActive())
            // {
                UserSettings.Save();
                HideOptionsMenu();
            // }
            // else if(GameData.GetGamePaused())
            // {
            //     if(optionsMenu.activeSelf)
            //     {
            //         OptionsMenuSetVisibility(false);
            //         UserSettings.Save();
            //     }
            //     UnPauseGame();
            // }
        }
    }


    public void OpenLevelMenu()
    {
        SceneManager.LoadScene("Level_Menu");
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    public void HideOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
