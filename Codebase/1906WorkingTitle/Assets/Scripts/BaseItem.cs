using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public enum Type { Weapon, Potion, EOF};
    [SerializeField] Type itemType;
    [SerializeField] int value;
    [SerializeField] string name = "";
    [SerializeField] Sprite sprite;


    public Type ItemType()
    {
        return itemType;
    }

    public int GetValue()
    {
        return value;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    protected void SetValue(int _value)
    {
        value = _value;
    }

    protected void SetSprite(Sprite _sprite)
    {
        sprite = _sprite;
    }

    protected void SetName(string _name)
    {
        name = _name;
    }

    protected string GetName()
    {
        return name;
    }
}
