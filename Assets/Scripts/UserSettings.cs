using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;


public static class UserSettings
{
    private static float jumpSoundVolume;
    private static string jumpSoundVolumeIdentifier = "jumpSoundVolumeIdentifier";
    private static float menuButtonSoundVolume;
    private static string menuButtonSoundVolumeIdentifier = "menuButtonSoundVolumeIdentifier";


    public static float GetJumpSoundVolume()
    {
        return jumpSoundVolume;
    }

    public static void SetJumpSoundVolume(float volume)
    {
        jumpSoundVolume = volume;
    }

    public static float GetMenuButtonSoundVolume()
    {
        return menuButtonSoundVolume;
    }

    public static void SetMenuButtonSoundVolume(float volume)
    {
        menuButtonSoundVolume = volume;
    }

    public static void Save()
    {
        SaveGame.Save<float>(jumpSoundVolumeIdentifier, jumpSoundVolume);
        SaveGame.Save<float>(menuButtonSoundVolumeIdentifier, menuButtonSoundVolume);

        Debug.Log("Saved: jumpSoundVolumeIdentifier, jumpSoundVolume: " + jumpSoundVolumeIdentifier + ", " + jumpSoundVolume);
    }

    public static void Load()
    {
        jumpSoundVolume = SaveGame.Load<float>(jumpSoundVolumeIdentifier);
        menuButtonSoundVolume = SaveGame.Load<float>(menuButtonSoundVolumeIdentifier);
        Debug.Log("Loaded: jumpSoundVolumeIdentifier, jumpSoundVolume: " + jumpSoundVolumeIdentifier + ", " + jumpSoundVolume);
    }

}
