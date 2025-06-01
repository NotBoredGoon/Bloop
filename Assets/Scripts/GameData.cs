using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class GameData
{

    private static bool gamePaused = false;

    public static bool GetGamePaused()
    {
        return gamePaused;
    }
    public static void SetGamePaused(bool paused)
    {
        gamePaused = paused;
    }
}
