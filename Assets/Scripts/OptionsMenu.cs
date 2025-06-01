using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject jumpSoundSlider;
    [SerializeField] private GameObject menuSoundSlider;
    [SerializeField] private GameObject menuButtonsVolume;

    void Start()
    {
        UserSettings.Load();
        menuButtonsVolume.SetActive(false);
        Debug.Log("UserSettings.GetJumpSoundVolume: " + UserSettings.GetJumpSoundVolume());
        jumpSoundSlider.GetComponent<Slider>().value = UserSettings.GetJumpSoundVolume();
        // menuSoundSlider.GetComponent<Slider>().value = UserSettings.GetMenuButtonSoundVolume();
        // Debug.Log("UserSettings.GetMenuButtonSoundVolume(): " + UserSettings.GetMenuButtonSoundVolume());
    }
    
    public void SetJumpSoundVolume()
    {
        UserSettings.SetJumpSoundVolume(jumpSoundSlider.GetComponent<Slider>().value);
        Debug.Log("UserSettings.GetJumpSoundVolume: " + UserSettings.GetJumpSoundVolume());
    }

    // public void SetMenuSoundVolume()
    // {
    //     UserSettings.SetMenuButtonSoundVolume(menuSoundSlider.GetComponent<Slider>().value);
    //     Debug.Log("UserSettings.GetMenuButtonSoundVolume: " + UserSettings.GetMenuButtonSoundVolume());
    //     SaveChanges();

    // }

    public void SaveChanges()
    {
        UserSettings.Save();
    }

}
