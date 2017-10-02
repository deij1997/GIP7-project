using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {

	// Use this for initialization
	public GameObject player;
	private Player playerPlayer;
	public ItemManager itemManager;
	public SpriteSet[] spriteSets;

	void Start ()
	{
		playerPlayer = player.GetComponent<Player>();
		itemManager = GameObject.FindObjectOfType<ItemManager>();

		if (itemManager == null)
		{
			itemManager = (Instantiate(Resources.Load("Items/ItemManager", typeof(GameObject))) as GameObject).GetComponent<ItemManager>();
		}

		int finalNumber = 0;
		foreach(ShopItem item in itemManager.items)
		{
			if (item.isEquiped) finalNumber += item.itemID;
		}

		playerPlayer.stand = spriteSets[finalNumber].stand;
		playerPlayer.walk1 = spriteSets[finalNumber].walk1;
		playerPlayer.walk2 = spriteSets[finalNumber].walk2;
		playerPlayer.jump = spriteSets[finalNumber].jump;
	}
	

	[System.Serializable]
	public class SpriteSet
	{
		public Sprite stand;
		public Sprite walk1;
		public Sprite walk2;
		public Sprite jump;
	}
}
