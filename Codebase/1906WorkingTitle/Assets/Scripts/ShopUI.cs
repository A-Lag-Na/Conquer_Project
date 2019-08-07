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
        mainUI = GameObject.Find("Main UI");
        mainUI.SetActive(false);

        Buy = transform.Find("Buy").GetComponent<Button>();
        Buy.onClick.AddListener(OpenBuyMenu);

        Sell = transform.Find("Sell").GetComponent<Button>();
        Sell.onClick.AddListener(OpenSellMenu);
        
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        purchaseText = transform.Find("Display").GetChild(0).GetComponent<Text>();

        Time.timeScale = 0;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            if ((go.name != "Shop UI" && go.name != "Main UI" && go.name != "Pause Menu"))
                go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnEnable()
    {
        if (mainUI != null)
        {
            Time.timeScale = 0;
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
            {
                if ((go.name != "Shop UI" && go.name != "Main UI" && go.name != "Pause Menu"))
                    go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
            }
            mainUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

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
    public void ExitMenu()
    {
        //Instantiate(Resources.Load<GameObject>("Prefabs/Main UI"));
        mainUI.SetActive(true);
        Time.timeScale = 1;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
        }
        //Destroy(transform.parent.gameObject);
        transform.parent.gameObject.SetActive(false);
    }
    #endregion

    public void Purchase()
    {
        if (currentItem != null)
        {
            inventory.AddCoins(-1 * currentItem.GetValue());
            if (currentItem.ItemType() == BaseItem.Type.Weapon)
            {
                inventory.AddWeapon(currentItem);
            }
            else
            {
                inventory.AddPotion(currentItem);
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
