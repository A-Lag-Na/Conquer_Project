using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public bool buy = true;

    [SerializeField] Button Buy, Sell, Exit;
    

    // Start is called before the first frame update
    void Start()
    {
        Buy.onClick.AddListener(OpenBuyMenu);
        Sell.onClick.AddListener(OpenSellMenu);
        Exit.onClick.AddListener(ExitMenu);
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
    }

    void OpenBuyMenu()
    {
        Debug.Log("Clicked Buy");
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
