using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : BaseItem
{
    public enum PotionType { Consumable, Thrown, EOF};
    PotionType potionType = PotionType.EOF;
    [SerializeField] float floatModifier = 5;
    [SerializeField] float intModifier = 5;
    [SerializeField] GameObject potionEffect = null;
    
    public PotionType GetPotionType()
    {
        return potionType;
    }

    public float GetFloatModifier()
    {
        return floatModifier;
    }
    public float GetIntModifier()
    {
        return intModifier;
    }

    public GameObject GetPotionEffect()
    {
        return potionEffect;
    }
}
