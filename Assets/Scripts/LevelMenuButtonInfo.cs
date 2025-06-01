using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuButtonInfo : MonoBehaviour
{
    public Text LevelText;
    public int unlocked;
    public Image image;
    public Sprite levelLockedImage;
    public Sprite noEggImage;
    public Sprite eggCollectedImage;
    public Sprite eggUncollectedImage;
    public Sprite notEnoughEasterEggsCollectedImage;
    public Sprite enoughEasterEggsCollectedImage;
}
