using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region InventoryStats
    [SerializeField] private int gold = 0, numBoxPieces = 0;
    [SerializeField] private LinkedListNode<Weapon> weaponNode = null;
    [SerializeField] private LinkedListNode<Consumable> consumableNode = null;
    [SerializeField] private int amountOfPotions = 0;

    private LinkedList<Weapon> weaponList = new LinkedList<Weapon>();
    private LinkedList<Consumable> consumableList = new LinkedList<Consumable>();
    private Player player = null;
    private ConditionManager con = null;
    private int bulletCount;
    #endregion

    private void Start()
    {
        player = GetComponentInParent<Player>();
        con = player.GetComponent<ConditionManager>();
        weaponNode = weaponList.First;
        consumableNode = consumableList.First;
    }

    private void Update()
    {
        if (weaponNode != null)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                CycleWeaponForward();
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                CycleWeaponBackward();
        }

        if (consumableNode != null)
        {
            if (Input.GetButtonDown("Potion Scroll Up"))
                CycleConsumableForward();
            if (Input.GetButtonUp("Potion Scroll Down"))
                CycleConsumableBackward();
        }

        amountOfPotions = consumableList.Count;
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

    public void SetCoins(int _coins)
    {
        gold = _coins;
    }
    #endregion

    #region Amount of Potions
    public int GetNumPotions()
    {
        return amountOfPotions;
    }
    #endregion

    #region Add Weapons and Consumables
    public void AddWeapon(BaseItem _weapon)
    {
        //weaponList.AddLast(WeaponDeepCopy((Weapon)_weapon));
        weaponList.AddLast(Weapon.Instantiate<Weapon>((Weapon)_weapon));
        if (weaponNode == null)
        {
            weaponNode = weaponList.First;
            player.ModifyDamage(weaponNode.Value.GetAttackDamage());
            player.ModifyAttackSpeed(weaponNode.Value.GetAttackSpeed());
        }
    }

    public void AddConsumable(BaseItem _consumable)
    {
        //consumableList.AddLast(ConsumableDeepCopy((Consumable)_consumable));
        consumableList.AddLast(Consumable.Instantiate<Consumable>((Consumable)_consumable));
        if (consumableNode == null)
        {
            consumableNode = consumableList.First;
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

    #region Cycle Consumable

    public void CycleConsumableForward()
    {
        if (consumableNode.Next != null)
            consumableNode = consumableNode.Next;
    }

    public void CycleConsumableBackward()
    {
        if (consumableNode.Previous != null)
            consumableNode = consumableNode.Previous;
    }

    #endregion

    #region Use Consumable

    public IEnumerator ConsumableTimer()
    {
        //check if there is a potion
        if (consumableNode != null)
        {
            //if consumable type
            if (consumableNode.Value.GetConsumableType() == Consumable.ConsumableType.Consumable)
            {
                switch (consumableNode.Value.GetName())
                {
                    case "Health Potion":
                        if (player.GetHealth() < player.GetMaxHealth())
                        {
                            player.RestoreHealth(consumableNode.Value.GetFloatModifier());
                            RemoveConsumable();
                        }
                        break;

                    case "Defense Potion":
                        player.ModifyDefense(consumableNode.Value.GetIntModifier());
                        int intModValue = consumableNode.Value.GetIntModifier();
                        RemoveConsumable();
                        yield return new WaitForSeconds(3f);
                        player.ModifyDefense(-1 * intModValue);
                        break;

                    case "Damage Buff Potion":
                        player.ModifyDamage(consumableNode.Value.GetIntModifier());
                        intModValue = consumableNode.Value.GetIntModifier();
                        RemoveConsumable();
                        yield return new WaitForSeconds(5f);
                        player.ModifyDamage(-1 * intModValue);
                        break;

                    case "Speed Potion":
                        player.ModifySpeed(consumableNode.Value.GetFloatModifier());
                        float floatModValue = consumableNode.Value.GetFloatModifier();
                        RemoveConsumable();
                        yield return new WaitForSeconds(6f);
                        player.ModifySpeed(consumableNode.Value.GetFloatModifier() * -1);
                        break;
                    case "Death Aura":
                        player.EnableDeathAura();
                        RemoveConsumable();
                        break;
                    case "Cone of Cold":
                        player.EnableConeofCold();
                        RemoveConsumable();
                        break;
                    default:
                        Debug.Log("Consumable without matching name");
                        break;
                }
            }
            //if thrown type
            else
            {
                player.ThrowConsumable(consumableNode.Value.GetConsumableEffect());
                RemoveConsumable();
            }

        }
    }

    #endregion

    #region Sprite Grabs
    public Sprite WeaponSprite()
    {
        if (weaponNode != null)
            return weaponNode.Value.GetSprite();
        else
            return Resources.Load<Sprite>("Sprites/background");
    }

    public Sprite ConsumableSprite()
    {
        if (consumableNode != null)
            return consumableNode.Value.GetSprite();
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

    public string ConsumableName()
    {
        if (consumableNode != null)
            return consumableNode.Value.GetName();
        else
            return "";
    }
    #endregion

    #region Remove Consumable
    private void RemoveConsumable()
    {
        //clear potion after use
        if (consumableNode.Next != null)
        {
            consumableNode = consumableNode.Next;
            consumableList.Remove(consumableNode.Previous);
        }
        else if (consumableNode.Previous != null)
        {
            consumableNode = consumableNode.Previous;
            consumableList.Remove(consumableNode.Next);
        }
        else
        {
            consumableList.Remove(consumableNode);
            consumableNode = null;
        }
    }
    #endregion

    #region Box Pieces
    public void AddBoxPiece()
    {
        numBoxPieces++;
    }

    public int GetBoxPieces()
    {
        return numBoxPieces;
    }

    public void SetBoxPieces(int _pieces)
    {
        numBoxPieces = _pieces;
    }
    #endregion

    #region Bullet Count
    public void AddBullet()
    {
        bulletCount++;
    }

    public int GetBulletCount()
    {
        return bulletCount;
    }

    public void SetBulletCount(int _bulletCount)
    {
        bulletCount = _bulletCount;
    }
    #endregion
}
