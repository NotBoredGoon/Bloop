using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameData;
using static UserSettings;
using UnityEngine.UI;           //required to could get a ref to a slider component
using BayatGames.SaveGameFree;
using TMPro;

//use "GameObject.Find("Game").GetComponent<GameUtil>().[method]();" to call methods from GameUtil 

public class GameUtil : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject easterEgg;
    [SerializeField] private AudioClip eggCollectedSound;
    [SerializeField] private GameObject pauseMenuLevelText;

    private float fastestCompletionTime;
    private bool easterEggExists;
    private bool easterEggCollected;
    private string fastestCompletionTimeIdentifier;
    private string easterEggExistsIdentifier;
    private string easterEggCollectedIdentifier;
    private string levelOpenIdentifier;
    private string easterEggsRequiredIdentifier;
    private string allEasterEggsCollectedIdentifier;
    private AudioSource audioSource;

    void Start()
    {
        fastestCompletionTimeIdentifier = SceneManager.GetActiveScene().name + "FastestCompletionTime";
        easterEggExistsIdentifier = SceneManager.GetActiveScene().name + "EasterEggExists";
        easterEggCollectedIdentifier = SceneManager.GetActiveScene().name + "EasterEggCollected";
        levelOpenIdentifier = SceneManager.GetActiveScene().name + "LevelOpen";
        easterEggsRequiredIdentifier = SceneManager.GetActiveScene().name + "EasterEggsRequired";
        allEasterEggsCollectedIdentifier = "AllEasterEggsCollected";
        SaveGame.Save<bool>(levelOpenIdentifier, true);                             //comment-out when reseting level data
        LoadLevelData();                                                            //comment-out when reseting level data
        UserSettings.Load();
        // EasterEgg.onPlayerCollision += SetEasterEggCollected;
        audioSource = GetComponent<AudioSource>();

        UnPauseGame();
        pauseMenuLevelText.GetComponent<TextMeshProUGUI>().text = SceneManager.GetActiveScene().name.Substring(0, 5) + " " + SceneManager.GetActiveScene().name.Substring(5);

        PrintLevelData();

        // SetEasterEggCollected(false);                                                //Uncomment when reseting level data
        // SetFastestCompletionTime(-1f);                                               //Uncomment when reseting level data
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        //Uncomment when reseting level data
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!GameData.GetGamePaused())
            {
                PauseGame();
            }
            else if (GameData.GetGamePaused())
            {
                if (optionsMenu.activeSelf)
                {
                    OptionsMenuSetVisibility(false);
                    UserSettings.Save();
                }
                UnPauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        GameData.SetGamePaused(true);
        Debug.Log("GamePaused");

        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        GameData.SetGamePaused(false);
        Debug.Log("GameUnPaused");
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void OptionsMenuSetVisibility(bool openOptions)
    {
        if (openOptions)
        {
            optionsMenu.SetActive(true);
            pauseMenu.SetActive(false);
            Debug.Log("OptionsMenuOpen");
        }
        else
        {
            optionsMenu.SetActive(false);
            pauseMenu.SetActive(true);
            Debug.Log("OptionsMenuClosed");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UnPauseGame();
    }

    public void NextLevel()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (SceneManager.GetActiveScene().name == "Level" + i)
            {
                int currentLevel = i;
                int nextLevel = currentLevel + 1;
                string nextLevelEasterEggsRequiredIdentifier = "Level" + nextLevel + "EasterEggsRequired";
                print("SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings: " + (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings));
                print("SaveGame.Exists(nextLevelEasterEggsRequiredIdentifier): " + SaveGame.Exists(nextLevelEasterEggsRequiredIdentifier));
                print("SaveGame.Load<int>(nextLevelEasterEggsRequiredIdentifier): " + SaveGame.Load<int>(nextLevelEasterEggsRequiredIdentifier));
                print("SaveGame.Load<int>(allEasterEggsCollectedIdentifier): " + (SaveGame.Load<int>(allEasterEggsCollectedIdentifier)));
                if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings && SaveGame.Exists(nextLevelEasterEggsRequiredIdentifier) && SaveGame.Load<int>(nextLevelEasterEggsRequiredIdentifier) <=
                    SaveGame.Load<int>(allEasterEggsCollectedIdentifier))
                {
                    PlayerPrefs.SetInt("Level" + nextLevel.ToString(), 1);
                    SetFastestCompletionTime(Time.timeSinceLevelLoad);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    SetFastestCompletionTime(Time.timeSinceLevelLoad);
                    openLevelMenu();
                }
            }
        }
    }

    public void openLevelMenu()
    {
        SceneManager.LoadScene("Level_Menu");
    }

    public float GetJumpSoundVolume()
    {
        return UserSettings.GetJumpSoundVolume();
    }

    public void SetJumpSoundVolume(float volume)
    {
        UserSettings.SetJumpSoundVolume(volume);
    }

    public void LoadLevelData()
    {
        if (!SaveGame.Exists(fastestCompletionTimeIdentifier))
        {
            SetFastestCompletionTime(-1f);
        }
        else
        {
            SetFastestCompletionTime(SaveGame.Load<float>(fastestCompletionTimeIdentifier));
        }

        if (easterEgg != null)
        {
            SetEasterEggExists(true);
        }
        else
        {
            SetEasterEggExists(false);
        }

        if (easterEggExists)
        {
            if (!SaveGame.Exists(easterEggCollectedIdentifier))
            {
                SetEasterEggCollected(false);
            }
            else
            {
                SetEasterEggCollected(SaveGame.Load<bool>(easterEggCollectedIdentifier));
            }
        }
    }

    public float GetFastestCompletionTime()
    {
        return fastestCompletionTime;
    }

    private void SetFastestCompletionTime(float time)
    {
        if (time < fastestCompletionTime || fastestCompletionTime <= 0f)
        {
            fastestCompletionTime = time;
            SaveGame.Save<float>(fastestCompletionTimeIdentifier, fastestCompletionTime);
            Debug.Log("SetFastestCompletionTime: " + fastestCompletionTime);
        }
    }

    public bool GetEasterEggExists()
    {
        return easterEggExists;
    }

    public void SetEasterEggExists(bool exists)
    {
        easterEggExists = exists;
        SaveGame.Save<bool>(easterEggExistsIdentifier, easterEggExists);
        Debug.Log("SetEasterEggExists: " + easterEggExists);

    }

    public bool GetEasterEggCollected()
    {
        return easterEggCollected;
    }

    public void SetEasterEggCollected(bool isCollected)
    {
        if (!easterEggCollected && isCollected)
        {
            easterEggCollected = isCollected;
            SaveGame.Save<bool>(easterEggCollectedIdentifier, easterEggCollected);
            int allEasterEggsCollected = SaveGame.Load<int>(allEasterEggsCollectedIdentifier);
            SaveGame.Save<int>(allEasterEggsCollectedIdentifier, allEasterEggsCollected);

            Debug.Log("SetEasterEggCollected: " + easterEggCollected + " | AllEasterEggsCollected: " + allEasterEggsCollected);

        }
        else if (!isCollected)
        {
            easterEggCollected = isCollected;
            SaveGame.Save<bool>(easterEggCollectedIdentifier, easterEggCollected);
            int allEasterEggsCollected = SaveGame.Load<int>(allEasterEggsCollectedIdentifier);
            SaveGame.Save<int>(allEasterEggsCollectedIdentifier, allEasterEggsCollected);

            Debug.Log("SetEasterEggCollected: " + easterEggCollected + " | AllEasterEggsCollected: " + allEasterEggsCollected);
        }
    }

    public void SetEasterEggCollected()
    {
        if (audioSource == null) Debug.LogError("audioScource is null on " + gameObject.name);

        audioSource.PlayOneShot(eggCollectedSound, UserSettings.GetJumpSoundVolume());
        SetEasterEggCollected(true);
    }

    public void PrintLevelData()
    {
        Debug.Log("easterEggExists: " + easterEggExists + " | easterEggCollected: " + easterEggCollected + " | AllEasterEggsCollected: " + SaveGame.Load<int>(allEasterEggsCollectedIdentifier) + " | Fastest Completion Time: " + fastestCompletionTime /*+ " | Previous Completion Time: " + previousCompletionTime*/);
    }
}
