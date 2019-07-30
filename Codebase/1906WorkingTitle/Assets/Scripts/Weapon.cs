using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : BaseItem
{
    [SerializeField] int damage;
    [SerializeField] float attackSpeed;

    public Weapon(Weapon _weapon)
    {
        this.damage = _weapon.damage;
        this.attackSpeed = _weapon.attackSpeed;
        this.SetName(_weapon.GetName());
        SetSprite(_weapon.GetSprite());
        SetValue(_weapon.GetValue());
    }

    public int Attack()
    {
        return damage;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }
}
