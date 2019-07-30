using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int gold;
    List<Weapon> weaponList = new List<Weapon>();
    List<Potion> potionList = new List<Potion>();
    [SerializeField] Weapon weapon;
    [SerializeField] Potion potion;

    
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


    public void ChangeWeapon(BaseItem _weapon)
    {
        weapon = new Weapon((Weapon)_weapon);
    }

    public void RemoveWeapon()
    {
        weapon = null;
    }

    public void ChangePotion(BaseItem _potion)
    {
        potion = new Potion((Potion)_potion);
    }

    public void RemovePotion()
    {
        potion = null;
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

    public Sprite WeaponSprite()
    {
        return weapon.GetSprite();
    }

    public Sprite PotionSprite()
    {
        return potion.GetSprite();
    }
}
