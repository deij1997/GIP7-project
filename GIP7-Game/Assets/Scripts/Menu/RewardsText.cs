using Assets.Scripts.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsText : MonoBehaviour {

    public Text text;

    void Awake()
    {
        if(text == null)
        {
            text = this.gameObject.GetComponent<Text>();
        }
    }
	
	public void SetText()
    {
        if (LevelLoader.CurrentPlayThrough.PlaythroughCoins == 1)
        {
            text.text = "Je hebt " + LevelLoader.CurrentPlayThrough.PlaythroughCoins + " coin ontvangen";
        }
        else
        {
            text.text = "Je hebt " + LevelLoader.CurrentPlayThrough.PlaythroughCoins + " coins ontvangen";
        }
    }
}
