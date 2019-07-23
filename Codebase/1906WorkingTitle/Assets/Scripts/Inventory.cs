using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int gold;
    List<Item> itemList = new List<Item>();
    

    #region gold
    public void AddCoins(int amountOfCoins)
    {
        gold += amountOfCoins;
    }

    public int GetCoins()
    {
        return gold;
    }
    #endregion

    public void AddItem(Item _item)
    {
        itemList.Add(_item);
    }
}
