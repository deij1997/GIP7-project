using System.Collections.Generic;
using Assets.Scripts.Base;
using UnityEngine;
using System;

public class ShopManager : MonoBehaviour
{
    private SaveSystem saveSystem;
    public GameStats gameStats { get { return GameStats.Instance; } }
    public ItemManager itemManager;

    void Awake()
    {
        saveSystem = new SaveSystem();
        itemManager = GameObject.FindObjectOfType<ItemManager>();

        if (itemManager== null)
        {
            itemManager = (Instantiate(Resources.Load("Items/ItemManager", typeof(GameObject))) as GameObject).GetComponent<ItemManager>();
        }

    }

    public void saveData()
    {
        gameStats.Save();
        itemManager.Save();
    }

    public void buyItem(string itemName)
    {
        ShopItem item = itemManager.getItem(itemName);

        if (item.price <= gameStats.coins && !item.isOwned)
        {
            gameStats.DecreaseCoins(item.price);
            itemManager.BuyItem(itemName);
            saveData();
        }
    }

    public void equipItem(string itemName)
    {
        ShopItem item = itemManager.getItem(itemName);

        if (item.isOwned)
        {
            itemManager.EquipItem(itemName, !item.isEquiped);
            saveData();
        }
    }
}
