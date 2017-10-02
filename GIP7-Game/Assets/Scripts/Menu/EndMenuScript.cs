using Assets.Scripts.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenuScript : MonoBehaviour
{

    public void LoadMainMenu()
    {
        LevelLoader.LoadMainMenu();
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayButtonClickSound();
        }
    }

    public void LoadStatistics()
    {
        LevelLoader.LoadStatistics();
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayButtonClickSound();
        }
    }
}
