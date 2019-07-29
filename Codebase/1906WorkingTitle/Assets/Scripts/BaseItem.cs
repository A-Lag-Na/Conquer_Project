using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public enum Type { Weapon, Potion, EOF};
    [SerializeField] Type itemType;
    [SerializeField] int value;
    [SerializeField] string name = "";


    public Type ItemType()
    {
        return itemType;
    }

    public int GetValue()
    {
        return value;
    }

}
