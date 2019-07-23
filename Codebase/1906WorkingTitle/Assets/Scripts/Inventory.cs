using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int gold;
    List<BaseItem> itemList = new List<BaseItem>();
    

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

    public void AddItem(BaseItem _item)
    {
        itemList.Add(_item);
    }
}
