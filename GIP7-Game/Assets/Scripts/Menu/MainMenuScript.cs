using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.Base;
using Random = UnityEngine.Random;

public class MainMenuScript : MonoBehaviour
{
    private static SaveSystem saveSystem = new SaveSystem();
    public GameStats gameStats { get { return GameStats.Instance; } }

    public void StartNewPlaythrough()
    {
        //Forces a new playthrough
        LevelLoader.NewPlayThrough(true);
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayButtonClickSound();
        }
    }

    public void ContinueOldPlaythrough()
    {
        //Loads the old, if any, else, loads a new one
        LevelLoader.LoadPlayThrough(true);
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayButtonClickSound();
        }
    }

    public void ViewStats()
    {
        //Loads the statistics scene
        LevelLoader.LoadStatistics();
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayButtonClickSound();
        }
    }

    public void ShowShop()
    {
        //Loads the statistics scene
        LevelLoader.LoadShop();
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayButtonClickSound();
        }
    }

    public void StartTutorial()
    {
        //Loads the tutorial scene
        LevelLoader.LoadTutorial();
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayButtonClickSound();
        }
    }
}
