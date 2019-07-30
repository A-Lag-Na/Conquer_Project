using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    // Start is called before the first frame update

    //recorded stats
    private Player player;
    private float health, attackSpeed, currentExperience, nextLevelExp;
    private int lives = 5, coins, level, defense, attackDamage;
    private Sprite slotOne, slotTwo;

    //UI elements to remember
    private Text healthText, livesText, coinText, statsText;
    private RectTransform healthTransform, levelTransform;
    private Image InvSlot1, InvSlot2, damageFlasher;

    void Start()
    {

        //grab player GameObject
        if (GameObject.Find("Player"))
            player = GameObject.Find("Player").GetComponent<Player>();
        healthTransform = transform.Find("Health Bar").GetChild(0).GetComponent<RectTransform>();
        healthText = transform.Find("Health Bar").GetChild(1).GetComponent<Text>();
        coinText = transform.Find("Coins Icon").GetChild(0).GetComponent<Text>();
        livesText = transform.Find("Lives Icon").GetChild(0).GetComponent<Text>();
        statsText = transform.Find("Stats").GetComponent<Text>();
        levelTransform = transform.Find("XP Bar").GetChild(0).GetComponent<RectTransform>();
        #region start
        ////update health
        //health = player.GetHealth();
        //Vector3 HealthScale = healthTransform.localScale;
        //if (player.GetMaxHealth() != 0)
        //    HealthScale.x = health / player.GetMaxHealth();
        //healthTransform.localScale = HealthScale;
        //healthText.text = $"health/{player.GetMaxHealth()}";

        ////update coin count
        //coins = player.GetCoins();
        //coinText.text = $"X{coins}";

        ////update lives count
        //lives = player.GetLives();
        //livesText.text = $"X{lives}";

        ////update stats text
        //level = player.GetLevel();
        //defense = player.GetDefense();
        //attackSpeed = player.GetAttackSpeed();
        //attackDamage = player.GetDamage();
        //statsText.text = $"Level - {level}\nDefense - {defense}\nAttack Speed - {attackSpeed}\nAttack Strength - {attackDamage}";
        #endregion
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
        healthTransform.localScale = HealthScale;
        healthText.text = $"{health} / {player.GetMaxHealth()}";

        //update level bar
        currentExperience = player.GetExperience();
        nextLevelExp = player.GetNextLevelExperience();
        Vector3 levelScale = levelTransform.localScale;
        levelScale.x = currentExperience / nextLevelExp;
        levelTransform.localScale = levelScale;

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


        //exit stat screen and reenable main ui
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenStats();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            OpenShop();
        }
    }

    void PauseGame()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Pause Menu"));
        Destroy(gameObject);
    }

    void OpenStats()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Stat Screen"));
        Destroy(gameObject);
    }

    void OpenShop()
    {
        //Instantiate(Resources.Load<GameObject>("Prefabs/Shop Camera"));
        float dist = Vector3.Distance(GameObject.Find("Shop Keeper").GetComponent<Transform>().position, transform.position);
        if (dist <= 100.0f)
        {
            GameObject.Find("Shop Keeper").GetComponent<ShopKeep>().OpenShop();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"Distance is {dist}");
        }
    }
}
