using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public bool buy = true;

    private Inventory inventory;
    private int coins;
    private Text coinText, purchaseText;
    private Button Buy, Sell, Exit;
    private GameObject mainUI;
    //List<BaseItem> shopItems = new List<BaseItem>();
    BaseItem currentItem;

    // Start is called before the first frame update
    void Start()
    {

        Buy = transform.Find("Buy").GetComponent<Button>();
        Buy.onClick.AddListener(OpenBuyMenu);

        Sell = transform.Find("Sell").GetComponent<Button>();
        Sell.onClick.AddListener(OpenSellMenu);

        Exit = transform.Find("Exit").GetComponent<Button>();
        Exit.onClick.AddListener(ExitMenu);

        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            if ((go.name != "Shop UI(Clone)" && go.name != "Main UI(Clone)" && go.name != "Pause Menu(Clone)"))
                go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
        }
        
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        purchaseText = transform.Find("Display").GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (buy)
        //{

        //}
        //else
        //{

        //}


        //update coin count
        coins = inventory.GetCoins();
        coinText = transform.Find("Coins Icon").GetChild(0).GetComponent<Text>();
        coinText.text = $"X{coins}";


        //exit stat screen and reenable main ui
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitMenu();
        }

    }

    #region Buttons
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
        Destroy(transform.parent.gameObject);
    }
    #endregion

    public void Purchase()
    {
        if (currentItem != null)
        {
            inventory.AddCoins(-1 * currentItem.GetValue());
            if (currentItem.ItemType() == BaseItem.Type.Weapon)
            {
                inventory.ChangeWeapon(currentItem);
            }
            else
            {
                inventory.ChangePotion(currentItem);
            }
            currentItem = null;
            purchaseText.text = "";
        }
    }

    public void Checkout(BaseItem _item)
    {
        if(_item.GetValue() < coins)
        {
            currentItem = _item;
            purchaseText.text = $"{currentItem.name}\n{currentItem.GetValue()} Coins";
        }
        else
        {
            //add popup dialog denying
        }
    }

    
}
