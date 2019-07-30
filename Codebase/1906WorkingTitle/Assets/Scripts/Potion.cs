using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : BaseItem
{
    [SerializeField] float healthReturn = 5;

    public Potion(Potion _potion)
    {
        this.SetName(_potion.GetName());
        this.healthReturn = _potion.healthReturn;
        SetSprite(_potion.GetSprite());
        SetValue(_potion.GetValue());
    }

    public float Heal()
    {
        return healthReturn;
    }
}
