using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public bool buy = true;

    private Inventory inventory;
    private Player player;
    private int coins;
    private Text coinText;
    private Button Buy, Sell, Exit;
    private GameObject mainUI;
    //List<BaseItem> shopItems = new List<BaseItem>();
    BaseItem currentItem;

    // Start is called before the first frame update
    void Start()
    {
        Buy.onClick.AddListener(OpenBuyMenu);
        Sell.onClick.AddListener(OpenSellMenu);
        Exit.onClick.AddListener(ExitMenu);

        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            if ((go.name != "Shop UI(Clone)" && go.name != "Main UI(Clone)" && go.name != "Pause Menu(Clone)"))
                go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
        }

        player = GameObject.Find("Player").GetComponent<Player>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();

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


        //update coin count
        coins = player.GetCoins();
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
        Destroy(gameObject);
    }
    #endregion

    public void Purchase()
    {
        inventory.AddCoins(currentItem.GetValue());
    }

    void Checkout(BaseItem _item)
    {
        if(_item.GetValue() < coins)
        {
            currentItem = _item;
        }
        else
        {

        }
    }

    
}
