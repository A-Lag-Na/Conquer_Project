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
        //weapon.SetShallow((Weapon)_weapon);
        weapon = (Weapon)_weapon;
    }

    public void RemoveWeapon()
    {
        weapon = null;
    }

    public void ChangePotion(BaseItem _potion)
    {
        //potion.SetShallow((Potion)_potion);
        potion = (Potion)_potion;
    }

    public void RemovePotion()
    {
        potion = null;
    }

    public float Heal()
    {
        if(potion!=null)
            return potion.Heal();
        else
            return 0.0f;
    }

    public int Attack()
    {
        if(weapon!=null)
            return weapon.Attack();
        else
            return 0;
    }

    public float GetAttackSpeed()
    {
        if(weapon!=null)
            return weapon.GetAttackSpeed();
        else
            return 0.0f;
    }

    public Sprite WeaponSprite()
    {
        if(weapon!=null)
            return weapon.GetSprite();
        else
            return Resources.Load<Sprite>("Sprites/background");
    }

    public Sprite PotionSprite()
    {
        if(potion!=null)
            return potion.GetSprite();
        else
            return Resources.Load<Sprite>("Sprites/background");
    }
}
