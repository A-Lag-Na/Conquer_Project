using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int gold;
    LinkedList<Weapon> weaponList = new LinkedList<Weapon>();
    LinkedList<Potion> potionList = new LinkedList<Potion>();
    [SerializeField] LinkedListNode<Weapon> weaponNode;
    [SerializeField] Weapon weapon;
    [SerializeField] Potion potion;
    [SerializeField] LinkedListNode<Potion> potionNode;

    private void Start()
    {
        weaponNode = weaponList.First;
        potionNode = potionList.First;
    }

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


    //public void ChangeWeapon(BaseItem _weapon)
    //{
    //    weapon = (Weapon)_weapon;
    //}

    public void RemoveWeapon()
    {
        weapon = null;
    }

    //public void ChangePotion(BaseItem _potion)
    //{
    //    potion = (Potion)_potion;
    //}

    public void RemovePotion()
    {
        potion = null;
    }

    #region Add Weapons and Potions
    public void AddWeapon(BaseItem _weapon)
    {
        weaponList.AddLast((Weapon)_weapon);
        if (weaponNode == null)
        {
            weaponNode = weaponList.First;
        }
    }
    public void AddPotion(BaseItem _potion)
    {
        potionList.AddLast((Potion)_potion);
        if (potionNode == null)
        {
            potionNode = potionList.First;
        }
    }
    #endregion

    #region Cycle Weapon

    public void CycleWeaponForward()
    {
        if (weaponNode.Next != null)
        {
            weaponNode = weaponNode.Next;
        }
    }

    public void CycleWeaponBackward()
    {
        if (weaponNode.Previous != null)
        {
            weaponNode = weaponNode.Previous;
        }
    }

    #endregion

    #region Cycle Potion

    public void CyclePotionForward()
    {
        if (potionNode.Next != null)
        {
            potionNode = potionNode.Next;
        }
    }

    public void CyclePotionBackward()
    {
        if (potionNode.Previous != null)
        {
            potionNode = potionNode.Previous;
        }
    }

    #endregion

    #region Potion stat Grab
    public float Heal()
    {
        if (potion != null)
        {
            float heal = potion.Heal();
            potion = null;
            return heal;
        }
        else
            return 0.0f;
    }
    #endregion

    #region Weapon Stat Grabs
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
    #endregion

    #region Sprite Grabs
    public Sprite WeaponSprite()
    {
        if(weapon!=null)
            return weaponNode.Value.GetSprite();
        else
            return Resources.Load<Sprite>("Sprites/background");
    }

    public Sprite PotionSprite()
    {
        if(potion!=null)
            return potionNode.Value.GetSprite();
        else
            return Resources.Load<Sprite>("Sprites/background");
    }
    #endregion
}
