using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreen : MonoBehaviour
{
    #region StatScreenProperties
    [SerializeField] private Player player = null;
    private float movementSpeed, currentHealth, maxHealth, currentExperience, nextLevelExp, attackSpeed = 0.0f;
    private int defense, damage, level, pointsAvailable = 0;
    private Button speedBTN, damageBTN, defenseBTN = null;
    private Text levelText, healthText, movementSpeedText, attackSpeedText, damageText, defenseText, pointsText = null;
    private RectTransform levelTransform = null;
    private GameObject mainUI = null, attackMax = null;
    private StopWatch stopWatch;
    #endregion
    
    void Start()
    {
        stopWatch = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<StopWatch>();
        if (GameObject.Find("Main UI"))
        {
            mainUI = GameObject.Find("Main UI");
        }
        //assign buttons
        speedBTN = transform.Find("Attack Speed").GetChild(0).GetComponent<Button>();
        damageBTN = transform.Find("Attack Damage").GetChild(0).GetComponent<Button>();
        defenseBTN = transform.Find("Defense").GetChild(0).GetComponent<Button>();

        //assign function listeners
        speedBTN.onClick.AddListener(AddSpeed);
        damageBTN.onClick.AddListener(AddDamage);
        defenseBTN.onClick.AddListener(AddDefense);

        //assign player if found
        if (GameObject.FindGameObjectWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //assign Texts
        levelText = transform.Find("Level").GetComponent<Text>();
        healthText = transform.Find("Health").GetComponent<Text>();
        movementSpeedText = transform.Find("Speed").GetComponent<Text>();
        attackSpeedText = transform.Find("Attack Speed").GetComponent<Text>();
        damageText = transform.Find("Attack Damage").GetComponent<Text>();
        defenseText = transform.Find("Defense").GetComponent<Text>();
        pointsText = transform.Find("Available Points").GetComponent<Text>();
        attackMax = transform.Find("Attack Speed").GetChild(1).gameObject;
        attackMax.SetActive(false);

        //grab level bar RectTransform
        levelTransform = transform.Find("Level").GetChild(0).GetComponent<RectTransform>();

        //update level
        levelText.text = $"Level {level}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t{level + 1}";

        //update health
        healthText.text = $"Current / Max Health\t\t\t\t\t\t\t\t\t{currentHealth} / {maxHealth}";

        //update movement speed
        movementSpeedText.text = $"Speed\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t{movementSpeed}";

        //update attack speed
        attackSpeedText.text = $"Attack Speed\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t{attackSpeed}";

        //update damage
        damageText.text = $"Attack Damage\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t{damage}";

        //update defense
        defenseText.text = $"Defense\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t{defense}";

        Time.timeScale = 0;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
            if (go.name != "Stat Screen")
                go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);

        if(stopWatch != null)
            stopWatch.PauseStopWatch();
    }

    private void Update()
    {
        if (player != null)
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
            movementSpeedText.text = $"Speed\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t{(int)movementSpeed}";

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
            if(pointsAvailable == 0)
                mainUI.GetComponent<UpdateUI>().StopLevelFlashing();
        }
        //exit stat screen and reenable main ui
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Open Stats"))
            ResumeGame();
        if(player.GetAttackSpeed() <= 0.2f)
        {
            speedBTN.enabled = false;
            attackMax.SetActive(true);
        }
    }

    #region StatScreenFunctions
    private void OnEnable()
    {
        if (mainUI != null)
        {
            Time.timeScale = 0;
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
                if (go.name != "Stat Screen")
                    go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
            if(stopWatch != null)
                stopWatch.PauseStopWatch();
        }
    }

    public void AddSpeed()
    {
        if (pointsAvailable > 0)
            player.AddAttackSpeed();
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

    void ResumeGame()
    {
        Time.timeScale = 1;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
            go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
        if (stopWatch != null)
            stopWatch.ResumeStopWatch();
        if (mainUI != null)
            mainUI.GetComponent<UpdateUI>().ResumeGame();
    }
    #endregion
}