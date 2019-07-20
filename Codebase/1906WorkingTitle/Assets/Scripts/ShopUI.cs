using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public bool buy = true;

    [SerializeField] Button Buy, Sell, Exit;
    private GameObject mainUI;


    // Start is called before the first frame update
    void Start()
    {
        Buy.onClick.AddListener(OpenBuyMenu);
        Sell.onClick.AddListener(OpenSellMenu);
        Exit.onClick.AddListener(ExitMenu);


        //grab main ui if active and existing
        if (GameObject.Find("Main UI"))
        {
            mainUI = GameObject.Find("Main UI");
            mainUI.SetActive(false);
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
            if (mainUI != null && mainUI.activeSelf)
                mainUI.SetActive(true);
            Destroy(gameObject);
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
        Destroy(gameObject);
    }

}
