using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int gold;
    List<Weapon> weaponList = new List<Weapon>();
    List<Potion> potionList = new List<Potion>();
    Weapon weapon;
    Potion potion;

    
    #region gold
    public void AddCoins(int amountOfCoins)
    {
        gold += amountOfCoins;
    }

    public int GetCoins()
    {
        return gold;
    }
    #endregion

    //public void AddWeapon(Weapon _weapon)
    //{
    //    weaponList.Add(_weapon);
    //}
    //public void AddPotion(Potion _potion)
    //{
    //    potionList.Add(_potion);
    //}


    public void ChangeWeapon(Weapon _weapon)
    {
        weapon = _weapon;
    }

    public void ChangePotion(Potion _potion)
    {
        potion = _potion;
    }

    public float Heal()
    {
        return potion.Heal();
    }

    public int Attack()
    {
        return weapon.Attack();
    }

    public float GetAttackSpeed()
    {
        return weapon.GetAttackSpeed();
    }
}
