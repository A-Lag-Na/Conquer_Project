using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : BaseItem
{
    [SerializeField] float healthReturn = 5;

    public float Heal()
    {
        return healthReturn;
    }
}
