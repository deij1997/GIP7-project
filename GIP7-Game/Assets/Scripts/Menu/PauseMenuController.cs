using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.Base;

public class PauseMenuController : MenusController
{
    public Button pausebutton;
    public Button resumebutton;
    public Button mainmenubutton;
    public GameObject pausepanel;

    protected override bool ShouldGoBack
    {
        get
        {
            return false;
        }
    }

    public void PauseGame()
    {
        pausepanel.SetActive(true);
        pausebutton.gameObject.SetActive(false);

        Time.timeScale = 0;
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayButtonClickSound();
        }
    }

    public void ResumeGame()
    {
        pausepanel.SetActive(false);
        pausebutton.gameObject.SetActive(true);

        Time.timeScale = 1;
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayButtonClickSound();
        }
    }

    public void ReturnToMain()
    {
        // ToDo: save all character stuff that hasnt been saved
        Time.timeScale = 1;
        LevelLoader.LoadMainMenu();
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayButtonClickSound();
        }
    }
}
