using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    private Inventory inventory;
    private int coins;
    private Text coinText, purchaseText;
    private Button Exit;
    private GameObject mainUI, denyScreen;
    //List<BaseItem> shopItems = new List<BaseItem>();
    BaseItem currentItem;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Main UI"))
        {
            mainUI = GameObject.Find("Main UI");
            mainUI.SetActive(false);
        }

        denyScreen = transform.Find("Deny Screen").gameObject;
        denyScreen.SetActive(false);

        if(GameObject.Find("Player"))
            inventory = GameObject.Find("Player").GetComponent<Inventory>();
        purchaseText = transform.Find("Display").GetChild(0).GetComponent<Text>();

        Time.timeScale = 0;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
            if ((go.name != "Shop UI" && go.name != "Main UI" && go.name != "Pause Menu"))
                go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
    }

    // Update is called once per frame
    void Update()
    {

        //update coin count
        if(inventory != null)
            coins = inventory.GetCoins();
        coinText = transform.Find("Coins Icon").GetChild(0).GetComponent<Text>();
        coinText.text = $"X{coins}";

        //exit stat screen and reenable main ui
        if (Input.GetKeyDown(KeyCode.Escape))
            ExitMenu();
    }

    private void OnEnable()
    {
        if (mainUI != null)
        {
            Time.timeScale = 0;
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
                if ((go.name != "Shop UI" && go.name != "Main UI" && go.name != "Pause Menu"))
                    go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
            mainUI.SetActive(false);
        }
    }
    
    public void ExitMenu()
    {
        Time.timeScale = 1;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
            go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
        transform.parent.gameObject.SetActive(false);
        if(mainUI != null)
            mainUI.SetActive(true);
    }

    public void Purchase()
    {
        if (currentItem != null && currentItem.GetValue() <= coins)
        {
            inventory.AddCoins(-1 * currentItem.GetValue());
            if (currentItem.GetItemType() == BaseItem.Type.Weapon)
                inventory.AddWeapon(currentItem);
            else
                inventory.AddPotion(currentItem);
        }
        else if(currentItem != null && currentItem.GetValue() > coins)
        {
            DenyPuchase();
        }
    }

    public void Checkout(BaseItem _item)
    {
        currentItem = _item;
        purchaseText.text = $"{currentItem.name}\n{currentItem.GetValue()} Coins";
    }

    void DenyPuchase()
    {
        denyScreen.SetActive(true);
    }

    public void Continue()
    {
        denyScreen.SetActive(false);
    }

}
