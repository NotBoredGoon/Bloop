using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;

    void Start()
    {
        // UserSettings.Load();

    }

    public void PlayOpenSound()
    {
        if (audioSource == null) Debug.LogError("audioScource is null on " + gameObject.name);

        audioSource.PlayOneShot(openSound/*, UserSettings.GetMenuButtonSoundVolume()*/);

    }

    public void PlayCloseSound()
    {
        if (audioSource == null) Debug.LogError("audioScource is null on " + gameObject.name);
        audioSource.PlayOneShot(closeSound/*, UserSettings.GetMenuButtonSoundVolume() / 1.25f*/);
    }

}
