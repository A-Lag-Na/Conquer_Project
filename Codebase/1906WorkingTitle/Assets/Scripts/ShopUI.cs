﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public bool buy = true;

    [SerializeField] Button Buy, Sell, Exit;
    private GameObject mainUI;
    List<BaseItem> shopItems = new List<BaseItem>();

    // Start is called before the first frame update
    void Start()
    {
        Buy.onClick.AddListener(OpenBuyMenu);
        Sell.onClick.AddListener(OpenSellMenu);
        Exit.onClick.AddListener(ExitMenu);

        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (buy)
        {

        }
        else
        {

        }

        //exit stat screen and reenable main ui
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitMenu();
        }
    }

    void OpenBuyMenu()
    {
        Debug.Log("Clicked Buy");
        buy = true;
    }
    void OpenSellMenu()
    {
        Debug.Log("Clicked Sell");
        buy = false;
    }
    void ExitMenu()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Main UI"));
        Destroy(gameObject);
    }

}
