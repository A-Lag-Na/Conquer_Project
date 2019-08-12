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
    Player player = null;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        weaponNode = weaponList.First;
        potionNode = potionList.First;
    }

    private void Update()
    {
        if (weaponNode != null)
        {
            if(Input.GetAxis("Mouse ScrollWheel") > 0f)
                CycleWeaponForward();
            if(Input.GetAxis("Mouse ScrollWheel") < 0f)
                CycleWeaponBackward();
        }

        if (potionNode != null)
        {
            if (Input.GetButtonDown("Weapon Scroll Up"))
                CyclePotionForward();
            if (Input.GetButtonUp("Weapon Scroll Down"))
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
            player.ModifyDamage(weaponNode.Value.GetAttackDamage());
            player.ModifyAttackSpeed(weaponNode.Value.GetAttackSpeed());
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
            player.ModifyDamage(-1 * weaponNode.Value.GetAttackDamage());
            player.ModifyAttackSpeed(-1 * weaponNode.Value.GetAttackSpeed());
            weaponNode = weaponNode.Next;
            player.ModifyDamage(weaponNode.Value.GetAttackDamage());
            player.ModifyAttackSpeed(weaponNode.Value.GetAttackSpeed());
        }
    }

    public void CycleWeaponBackward()
    {
        if (weaponNode.Previous != null)
        {
            player.ModifyDamage(-1 * weaponNode.Value.GetAttackDamage());
            player.ModifyAttackSpeed(-1 * weaponNode.Value.GetAttackSpeed());
            weaponNode = weaponNode.Previous;
            player.ModifyDamage(weaponNode.Value.GetAttackDamage());
            player.ModifyAttackSpeed(weaponNode.Value.GetAttackSpeed());
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

    #region Use Potion
    
    public IEnumerator PotionTimer()
    {
        //check if there is a potion
        if (potionNode != null)
        {
            //if consumable type
            if (potionNode.Value.GetPotionType() == Potion.PotionType.Consumable)
            {
                switch (potionNode.Value.name)
                {
                    case "Health Potion":
                        if (player.GetHealth() < player.GetMaxHealth())
                        {
                            player.RestoreHealth(potionNode.Value.GetFloatModifier());
                            RemovePotion();
                        }
                        break;

                    case "Defense Potion":
                        player.ModifyDefense(potionNode.Value.GetIntModifier());
                        int intModValue = potionNode.Value.GetIntModifier();
                        RemovePotion();
                        yield return new WaitForSeconds(3f);
                        player.ModifyDefense(-1 * intModValue);
                        break;

                    case "Damage Buff Potion":
                        player.ModifyDamage(potionNode.Value.GetIntModifier());
                        intModValue = potionNode.Value.GetIntModifier();
                        RemovePotion();
                        yield return new WaitForSeconds(5f);
                        player.ModifyDamage(-1 * intModValue);
                        break;

                    case "Movement Speed Potion":
                        player.ModifySpeed(potionNode.Value.GetFloatModifier());
                        float floatModValue = potionNode.Value.GetFloatModifier();
                        RemovePotion();
                        yield return new WaitForSeconds(6f);
                        player.ModifySpeed(-1 * floatModValue);
                        break;

                    default:
                        break;
                }
            }
            //if thrown type
            else
            {
                player.ThrowPotion(potionNode.Value.GetPotionEffect());
                RemovePotion();
            }

        }
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

    #region Name Grabs
    public string WeaponName()
    {
        if (weaponNode != null)
            return weaponNode.Value.GetName();
        else
            return "";
    }

    public string PotionName()
    {
        if (potionNode != null)
            return potionNode.Value.GetName();
        else
            return "";
    }
    #endregion

    #region Remove Potion
    private void RemovePotion()
    {
        //clear potion after use
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
    }
    #endregion

}
