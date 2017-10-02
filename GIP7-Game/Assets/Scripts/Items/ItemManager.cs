using Assets.Scripts.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
	public List<ShopItem> items { get; set; }

	private static SaveSystem ss = new SaveSystem();

	private static ItemManager instance = null;
	public static ItemManager Instance { get { return instance; } }

    public GameStats gameStats { get { return GameStats.Instance; } }

    public bool destroy = false;

	private void Awake()
	{
		DontDestroyObject();
		Load();
	    gameStats.Load();
	}


	private void DontDestroyObject()
	{
		if (!destroy)
		{
			if (instance != null && instance != this)
			{
				Destroy(this.gameObject);
				return;
			}
			else
			{
				instance = this;
			}
			DontDestroyOnLoad(this.gameObject);
		}
	}

	public bool Load()
	{
		ss.Clear();
		bool itemsExist = ss.Load(Files.ITEMS_FNAME);
		ss.Clear();

		if (!itemsExist)
		{
			Debug.Log("Creating new Items file");

			items = new List<ShopItem>();

			items.Add(new ShopItem("Groen shirt", 2, false, false, 1));
			items.Add(new ShopItem("Groene broek", 3, false, false, 2));

			ss.Clear();
			ss.Add(items);
			ss.Save(Files.ITEMS_FNAME);
			ss.Clear();
		}

		ss.Clear();
		ss.Load(Files.ITEMS_FNAME);
		items = ss.GetObject<List<ShopItem>>();
		ss.Clear();




		return items == null;
	}

	public void Save()
	{
		ss.Clear();
		ss.Add(items);
		ss.Save(Files.ITEMS_FNAME);
		ss.Clear();
	}

	public ShopItem getItem(string name)
	{
		foreach (ShopItem item in items)
		{
			if (item.name == name)
			{
				return item;
			}
		}

		return null;
	}

	public void BuyItem(string name)
	{
		getItem(name).isOwned = true;
	}

	public void EquipItem(string name, bool isEquiped)
	{
		getItem(name).isEquiped = isEquiped;
	}
}
