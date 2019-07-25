using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : BaseItem
{
    [SerializeField] int damage;
    [SerializeField] float attackSpeed;

    public int Attack()
    {
        return damage;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }
}
