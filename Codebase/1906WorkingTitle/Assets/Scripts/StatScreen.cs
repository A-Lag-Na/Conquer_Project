using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreen : MonoBehaviour
{
    private Button speedBTN, damageBTN, defenseBTN;
    [SerializeField] Player player;
    private float movementSpeed, currentHealth, maxHealth, currentExperience, nextLevelExp ,attackSpeed;
    private int defense, damage, level, pointsAvailable;

    private Text levelText, healthText, movementSpeedText, attackSpeedText, damageText, defenseText, pointsText;
    private RectTransform levelTransform;
    private GameObject mainUI;
    
    // Start is called before the first frame update
    void Start()
    {
        mainUI = GameObject.Find("Main UI");
        mainUI.SetActive(false);
        //assign buttons
        speedBTN = transform.Find("Attack Speed").GetChild(0).GetComponent<Button>();
        damageBTN = transform.Find("Attack Damage").GetChild(0).GetComponent<Button>();
        defenseBTN = transform.Find("Defense").GetChild(0).GetComponent<Button>();

        //assign funsction listeners
        speedBTN.onClick.AddListener(AddSpeed);
        damageBTN.onClick.AddListener(AddDamage);
        defenseBTN.onClick.AddListener(AddDefense);

        //assign player if found
        if(GameObject.Find("Player"))
            player = GameObject.Find("Player").GetComponent<Player>();

        //assign Texts
        levelText = GameObject.Find("Level").GetComponent<Text>();
        healthText = GameObject.Find("Health").GetComponent<Text>();
        movementSpeedText = GameObject.Find("Speed").GetComponent<Text>();
        attackSpeedText = GameObject.Find("Attack Speed").GetComponent<Text>();
        damageText = GameObject.Find("Attack Damage").GetComponent<Text>();
        defenseText = GameObject.Find("Defense").GetComponent<Text>();
        pointsText = GameObject.Find("Available Points").GetComponent<Text>();

        //grab level bar RectTransform
        levelTransform = transform.Find("Level").GetChild(0).GetComponent<RectTransform>();

        //update level
        level = player.GetLevel();
        levelText.text = $"Level {level}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t{level + 1}";

        //update level bar
        currentExperience = player.GetExperience();
        nextLevelExp = player.GetNextLevelExperience();
        Vector3 levelScale = levelTransform.localScale;
        levelScale.x = currentExperience / nextLevelExp;
        levelTransform.localScale = levelScale;

        //update health
        currentHealth = player.GetHealth();
        maxHealth = player.GetMaxHealth();
        healthText.text = $"Current / Max Health\t\t\t\t\t\t\t\t\t{currentHealth} / {maxHealth}";

        //update movement speed
        movementSpeed = player.GetMovementSpeed();
        movementSpeedText.text = $"Speed\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t{movementSpeed}";

        //update attack speed
        attackSpeed = player.GetAttackSpeed();
        attackSpeedText.text = $"Attack Speed\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t{attackSpeed}";

        //update damage
        damage = player.GetDamage();
        damageText.text = $"Attack Damage\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t{damage}";

        //update defense
        defense = player.GetDefense();
        defenseText.text = $"Defense\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t{defense}";

        //update available points
        pointsAvailable = player.GetSpendingPoints();
        pointsText.text = $"{pointsAvailable}\t\tPoints Available";

        Time.timeScale = 0;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            if ((go.name != "Shop UI(Clone)" && go.name != "Main UI(Clone)" && go.name != "Pause Menu(Clone)"))
                go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnEnable()
    {
        if (mainUI != null)
        {
            Time.timeScale = 0;
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
            {
                if ((go.name != "Shop UI(Clone)" && go.name != "Main UI(Clone)" && go.name != "Pause Menu(Clone)"))
                    go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
            }
            mainUI.SetActive(false);
        }
    }

    public void AddSpeed()
    {
        if (pointsAvailable > 0)
        {
            player.AddAttackSpeed();
        }
    }

    public void AddDamage()
    {
        if (pointsAvailable > 0)
            player.AddDamage();
    }

    public void AddDefense()
    {
        if (pointsAvailable > 0)
            player.AddDefense();
    }

    private void Update()
    {
        //update level
        level = player.GetLevel();
        levelText.text = $"Level {level}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t{level + 1}";

        //update level bar
        currentExperience = player.GetExperience();
        nextLevelExp = player.GetNextLevelExperience();
        Vector3 levelScale = levelTransform.localScale;
        levelScale.x = currentExperience / nextLevelExp;
        levelTransform.localScale = levelScale;

        //update health
        currentHealth = player.GetHealth();
        maxHealth = player.GetMaxHealth();
        healthText.text = $"Current / Max Health\t\t\t\t\t\t\t\t\t{currentHealth} / {maxHealth}";

        //update movement speed
        movementSpeed = player.GetMovementSpeed();
        movementSpeedText.text = $"Speed\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t{movementSpeed}";

        //update attack speed
        attackSpeed = player.GetAttackSpeed();
        attackSpeedText.text = $"Attack Speed\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t{attackSpeed}";

        //update damage
        damage = player.GetDamage();
        damageText.text = $"Attack Damage\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t{damage}";

        //update defense
        defense = player.GetDefense();
        defenseText.text = $"Defense\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t{defense}";

        //update available points
        pointsAvailable = player.GetSpendingPoints();
        pointsText.text = $"{pointsAvailable}\t\tPoints Available";

        //exit stat screen and reenable main ui
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            ResumeGame();
        }
    }

    void ResumeGame()
    {
        mainUI.SetActive(true);
        Time.timeScale = 1;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
        }
    }
    
}
