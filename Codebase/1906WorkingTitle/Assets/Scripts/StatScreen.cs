using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreen : MonoBehaviour
{
    private Button speedBTN, damageBTN, defenseBTN;
    // Start is called before the first frame update
    void Start()
    {
        //assign buttons
        speedBTN = transform.Find("Attack Speed").GetChild(0).GetComponent<Button>();
        damageBTN = transform.Find("Attack Damage").GetChild(0).GetComponent<Button>();
        defenseBTN = transform.Find("Defense").GetChild(0).GetComponent<Button>();

        //assign funsction listeners
        speedBTN.onClick.AddListener(AddSpeed);
        speedBTN.onClick.AddListener(AddDamage);
        speedBTN.onClick.AddListener(AddDefense);
    }

    private void AddSpeed()
    {

    }

    private void AddDamage()
    {

    }

    private void AddDefense()
    {

    }
}
