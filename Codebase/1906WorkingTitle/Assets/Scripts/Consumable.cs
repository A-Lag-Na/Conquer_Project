using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : BaseItem
{
    #region ConsumableStats
    public enum ConsumableType { Consumable, Thrown, EOF};
    [SerializeField] ConsumableType consumableType = ConsumableType.Consumable;
    [SerializeField] float floatModifier = 5;
    [SerializeField] int intModifier = 5;
    [SerializeField] GameObject consumableEffect = null;
    #endregion

    #region ConsumableFunctions
    public ConsumableType GetConsumableType()
    {
        return consumableType;
    }

    public float GetFloatModifier()
    {
        return floatModifier;
    }

    public int GetIntModifier()
    {
        return intModifier;
    }

    public GameObject GetPotionEffect()
    {
        return consumableEffect;
    }
    #endregion
}
