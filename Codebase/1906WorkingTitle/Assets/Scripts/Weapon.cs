using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    int damage;
    float attackSpeed;

    int Attack()
    {
        return damage;
    }

    float GetAttackSpeed()
    {
        return attackSpeed;
    }
}
