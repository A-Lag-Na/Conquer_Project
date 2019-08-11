using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : BaseItem
{
    public enum PotionType { Consumable, Thrown, EOF};
    [SerializeField] PotionType potionType = PotionType.Consumable;
    [SerializeField] float floatModifier = 5;
    [SerializeField] int intModifier = 5;
    [SerializeField] GameObject potionEffect = null;
    
    public PotionType GetPotionType()
    {
        return potionType;
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
        return potionEffect;
    }
}
