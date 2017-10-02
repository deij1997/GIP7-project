using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonItem : MonoBehaviour {

    private static ShopScreenManager man;
    public int index;

    void Start()
    {
        if (man == null)
        {
            man = GameObject.Find("shopScreenManager").GetComponent<ShopScreenManager>();
        }
    }

    public void Trigger()
    {
        man.selectItem(index);
    }
}
