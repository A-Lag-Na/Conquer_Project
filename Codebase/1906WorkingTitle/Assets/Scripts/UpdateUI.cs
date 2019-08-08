using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    // Start is called before the first frame update

    #region RecordedStats
    [SerializeField] private Player player;
    [SerializeField] private Inventory inventory;
    private float health, maxHealth, currentExperience, nextLevelExp;
    private int lives, coins;
    #endregion

    #region UI elements to remember
    private Text healthText, livesText, coinText;
    private RectTransform healthTransform, levelTransform;
    private Image InvSlot1, InvSlot2, damageFlasher, levelFlasher, buttonPrompt;
    private Sprite cSprite, tabSprite;
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
        pauseMenu = GameObject.Find("Pause Menu").gameObject;
        pauseMenu.SetActive(false);
        statScreen = GameObject.Find("Stat Screen").gameObject;
        statScreen.SetActive(false);
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
        InvSlot2 = transform.Find("Inventory Slot 2").GetComponent<Image>();

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
        //update health
        health = player.GetHealth();
        maxHealth = player.GetMaxHealth();
        Vector3 HealthScale = healthTransform.localScale;
        if (player.GetMaxHealth() != 0)
            HealthScale.x = health / maxHealth;
        healthTransform.localScale = HealthScale;
        healthText.text = $"{health} / {maxHealth}";

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
        InvSlot1.sprite = player.GetWeapon();
        InvSlot2.sprite = player.GetPotion();


        //taking damage
        if (damageFlasher.color != damageColor)
            damageFlasher.color = Color.Lerp(damageFlasher.color, damageColor, 0.1f);

        if (levelUp)
            levelFlasher.color = Color.Lerp(levelColorTransparent, levelColorOpaque, Mathf.PingPong(Time.time * 2, 1));

        //check if near shop
        dist = Vector3.Distance(GameObject.Find("Shop Keeper").GetComponent<Transform>().position, player.transform.position);
        if (dist <= 5.2f)
        {
            buttonPrompt.color = new Color32(255, 255, 255, 255);
            buttonPrompt.sprite = cSprite;
        }
        else if (levelUp)
        {
            buttonPrompt.color = new Color32(255, 255, 255, 255);
            buttonPrompt.sprite = tabSprite;
        }
        else
            buttonPrompt.color = new Color32(0, 0, 0, 0);

        //exit stat screen and reenable main ui
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            PauseGame();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            levelUp = false;
            levelFlasher.color = levelColorTransparent;
            buttonPrompt.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            OpenStats();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.UsePotion();
        }

        if (Input.GetKeyDown(KeyCode.C))
            OpenShop();
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);
    }

    void OpenStats()
    {
        statScreen.SetActive(true);
    }

    void OpenShop()
    {
        if (dist <= 5.2f)
            GameObject.Find("Shop Keeper").GetComponent<ShopKeep>().OpenShop();
    }
}
