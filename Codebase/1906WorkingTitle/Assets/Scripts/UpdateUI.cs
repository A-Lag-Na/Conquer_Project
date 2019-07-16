using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    // Start is called before the first frame update

    //recorded stats
    [SerializeField] float health = 10.0f;
    [SerializeField] int lives = 5;
    [SerializeField] int coins = 10;
    [SerializeField] Sprite slotOne, slotTwo;
    [SerializeField] int level;
    [SerializeField] int defense;
    [SerializeField] int attackSpeed;
    [SerializeField] int attackStrength;

    //UI elements to remember
    [SerializeField] Text healthText, livesText, coinText, statsText;
    [SerializeField] RectTransform healthTransform;
    [SerializeField] Image InvSlot1, InvSlot2, damageFlasher;
    
    
    void Start()
    {
        //update health
        //float health = player.GetHealth();
        healthTransform = transform.Find("Health Bar").GetChild(0).GetComponent<RectTransform>();
        Vector3 scale = healthTransform.localScale;
        scale.x = health / 10.0f;
        healthTransform.localScale = scale;
        healthText = transform.Find("Health Bar").GetChild(1).GetComponent<Text>();
        healthText.text = health.ToString() + "/10";

        //update coin count
        //float coins = player.GetCoins();
        coinText = transform.Find("Coins Icon").GetChild(0).GetComponent<Text>();
        coinText.text = "X" + coins.ToString();

        //update lives count
        //float lives = player.GetLives();
        livesText = transform.Find("Lives Icon").GetChild(0).GetComponent<Text>();
        livesText.text = "X" + lives.ToString();

        //update stats text
        statsText = transform.Find("Stats").GetComponent<Text>();

        //update inventory slots
        InvSlot1 = transform.Find("Inventory Slot 1").GetComponent<Image>();
        InvSlot2 = transform.Find("Inventory Slot 2").GetComponent<Image>();
        
        //grab damage flashing panel
        damageFlasher = transform.Find("DamageFlash").GetComponent<Image>();
        
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
        //float health = player.GetHealth();
        Vector3 HealthScale = transform.Find("Health Bar").GetChild(0).GetComponent<RectTransform>().localScale;
        HealthScale.x = health / 100.0f;
        transform.Find("Health Bar").GetChild(0).GetComponent<RectTransform>().localScale = HealthScale;
        healthText.text = health.ToString() + "/100";

        //update coin count
        //float coins = player.GetCoins();
        coinText.text = "X" + coins.ToString();

        //update lives count
        //float lives = player.GetLives();
        livesText.text = "X" + lives.ToString();

        //update inventory

        InvSlot1.sprite = slotOne;
        InvSlot2.sprite = slotTwo;

        //update player stats

        statsText.text = $"Level - {level}\nDefense - {defense}\nAttack Speed - {attackSpeed}\nAttack Strength - {attackStrength}";

        //taking damage
        //damageFlasher.color = Color.Lerp(new Color(255.0f, 255.0f, 255.0f, 125.0f), new Color(255.0f, 255.0f, 255.0f, 0.0f), damageTime);
        //if (damageTime < 1.0f)
        //{ // while t below the end limit...
        //  // increment it at the desired rate every update:
        //    damageTime += Time.deltaTime / damageTimeDuration;
        //}
        if (damageFlasher.color != new Color(255.0f, 0.0f, 0.0f, 0.0f))
        {
            damageFlasher.color = Color.Lerp(damageFlasher.color, new Color(255.0f, 0.0f, 0.0f, 0.0f), 0.1f);
        }
    }
}
