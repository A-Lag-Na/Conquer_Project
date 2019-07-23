using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    // Start is called before the first frame update

    //recorded stats
    private Player player;
    private float health, attackSpeed;
    private int lives = 5, coins, level, defense, attackDamage;
    private Sprite slotOne, slotTwo;

    //UI elements to remember
    private Text healthText, livesText, coinText, statsText;
    private RectTransform healthTransform;
    private Image InvSlot1, InvSlot2, damageFlasher;


    void Start()
    {
        //grab player GameObject
        if(GameObject.Find("Player"))
            player = GameObject.Find("Player").GetComponent<Player>();

        //update health
        health = player.GetHealth();
        healthTransform = transform.Find("Health Bar").GetChild(0).GetComponent<RectTransform>();
        Vector3 HealthScale = healthTransform.localScale;
        if(player.GetMaxHealth()!=0)
            HealthScale.x = health / player.GetMaxHealth();
        else
            Debug.Log("Error on first healthscale");
        healthTransform.localScale = HealthScale;
        healthText = transform.Find("Health Bar").GetChild(1).GetComponent<Text>();
        healthText.text = $"health/{player.GetMaxHealth()}";

        //update coin count
        coins = player.GetCoins();
        coinText = transform.Find("Coins Icon").GetChild(0).GetComponent<Text>();
        coinText.text = $"X{coins}";

        //update lives count
        lives = player.GetLives();
        livesText = transform.Find("Lives Icon").GetChild(0).GetComponent<Text>();
        livesText.text = $"X{lives}";

        //update stats text
        level = player.GetLevel();
        defense = player.GetDefense();
        attackSpeed = player.GetAttackSpeed();
        attackDamage = player.GetDamage();
        statsText = transform.Find("Stats").GetComponent<Text>();
        statsText.text = $"Level - {level}\nDefense - {defense}\nAttack Speed - {attackSpeed}\nAttack Strength - {attackDamage}";

        //update inventory slots
        InvSlot1 = transform.Find("Inventory Slot 1").GetComponent<Image>();
        InvSlot2 = transform.Find("Inventory Slot 2").GetComponent<Image>();
        
        //grab damage flashing panel
        damageFlasher = transform.Find("DamagePanel").GetComponent<Image>();
        
    }
    

    #region inDev
    //void UpdateCoins()
    //{

    //    //update coin count
    //    //float coins = player.GetCoins();
    //    transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "X" + coins.ToString();
    //}

    //void UpdateLives()
    //{

    //    //update lives count
    //    //float lives = player.GetLives();
    //    transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "X" + lives.ToString();
    //}

    //void UpdateHealth()
    //{

    //    //update health
    //    //float health = player.GetHealth();
    //    Vector3 scale = transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().localScale;
    //    scale.x = health / 100.0f;
    //    transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().localScale = scale;
    //    transform.GetChild(1).GetChild(1).GetComponent<Text>().text = health.ToString() + "/100";
    //}

    //void UpdateInventorySlot1()
    //{

    //    //transform.GetChild(3).GetComponent<Image>().sprite = slotOne;
    //}

    //void UpdateInventorySlot2()
    //{

    //    //transform.GetChild(4).GetComponent<Image>().sprite = slotTwo;
    //}
    #endregion
    

    public void TakeDamage()
    {
        damageFlasher.color = new Color(255.0f, 0.0f, 0.0f, 0.25f);
    }
    

    private void Update()
    {
        //update health
        health = player.GetHealth();
        Vector3 HealthScale = healthTransform.localScale;
        if (player.GetMaxHealth() != 0)
            HealthScale.x = health / player.GetMaxHealth();
        else
            Debug.Log("Error on second healthscale");
        healthTransform.localScale = HealthScale;
        healthText.text = $"{health} / {player.GetMaxHealth()}";

        //update coin count
        coins = player.GetCoins();
        coinText.text = $"X{coins}";

        //update lives count
        lives = player.GetLives();
        livesText.text = $"X{lives}";

        //update inventory
        InvSlot1.sprite = slotOne;
        InvSlot2.sprite = slotTwo;

        //update player stats
        level = player.GetLevel();
        defense = player.GetDefense();
        attackSpeed = player.GetAttackSpeed();
        attackDamage = player.GetDamage();
        statsText.text = $"Level - {level}\nDefense - {defense}\nAttack Speed - {attackSpeed}\nAttack Strength - {attackDamage}";

        //taking damage
        if (damageFlasher.color != new Color(255.0f, 0.0f, 0.0f, 0.0f))
        {
            damageFlasher.color = Color.Lerp(damageFlasher.color, new Color(255.0f, 0.0f, 0.0f, 0.0f), 0.1f);
        }
    }
}
