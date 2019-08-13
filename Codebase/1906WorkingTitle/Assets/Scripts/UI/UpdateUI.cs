﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    // Start is called before the first frame update

    #region RecordedStats
    [SerializeField] private Player player = null;
    [SerializeField] private Inventory inventory = null;
    private float health, maxHealth, currentExperience, nextLevelExp = 0.0f;
    private int lives, coins = 0;
    #endregion

    #region UI elements to remember
    private Text healthText, livesText, coinText, InvSlot1Name, InvSlot2Name = null;
    private RectTransform healthTransform, levelTransform = null;
    private Image InvSlot1, InvSlot2, damageFlasher, levelFlasher, buttonPrompt = null;
    private Sprite cSprite, tabSprite = null;
    #endregion

    //Color flashes
    [SerializeField] Color damageColor, levelColorOpaque, levelColorTransparent;

    //distance from shop
    float dist;

    // stat screen and pause menu references to keep
    private GameObject statScreen, pauseMenu;

    //bool for level up flashing
    private bool levelUp = false;

    void Start()
    {
        if (GameObject.Find("Pause Menu"))
        {
            pauseMenu = GameObject.Find("Pause Menu").gameObject;
            pauseMenu.SetActive(false);
        }

        if (GameObject.Find("Stat Screen"))
        {
            statScreen = GameObject.Find("Stat Screen").gameObject;
            statScreen.SetActive(false);
        }
        //grab player GameObject
        if (GameObject.Find("Player"))
        {
            player = GameObject.Find("Player").GetComponent<Player>();
            inventory = GameObject.Find("Player").GetComponent<Inventory>();
        }

        healthTransform = transform.Find("Health Bar").GetChild(0).GetComponent<RectTransform>();
        healthText = transform.Find("Health Bar").GetChild(1).GetComponent<Text>();
        coinText = transform.Find("Coins Icon").GetChild(0).GetComponent<Text>();
        livesText = transform.Find("Lives Icon").GetChild(0).GetComponent<Text>();
        levelTransform = transform.Find("XP Bar").GetChild(0).GetComponent<RectTransform>();
        levelFlasher = transform.Find("XP Bar").transform.Find("LevelUpFlash").GetComponent<Image>();
        buttonPrompt = transform.Find("ButtonPrompt").GetComponent<Image>();

        //update inventory slots
        InvSlot1 = transform.Find("Inventory Slot 1").GetComponent<Image>();
        InvSlot1Name = transform.Find("Inventory Slot 1").GetChild(0).GetComponent<Text>();
        InvSlot2 = transform.Find("Inventory Slot 2").GetComponent<Image>();
        InvSlot2Name = transform.Find("Inventory Slot 2").GetChild(0).GetComponent<Text>();

        //grab damage flashing panel
        damageFlasher = transform.Find("DamagePanel").GetComponent<Image>();

        //set flash colors
        damageColor = new Color(255.0f, 0.0f, 0.0f, 0.0f);
        levelColorOpaque = new Color32(1, 210, 231, 128);
        levelColorTransparent = new Color32(1, 210, 231, 0);

        //set button sprites
        cSprite = Resources.Load<Sprite>("Sprites/c_sprite");
        tabSprite = Resources.Load<Sprite>("Sprites/tab_sprite");
    }

    private void OnEnable()
    {
        if (statScreen != null)
            statScreen.SetActive(false);
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    public void TakeDamage()
    {
        damageFlasher.color = new Color(255.0f, 0.0f, 0.0f, 0.25f);
    }

    public void LevelUp()
    {
        levelUp = true;
        buttonPrompt.color = new Color32(255, 255, 255, 255);
        buttonPrompt.sprite = tabSprite;
    }

    private void Update()
    {
        #region UIUpdates
        if (player != null)
        {
            #region Health Update
            //update health
            health = player.GetHealth();
            maxHealth = player.GetMaxHealth();
            Vector3 HealthScale = healthTransform.localScale;
            if (player.GetMaxHealth() != 0)
                HealthScale.x = health / maxHealth;
            healthTransform.localScale = HealthScale;
            healthText.text = $"{health} / {maxHealth}";
            #endregion

            #region Level Update
            //update level bar
            currentExperience = player.GetExperience();
            nextLevelExp = player.GetNextLevelExperience();
            Vector3 levelScale = levelTransform.localScale;
            levelScale.x = currentExperience / nextLevelExp;
            levelTransform.localScale = levelScale;
            #endregion

            #region Coin Update
            //update coin count
            coins = player.GetCoins();
            coinText.text = $"X{coins}";
            #endregion

            #region Lives Update
            //update lives count
            lives = player.GetLives();
            livesText.text = $"X{lives}";
            #endregion

            #region Inventory Update
            //update inventory
            InvSlot1.sprite = inventory.WeaponSprite();
            InvSlot2.sprite = inventory.PotionSprite();
            InvSlot1Name.text = inventory.WeaponName();
            InvSlot2Name.text = inventory.ConsumableName();
            #endregion
        }
        #endregion

        #region DamageFlash
        //taking damage
        if (damageFlasher.color != damageColor)
            damageFlasher.color = Color.Lerp(damageFlasher.color, damageColor, 0.1f);
        #endregion

        #region Level up
        if (levelUp)
            levelFlasher.color = Color.Lerp(levelColorTransparent, levelColorOpaque, Mathf.PingPong(Time.time * 2, 1));
        #endregion

        #region Button Prompts
        //check if near shop
        if (GameObject.Find("Shop Keeper") != null)
        {
            dist = Vector3.Distance(GameObject.Find("Shop Keeper").GetComponent<Transform>().position, player.transform.position);
            if (levelUp)
            {
                buttonPrompt.color = new Color32(255, 255, 255, 255);
                buttonPrompt.sprite = tabSprite;
            }
        }
        if (dist <= 5.2f)
        {
            buttonPrompt.color = new Color32(255, 255, 255, 255);
            buttonPrompt.sprite = cSprite;
        }
        else if (buttonPrompt.sprite != tabSprite)
            buttonPrompt.color = new Color32(0, 0, 0, 0);
        #endregion

        #region InputCheck
        //check for menu or inventory input
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            PauseGame();

        if (Input.GetButtonDown("Open Stats"))
        {
            levelUp = false;
            levelFlasher.color = levelColorTransparent;
            buttonPrompt.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            OpenStats();
        }

        if (Input.GetButtonDown("Use Potion"))
        {
            StartCoroutine(inventory.ConsumableTimer());
        }

        if (Input.GetButtonDown("Open Shop"))
            OpenShop();
        #endregion
    }

    void PauseGame()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(true);
    }

    void OpenStats()
    {
        if (statScreen != null)
            statScreen.SetActive(true);
    }

    void OpenShop()
    {
        if (dist <= 5.2f)
            if (GameObject.Find("Shop Keeper") != null)
                GameObject.Find("Shop Keeper").GetComponent<ShopKeep>().OpenShop();
    }
}
