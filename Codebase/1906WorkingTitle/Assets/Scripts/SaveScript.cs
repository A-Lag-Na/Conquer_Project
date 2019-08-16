using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    Player player = null;
    void Start()
    {
        player = GetComponent<Player>();
    }

    public void Save()
    {
        Vector3 playerPosition = player.GetPosition();
        float playerMovementSpeed = player.GetMovementSpeed();
        float playerHealth = player.GetHealth();
        float maxPlayerHealth = player.GetMaxHealth();
        float playerAttackSpeed = player.GetFireRate();
        float playerExperience = player.GetExperience();
        float nextLevelExperience = player.GetNextLevelExperience();
        int playerDefense = player.GetDefense();
        int visualAttackSpeed = player.GetAttackSpeed();
        int playerAttackDamage = player.GetDamage();
        int playerLevel = player.GetLevel();
        int playerSpendingPoints = player.GetSpendingPoints();
        int playerLives = player.GetLives();

        PlayerPrefs.SetFloat("PlayerPositionX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerPositionY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", playerPosition.z);
        PlayerPrefs.SetFloat("MoveSpeed", playerMovementSpeed);
        PlayerPrefs.SetFloat("Health", playerHealth);
        PlayerPrefs.SetFloat("MaxHealth", maxPlayerHealth);
        PlayerPrefs.SetFloat("AttackSpeed", playerAttackSpeed);
        PlayerPrefs.SetFloat("Exp", playerExperience);
        PlayerPrefs.SetFloat("NextLevelExp", nextLevelExperience);
        PlayerPrefs.SetInt("Defense", playerDefense);
        PlayerPrefs.SetInt("VisualAttackSpeed", visualAttackSpeed);
        PlayerPrefs.SetInt("Damage", playerAttackDamage);
        PlayerPrefs.SetInt("Level", playerLevel);
        PlayerPrefs.SetInt("SpendingPoints", playerSpendingPoints);
        PlayerPrefs.SetInt("Lives", playerLives);

        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("PlayerPositionX"))
        {
            float playerPositionX = PlayerPrefs.GetFloat("PlayerPositionX");
            float playerPositionY = PlayerPrefs.GetFloat("PlayerPositionY");
            float playerPositionZ = PlayerPrefs.GetFloat("PlayerPositionZ");
            float playerMovementSpeed = PlayerPrefs.GetFloat("MoveSpeed");
            float playerHealth = PlayerPrefs.GetFloat("Health");
            float maxPlayerHealth = PlayerPrefs.GetFloat("MaxHealth");
            float playerAttackSpeed = PlayerPrefs.GetFloat("AttackSpeed");
            float playerExperience = PlayerPrefs.GetFloat("Exp");
            float nextLevelExperience = PlayerPrefs.GetFloat("NextLevelExp");
            int playerDefense = PlayerPrefs.GetInt("Defense");
            int visualAttackSpeed = PlayerPrefs.GetInt("VisualAttackSpeed");
            int playerAttackDamage = PlayerPrefs.GetInt("Damage");
            int playerLevel = PlayerPrefs.GetInt("Level");
            int playerSpendingPoints = PlayerPrefs.GetInt("SpendingPoints");
            int playerLives = PlayerPrefs.GetInt("Lives");

            player.GetComponent<CharacterController>().enabled = false;
            player.SetPosition(new Vector3(playerPositionX, playerPositionY, playerPositionZ));
            player.SetMovementSpeed(playerMovementSpeed);
            player.SetHealth(playerHealth);
            player.SetMaxHealth(maxPlayerHealth);
            player.SetFireRate(playerAttackSpeed);
            player.SetExperience(playerExperience);
            player.SetNextLevelExperience(nextLevelExperience);
            player.SetDefense(playerDefense);
            player.SetAttackSpeed(visualAttackSpeed);
            player.SetDamage(playerAttackDamage);
            player.SetLevel(playerLevel);
            player.SetSpendingPoints(playerSpendingPoints);
            player.SetLives(playerLives);
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
