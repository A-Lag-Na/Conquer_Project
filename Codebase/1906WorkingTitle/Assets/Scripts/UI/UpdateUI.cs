using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    #region RecordedStats
    //Color flashes
    [SerializeField] Color damageColor, levelColorOpaque, levelColorTransparent = Color.clear;
    [SerializeField] private Player player = null;
    [SerializeField] private Inventory inventory = null;
    private Text healthText, livesText, coinText, InvSlot1Name, InvSlot2Name = null, buttonPromptText;
    private RectTransform healthTransform, levelTransform = null;
    private Image InvSlot1, InvSlot2, damageFlasher, levelFlasher, buttonPrompt = null;

    private float health, maxHealth, currentExperience, nextLevelExp = 0.0f;
    private int lives, coins = 0;
    
    //distance from shop
    private float dist = 0.0f;

    // stat screen and pause menu references to keep
    private GameObject statScreen, pauseMenu = null;

    //bool for level up flashing
    private bool levelUp = false;
    #endregion

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
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        }

        healthTransform = transform.Find("Health").GetChild(1).GetComponent<RectTransform>();
        healthText = transform.Find("Health").GetChild(2).GetComponent<Text>();

        coinText = transform.Find("Coins Icon").GetChild(0).GetComponent<Text>();
        livesText = transform.Find("Lives Icon").GetChild(0).GetComponent<Text>();

        levelTransform = transform.Find("Level").GetChild(1).GetComponent<RectTransform>();
        levelFlasher = transform.Find("Level").Find("LevelUpFlash").GetComponent<Image>();

        buttonPrompt = transform.Find("ButtonPrompt").GetComponent<Image>();
        buttonPromptText = transform.Find("ButtonPrompt").GetChild(0).GetComponent<Text>();

        //update inventory slots
        InvSlot1 = transform.Find("Inventory").Find("Inventory Slot 1").GetComponent<Image>();
        InvSlot1Name = transform.Find("Inventory").Find("Inventory Slot 1").GetChild(0).GetComponent<Text>();
        InvSlot2 = transform.Find("Inventory").Find("Inventory Slot 2").GetComponent<Image>();
        InvSlot2Name = transform.Find("Inventory").Find("Inventory Slot 2").GetChild(0).GetComponent<Text>();

        //grab damage flashing panel
        damageFlasher = transform.Find("DamagePanel").GetComponent<Image>();

        //set flash colors
        damageColor = new Color(255.0f, 0.0f, 0.0f, 0.0f);
        levelColorOpaque = new Color32(1, 210, 231, 128);
        levelColorTransparent = new Color32(1, 210, 231, 0);
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
            healthText.text = $"{(int)health} / {maxHealth}";
            #endregion

            #region Level Update
            //update level bar
            if (!levelUp)
            {
                currentExperience = player.GetExperience();
                nextLevelExp = player.GetNextLevelExperience();
                Vector3 levelScale = levelTransform.localScale;
                levelScale.x = currentExperience / nextLevelExp;
                levelTransform.localScale = levelScale;
            }
            else
            {
                levelTransform.localScale = new Vector3(1, 1, 1);
            }
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
            InvSlot2.sprite = inventory.ConsumableSprite();
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
                buttonPromptText.text = "Level Up!";
            }
        }
        if (GameObject.Find("Shop Keeper") != null && dist <= 8.2f)
        {
            buttonPrompt.color = new Color32(255, 255, 255, 255);
            buttonPromptText.text = "Shop";
        }
        else if (buttonPromptText.text != "Level Up!")
        {
            buttonPrompt.color = new Color32(0, 0, 0, 0);
            buttonPromptText.text = "";
        }
        #endregion

        #region InputCheck
            //check for menu or inventory input
            if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !statScreen.activeSelf)
                PauseGame();

            if (Input.GetButtonDown("Open Stats"))
            {
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

    #region UIFunctions
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
        buttonPromptText.text = "Level Up!";
    }

    void PauseGame()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        if (statScreen != null)
            statScreen.SetActive(false);
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    void OpenStats()
    {
        if (statScreen != null)
            statScreen.SetActive(true);
    }

    void OpenShop()
    {
        if (dist <= 8.2f)
            if (GameObject.Find("Shop Keeper") != null)
                GameObject.Find("Shop Keeper").GetComponent<ShopKeep>().OpenShop();
    }

    public void StopLevelFlashing()
    {
        levelUp = false;
        levelFlasher.color = levelColorTransparent;
        buttonPrompt.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }
    
    #endregion
}
