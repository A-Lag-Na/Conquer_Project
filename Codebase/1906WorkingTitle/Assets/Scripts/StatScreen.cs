using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreen : MonoBehaviour
{
    private Button speedBTN, damageBTN, defenseBTN;
    private Player player;
    private float movementSpeed, currentHealth, maxHealth, currentExperience, attackSpeed;
    private int defense, damage, level, pointsAvailable;

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

        //update health
        currentHealth = player.GetHealth();
        maxHealth = player.GetMaxHealth();

        //update movement speed
        movementSpeed = player.GetMovementSpeed();

        //update attack speed
        attackSpeed = player.GetAttackSpeed();

        //update damage
        damage = player.GetDamage();

        //update defense
        defense = player.GetDefense();

        //update available points
        //pointsAvailable = player.GetPointsLeft();
    }

    private void AddSpeed()
    {
        if(pointsAvailable>0)
            player.AddAttackSpeed();
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
        //pointsAvailable = player.GetPointsLeft();
    }
}
