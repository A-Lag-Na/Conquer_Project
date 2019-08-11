using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : BaseItem
{
    [SerializeField] int damage = 0;
    [SerializeField] float attackSpeed = 0;

    public int GetAttackDamage()
    {
        return damage;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }
}
