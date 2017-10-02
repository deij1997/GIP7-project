using Assets.Scripts.Base;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreenManager : MenusController
{
    public ShopManager shopManager;
    private GameStats gameStats
    {
        get { return GameStats.Instance; }
    }

    public Dropdown dropdown;

    public Text itemName;
    public Text itemPrice;
    public Toggle isOwned;
    public Toggle isEquiped;

    public Button Btn;
    public Button equipBtn;
    public Text BtnTxt;

    public Text coins;

    private ShopItem selectedItem = null;

    void Start()
    {
        selectItem(0);
    }

    void UpdateInfo()
    {
        coins.text = gameStats.coins.ToString();

        fillItemInfo();

        if (selectedItem != null)
        {
			if (!selectedItem.isOwned) BtnTxt.text = "Koop";
			else if (selectedItem.isEquiped) BtnTxt.text = "Deselecteer";
			else BtnTxt.text = "Selecteer";
        }
        else
        {
            BtnTxt.text = "";           
            Btn.enabled = false;
        }
    }

    public void fillItemInfo()
    {
        if (selectedItem != null)
        {
            itemName.text = selectedItem.name;
            itemPrice.text = selectedItem.price.ToString();

            isOwned.isOn = selectedItem.isOwned;
            isEquiped.isOn = selectedItem.isEquiped;
        }
        else
        {
            itemName.text = "";
            itemPrice.text = "";

            isOwned.isOn = false;
            isEquiped.isOn = false;
        }
    }

    public void selectItem(int index)
    {
        selectedItem = shopManager.itemManager.items[index];
        UpdateInfo();
    }

	public void BuyOrEquip()
	{
	    if (!selectedItem.isOwned)
	    {
	        buyItem();
        }
	    else
	    {
	        equipItem();
	    }
	}

	public void buyItem()
    {
        shopManager.buyItem(selectedItem.name);
        SoundManager.Instance.PlayButtonClickSound();
        UpdateInfo();
    }

    public void equipItem()
    {
        shopManager.equipItem(selectedItem.name);
        SoundManager.Instance.PlayButtonClickSound();
        UpdateInfo();
    }

    public void backToMenu()
    {
        shopManager.saveData();
        LevelLoader.LoadMainMenu();
        SoundManager.Instance.PlayButtonClickSound();
    }
}
