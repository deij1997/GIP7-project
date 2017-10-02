using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Base;

public class NPCController : MonoBehaviour
{
    private GameStats gameStats;
    private const int REWARDSIZE = 1;

    public Dialog[] dialogList;
    public Emotion currentEmotion;

    private CanvasScript canvasScript;
    private int currentDialogNumber = 0;
    private Option lastOption;

    /*
    private void Awake()
    {
        gameStats = GameObject.FindObjectOfType<GameStats>();

        if (gameStats == null)
        {
            gameStats = (Instantiate(Resources.Load("Stats/GameStats", typeof(GameObject))) as GameObject).GetComponent<GameStats>();
			gameStats.Save();
		}

    }*/

    public void SetCanvas(CanvasScript canvas)
    {
        canvasScript = canvas;
    }

    public void Talk()
    {
        canvasScript.SetEmotionImage(currentEmotion);
        if (currentDialogNumber == -1)
        {
            canvasScript.DisableAllButtons();
            canvasScript.SetDialogBox(lastOption.charaterResponse);
        }
        else
        {
            canvasScript.EnableAllButtons();
            Dialog currentDialog = dialogList[currentDialogNumber];
            canvasScript.SetDialogBox(currentDialog.initialDialog);

            for (int i = 0; i < 4; i++)
            {
                canvasScript.answerButtons[i].onClick.RemoveAllListeners();
                LoadDialog(i, currentDialog);
            }
        }
    }

    void OnMouseDown()
    {
        //If able to talk. Talk
        Talk();
    }

    private void LoadDialog(int i, Dialog currentDialog)
    {
        if (currentDialog.answers[i].answerType != SolutionTypes.Confrontation)
        {
            canvasScript.answerButtons[i].transform.GetChild(0).GetComponent<Text>().text = currentDialog.answers[i].buttonText;
            canvasScript.answerButtons[i].onClick.AddListener(delegate { RespondBad(currentDialog.answers[i]); });
        }
        else
        {
            canvasScript.answerButtons[i].transform.GetChild(0).GetComponent<Text>().text = currentDialog.answers[i].buttonText;
            canvasScript.answerButtons[i].onClick.AddListener(delegate { RespondGood(currentDialog.answers[i]); });
        }
    }

    public void RespondGood(Option selectedResponse)
    {
        currentDialogNumber++;

        if (currentDialogNumber >= dialogList.Length)
        {
            lastOption = selectedResponse;
            currentDialogNumber = -1;
			ShopManager shop = new ShopManager();

            LevelLoader.CurrentPlayThrough.IncreaseCoinsCurrentLevel(REWARDSIZE);
		}
		currentEmotion = selectedResponse.resultedEmotion;
        Talk();
    }

    public void RespondBad(Option selectedResponse)
    {
        lastOption = selectedResponse;
        currentDialogNumber = -1;
        currentEmotion = lastOption.resultedEmotion;
        Talk();

    }

    public SolutionTypes GetSolution()
    {
        if (lastOption == null)
        {
            return SolutionTypes.Ignore;
        }

        return lastOption.answerType;
    }

    [System.Serializable]
    public class Dialog
    {
        public string initialDialog;
        public Option[] answers;
    }

    [System.Serializable]
    public class Option
    {
        public string buttonText;
        public SolutionTypes answerType;
        public Emotion resultedEmotion;
        public string charaterResponse;
    }

    public enum SolutionTypes
    {
        Fight,
        Flight,
        Confrontation,
        Ignore
    }

    public enum Emotion
    {
        Happy,
        Sad,
        Scared,
        Angry
    }
}
