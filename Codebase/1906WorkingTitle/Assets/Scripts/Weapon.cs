using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : BaseItem
{
    [SerializeField] int damage;
    [SerializeField] float attackSpeed;
    private Weapon shallow;
    

    private void Update()
    {
        if (shallow != null)
        {
            this.damage = shallow.damage;
            this.attackSpeed = shallow.attackSpeed;
            this.SetName(shallow.GetName());
            SetSprite(shallow.GetSprite());
            SetValue(shallow.GetValue());
            shallow = null;
        }
    }

    public int Attack()
    {
        return damage;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }
    public void SetShallow(Weapon _shallow)
    {
        shallow = _shallow;
    }
}
