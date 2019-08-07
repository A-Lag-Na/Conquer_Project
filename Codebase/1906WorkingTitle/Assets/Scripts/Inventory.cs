using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int gold;
    LinkedList<Weapon> weaponList = new LinkedList<Weapon>();
    LinkedList<Potion> potionList = new LinkedList<Potion>();
    [SerializeField] LinkedListNode<Weapon> weaponNode;
    [SerializeField] LinkedListNode<Potion> potionNode;
    [SerializeField] int amountOfPotions;

    private void Start()
    {
        weaponNode = weaponList.First;
        potionNode = potionList.First;
    }

    private void Update()
    {
        if (weaponNode != null)
        {
            if (Input.GetKeyDown(KeyCode.Keypad7))
                CycleWeaponForward();
            if (Input.GetKeyDown(KeyCode.Keypad1))
                CycleWeaponBackward();
        }

        if (potionNode != null)
        {
            if (Input.GetKeyDown(KeyCode.Keypad9))
                CyclePotionForward();
            if (Input.GetKeyDown(KeyCode.Keypad3))
                CyclePotionBackward();
        }
        amountOfPotions = potionList.Count;
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
        if (potionNode != null)
        {
            float heal = potionNode.Value.Heal();
            if (potionNode.Next != null)
            {
                potionNode = potionNode.Next;
                potionList.Remove(potionNode.Previous);
            }
            else if (potionNode.Previous != null)
            {
                potionNode = potionNode.Previous;
                potionList.Remove(potionNode.Next);
            }
            else
            {
                potionList.Remove(potionNode);
                potionNode = null;
            }
            return heal;
        }
        else
            return 0.0f;
    }
    #endregion

    #region Weapon Stat Grabs
    public int Attack()
    {
        if(weaponNode!=null)
            return weaponNode.Value.Attack();
        else
            return 0;
    }

    public float GetAttackSpeed()
    {
        if(weaponNode!=null)
            return weaponNode.Value.GetAttackSpeed();
        else
            return 0.0f;
    }
    #endregion

    #region Sprite Grabs
    public Sprite WeaponSprite()
    {
        if(weaponNode!=null)
            return weaponNode.Value.GetSprite();
        else
            return Resources.Load<Sprite>("Sprites/background");
    }

    public Sprite PotionSprite()
    {
        if(potionNode!=null)
            return potionNode.Value.GetSprite();
        else
            return Resources.Load<Sprite>("Sprites/background");
    }
    #endregion
}
