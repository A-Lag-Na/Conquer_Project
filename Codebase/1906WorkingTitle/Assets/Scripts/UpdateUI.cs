using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Update_UI : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float health = 100.0f;
    [SerializeField] int lives = 5;
    [SerializeField] int coins = 10;
    [SerializeField] Sprite slotOne;
    [SerializeField] Sprite slotTwo;
    [SerializeField] int level;
    [SerializeField] int defense;
    [SerializeField] int attackSpeed;
    [SerializeField] int attackStrength;
    void Start()
    {
        //update health
        //float health = player.GetHealth();
        Vector3 scale = transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().localScale;
        scale.x = health / 100.0f;
        transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().localScale = scale;
        transform.GetChild(1).GetChild(1).GetComponent<Text>().text = health.ToString() + "/100";

        //update coin count
        //float coins = player.GetCoins();
        transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "X" + coins.ToString();

        //update lives count
        //float lives = player.GetLives();
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "X" + lives.ToString();
    }

    void UpdateCoins()
    {

        //update coin count
        //float coins = player.GetCoins();
        transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "X" + coins.ToString();
    }

    void UpdateLives()
    {

        //update lives count
        //float lives = player.GetLives();
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "X" + lives.ToString();
    }

    void UpdateHealth()
    {

        //update health
        //float health = player.GetHealth();
        Vector3 scale = transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().localScale;
        scale.x = health / 100.0f;
        transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().localScale = scale;
        transform.GetChild(1).GetChild(1).GetComponent<Text>().text = health.ToString() + "/100";
    }

    void UpdateInventorySlot1()
    {

        //transform.GetChild(3).GetComponent<Image>().sprite = slotOne;
    }

    void UpdateInventorySlot2()
    {

        //transform.GetChild(4).GetComponent<Image>().sprite = slotTwo;
    }

    private void Update()
    {
        //update health
        //float health = player.GetHealth();
        Vector3 scale = transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().localScale;
        scale.x = health / 100.0f;
        transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().localScale = scale;
        transform.GetChild(1).GetChild(1).GetComponent<Text>().text = health.ToString() + "/100";

        //update coin count
        //float coins = player.GetCoins();
        transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "X" + coins.ToString();

        //update lives count
        //float lives = player.GetLives();
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "X" + lives.ToString();

        //update inventory

        transform.GetChild(3).GetComponent<Image>().sprite = slotOne;
        transform.GetChild(4).GetComponent<Image>().sprite = slotTwo;

        //update player stats

        transform.GetChild(5).GetComponent<Text>().text = $"Level - {level}\nDefense - {defense}\nAttack Speed - {attackSpeed}\nAttack Strength - {attackStrength}";
    }
}
