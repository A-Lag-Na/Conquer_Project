using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreen : MonoBehaviour
{
    private Button speedBTN, damageBTN, defenseBTN;
    private Player player;
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
    }

    private void AddSpeed()
    {
        player.AddAttackSpeed();
    }

    private void AddDamage()
    {
        player.AddDamage();
    }

    private void AddDefense()
    {
        player.AddDefense();
    }


}
