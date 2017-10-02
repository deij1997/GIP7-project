using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Base;
using UnityEngine;
using UnityEngine.UI;

public class StatsSceneManager : MenusController
{
    public GameObject categoryText;
    public GameObject content;
    public Text levelDifficultyText;
    public Text coins;
    public Text amountOfPlaythroughsText;
    public Text lastFinishedPlaythroughText;
    public int spaceBetweenRows = 190;
    public int textBoxWidth = 1660;
    public int textBoxLength = 158;

    private static SaveSystem saveSystem = new SaveSystem();
    private List<Stats> categories;

    // Used for initialization. Calls method to load all stats from the stats file.
    void Start()
    {
        LoadStats();
    }

    // Loads starts from the stats file and puts them into the scene.
    private void LoadStats()
    {
        saveSystem.Clear();
        saveSystem.Load(Files.STATS_FNAME);
        categories = saveSystem.GetObject<List<Stats>>();
        saveSystem.Clear();

        spaceBetweenRows = 190;
        textBoxWidth = 1660;
        textBoxLength = 158;

        int counter = 0;

        foreach (Stats statsLine in categories)
        {
            if (statsLine.categoryName != null)
            {
                if (statsLine.Total > 0)
                {
                    categoryText = Instantiate(Resources.Load("CategoryText")) as GameObject;
                    categoryText.name = statsLine.categoryName;
                    categoryText.GetComponent<Text>().text = "Moeilijkheidsgraad " + statsLine.categoryName + " (" +
                                                             statsLine.Total + " situaties totaal):\nAanpakken: " +
                                                             statsLine.handle
                                                             + " - Knokken: " + statsLine.fight + " - Wegkruipen: " +
                                                             statsLine.hide;
                    categoryText.transform.SetParent(content.transform);
                    categoryText.transform.localPosition = new Vector3(5, -100, 0);
                    categoryText.transform.localPosition += new Vector3(0, -spaceBetweenRows * counter, 0);
                    categoryText.GetComponent<RectTransform>().sizeDelta = new Vector2(textBoxWidth, textBoxLength);
                    counter++;
                }
            }
            else
            {
                levelDifficultyText.text = "Huidige moeilijkheidsgraad: " + statsLine.levelDifficulty;
                coins.text = "Aantal verzamelde muntjes: " + statsLine.coins;
                amountOfPlaythroughsText.text = "Aantal gespeelde playthroughs: " + statsLine.amountOfPlaythroughs;

                if (statsLine.lastFinishedPlaythrough.ToString("dd-MM-yyyy hh:mm").Equals("01-01-0001 12:00"))
                {
                    lastFinishedPlaythroughText.text = "Laatste playthrough voltooid: " + "-";
                }
                else
                {
                    lastFinishedPlaythroughText.text = "Laatste playthrough voltooid: " +
                                                       statsLine.lastFinishedPlaythrough.ToString("dd-MM-yyyy HH:mm");
                }
            }
        }
    }

    public void ReturnToMain()
    {
        LevelLoader.LoadMainMenu();
        SoundManager.Instance.PlayButtonClickSound();
    }
}
