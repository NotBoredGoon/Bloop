using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BayatGames.SaveGameFree;


public class LEVEL_M_TEST : MonoBehaviour {

    public GameObject LoadingScene;
    public GameObject notEnoughEggsMessage;
    private string allEasterEggsCollectedIdentifier = "AllEasterEggsCollected";
    private bool timerActive;
    private float timerTimeLeft;


    [System.Serializable]
    public class Level
    {
       
        public string LevelText;
        // public int Unlock;
        public bool isInteractible;
        public int easterEggsRequired; //How many Easter Eggs, if any, does the player need to have in order to play this level?

        public Button.ButtonClickedEvent OnClick;
    }
    public GameObject LEVELButton;
    public Transform Spacer;
    public List<Level> LevelList;

    private float previousLevelFastestCompletionTime;       // Use this for initialization

    void Start ()
    {
        DisplayNotEnoughEggsMessage(false);
        FillList();
	}

    void Update()
    {
        if(timerActive)
        {
            UpdateTimer();
        }
    }

	void FillList()
    {
        int count = 1;
        int allEasterEggsCollected = 0;

        foreach(var level in LevelList)
        {
            string easterEggExistsIdentifier = "Level" + count.ToString() + "EasterEggExists";
            string easterEggCollectedIdentifier = "Level" + count.ToString() + "EasterEggCollected";
            string fastestCompletionTimeIdentifier = "Level" + count.ToString() + "FastestCompletionTime";
            string easterEggsRequiredIdentifier = "Level" + count.ToString() + "EasterEggsRequired";

            GameObject newbutton = Instantiate(LEVELButton) as GameObject;
            LevelMenuButtonInfo button = newbutton.GetComponent<LevelMenuButtonInfo>();
            level.LevelText = count.ToString();

            button.LevelText.text =  level.LevelText;

            if(count == 1)
            {
                level.isInteractible = true;
                button.image.sprite = button.noEggImage;
            }
            else if (SaveGame.Exists(fastestCompletionTimeIdentifier))
            {
                if (previousLevelFastestCompletionTime > 0)
                {
                    if (SaveGame.Exists(easterEggExistsIdentifier) && SaveGame.Load<bool>(easterEggExistsIdentifier))
                    {
                        if (SaveGame.Load<bool>(easterEggCollectedIdentifier))
                        {
                            button.image.sprite = button.eggCollectedImage;
                            allEasterEggsCollected++;
                        }
                        else
                        {
                            button.image.sprite = button.eggUncollectedImage;
                        }
                    }
                    else
                    {
                        button.image.sprite = button.noEggImage;
                    }
                    level.isInteractible = true;
                }
                else
                {
                    button.image.sprite = button.levelLockedImage;
                }
            }
            else
            {
                button.image.sprite = button.levelLockedImage;

            }
            if (level.easterEggsRequired > 0)
            {
                if (level.easterEggsRequired > allEasterEggsCollected)
                {
                    button.image.sprite = button.notEnoughEasterEggsCollectedImage;
                }
                else
                {
                    button.image.sprite = button.enoughEasterEggsCollectedImage;
                }
                SaveGame.Save<int>(("Level" + count + "EasterEggsRequired"), level.easterEggsRequired);
            }

            button.GetComponent<Button>().interactable = level.isInteractible;
            if(allEasterEggsCollected >= level.easterEggsRequired)
            {
                button.GetComponent<Button>().onClick.AddListener(() => LoadLevel("Level" + button.LevelText.text));
            }
            else
            {
                button.GetComponent<Button>().onClick.AddListener(() => DisplayNotEnoughEggsMessage(true));
            }


            SaveGame.Save<int>(easterEggsRequiredIdentifier, level.easterEggsRequired);
            newbutton.transform.SetParent(Spacer);
            count++;
            if (SaveGame.Exists(fastestCompletionTimeIdentifier))
            {
                previousLevelFastestCompletionTime = SaveGame.Load<float>(fastestCompletionTimeIdentifier);
            }
            else
            {
                previousLevelFastestCompletionTime = -1f;
            }

            
        }
        SaveGame.Save<int>(allEasterEggsCollectedIdentifier, allEasterEggsCollected);
    }


    void LoadLevel(string value)
    {
        SceneManager.LoadScene(value);
    }

    public void DisplayNotEnoughEggsMessage(bool isActive)
    {
        if(isActive)
        {
            notEnoughEggsMessage.SetActive(true);
            StartTimer(3f);
        }
        else
        {
            notEnoughEggsMessage.SetActive(false);
        }
    }

    public void StartTimer(float time)
    {
        timerTimeLeft = time;
        timerActive = true;
    }

    public void UpdateTimer()
    {
        timerTimeLeft -= Time.deltaTime;
        if(timerTimeLeft <= 0f)
        {
            DisplayNotEnoughEggsMessage(false);
            timerActive = false;
        }
    }
}
