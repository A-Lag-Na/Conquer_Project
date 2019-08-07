using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    #region BaseProperties
    public enum Type { Weapon, Potion, EOF};
    [SerializeField] private Type itemType = (Type)1;
    [SerializeField] private int value = 0;
    [SerializeField] private new string name = "";
    [SerializeField] private Sprite sprite;
    #endregion

    #region BaseFunctions
    public Type GetItemType()
    {
        return itemType;
    }

    public int GetValue()
    {
        return value;
    }

    public Sprite GetSprite()
    {
        if (sprite != null)
            return sprite;
        else
            return Resources.Load<Sprite>("Sprites/background");
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
    #endregion
}
