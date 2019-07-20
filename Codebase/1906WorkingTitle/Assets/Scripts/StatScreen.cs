using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreen : MonoBehaviour
{
    private Button speedBTN, damageBTN, defenseBTN;
    [SerializeField] Player player;
    private float movementSpeed, currentHealth, maxHealth, currentExperience, attackSpeed;
    [SerializeField] int defense, damage, level, pointsAvailable;

    private Text levelText, healthText, movementSpeedText, attackSpeedText, damageText, defenseText, pointsText;
    private RectTransform levelTransform;
    
    // Start is called before the first frame update
    void Start()
    {
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
        //pointsAvailable = player.GetPointsLeft();
        pointsAvailable = 100000;
    }

    private void AddSpeed()
    {
        if (pointsAvailable > 0)
        {
            player.AddAttackSpeed();
        }
    }

    private void AddDamage()
    {
        if (pointsAvailable > 0)
            player.AddDamage();
    }

    private void AddDefense()
    {
        if (pointsAvailable > 0)
            player.AddDefense();
    }

    private void Update()
    {
        //update level
        level = player.GetLevel();
        levelText.text = $"Level {level}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t{level + 1}";

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
        //pointsAvailable = player.GetPointsLeft();
    }
}
