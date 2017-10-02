using System;

[Serializable]
public class ShopItem
{
    public string name { get; set; }
    public int price { get; set; }

    public bool isEquiped { get; set; }
    public bool isOwned { get; set; }
	public int itemID { get; set; }

    public ShopItem(string name, int price, bool isEquiped, bool isOwned, int itemID)
    {
        this.name = name;
        this.price = price;
        this.isEquiped = isEquiped;
        this.isOwned = isOwned;
		this.itemID = itemID;
    }

    public override string ToString()
    {
        return name;
    }
}
