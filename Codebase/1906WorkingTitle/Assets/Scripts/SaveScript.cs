using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    Player player = null;
    int saveSlot = 0;
    void Start()
    {
        player = GetComponent<Player>();
    }

    public void Save()
    {
        float playerMovementSpeed = player.GetMovementSpeed();
        float maxPlayerHealth = player.GetMaxHealth();
        if (player.GetCompanion() != null)
            if (player.GetCompanion().gameObject.name == "Health Regen Companion")
                maxPlayerHealth -= 10;
        float playerAttackSpeed = player.GetFireRate();
        float playerExperience = player.GetExperience();
        float nextLevelExperience = player.GetNextLevelExperience();
        int playerDefense = player.GetDefense();
        int visualAttackSpeed = player.GetAttackSpeed();
        int playerAttackDamage = player.GetDamage();
        int playerLevel = player.GetLevel();
        int playerSpendingPoints = player.GetSpendingPoints();
        int playerGold = player.gameObject.GetComponent<Inventory>().GetCoins();
        int playerBoxes = player.gameObject.GetComponent<Inventory>().GetBoxPieces();

        PlayerPrefs.SetFloat($"MoveSpeed{saveSlot}", playerMovementSpeed);
        PlayerPrefs.SetFloat($"MaxHealth{saveSlot}", maxPlayerHealth);
        PlayerPrefs.SetFloat($"AttackSpeed{saveSlot}", playerAttackSpeed);
        PlayerPrefs.SetFloat($"Exp{saveSlot}", playerExperience);
        PlayerPrefs.SetFloat($"NextLevelExp{saveSlot}", nextLevelExperience);
        PlayerPrefs.SetInt($"Defense{saveSlot}", playerDefense);
        PlayerPrefs.SetInt($"VisualAttackSpeed{saveSlot}", visualAttackSpeed);
        PlayerPrefs.SetInt($"Damage{saveSlot}", playerAttackDamage);
        PlayerPrefs.SetInt($"Level{saveSlot}", playerLevel);
        PlayerPrefs.SetInt($"SpendingPoints{saveSlot}", playerSpendingPoints);
        PlayerPrefs.SetInt($"Gold{saveSlot}", playerGold);
        PlayerPrefs.SetInt($"Boxes{saveSlot}", playerBoxes);

        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey($"MoveSpeed{saveSlot}"))
        {
            float playerMovementSpeed = PlayerPrefs.GetFloat($"MoveSpeed{saveSlot}");
            float maxPlayerHealth = PlayerPrefs.GetFloat($"MaxHealth{saveSlot}");
            float playerAttackSpeed = PlayerPrefs.GetFloat($"AttackSpeed{saveSlot}");
            float playerExperience = PlayerPrefs.GetFloat($"Exp{saveSlot}");
            float nextLevelExperience = PlayerPrefs.GetFloat($"NextLevelExp{saveSlot}");
            int playerDefense = PlayerPrefs.GetInt($"Defense{saveSlot}");
            int visualAttackSpeed = PlayerPrefs.GetInt($"VisualAttackSpeed{saveSlot}");
            int playerAttackDamage = PlayerPrefs.GetInt($"Damage{saveSlot}");
            int playerLevel = PlayerPrefs.GetInt($"Level{saveSlot}");
            int playerSpendingPoints = PlayerPrefs.GetInt($"SpendingPoints{saveSlot}");
            int playerGold = PlayerPrefs.GetInt($"Gold{saveSlot}");
            int playerBoxes = PlayerPrefs.GetInt($"Boxes{saveSlot}");

            GetComponent<CharacterController>().enabled = false;
            player.SetPosition(new Vector3(-1.4f, -9.9f, -55.6f));
            player.SetMovementSpeed(playerMovementSpeed);
            GetComponent<ConditionManager>().SetMaxSpeed(playerMovementSpeed);
            player.SetHealth(maxPlayerHealth);
            player.SetMaxHealth(maxPlayerHealth);
            player.SetFireRate(playerAttackSpeed);
            player.SetExperience(playerExperience);
            player.SetNextLevelExperience(nextLevelExperience);
            player.SetDefense(playerDefense);
            player.SetAttackSpeed(visualAttackSpeed);
            player.SetDamage(playerAttackDamage);
            player.SetLevel(playerLevel);
            player.SetSpendingPoints(playerSpendingPoints);
            player.gameObject.GetComponent<Inventory>().SetCoins(playerGold);
            player.gameObject.GetComponent<Inventory>().SetBoxPieces(playerBoxes);
            player.SetLives(5);
            GetComponent<CharacterController>().enabled = true;
        }
    }

    public int GetSaveSlot()
    {
        return saveSlot;
    }

    public void SetSaveSlot(int _newSave)
    {
        saveSlot = _newSave;
    }
}
