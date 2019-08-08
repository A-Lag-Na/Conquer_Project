using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : BaseItem
{
    public enum PotionType { Consumable, Thrown, EOF};
    PotionType potionType;
    [SerializeField] float healthReturn = 5;
    
    public PotionType GetPotionType()
    {
        return potionType;
    }

    public float Heal()
    {
        return healthReturn;
    }
}
