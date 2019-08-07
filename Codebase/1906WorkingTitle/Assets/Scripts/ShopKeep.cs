using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeep : MonoBehaviour
{
    [SerializeField] GameObject Shop;

    // Start is called before the first frame update
    private void Start()
    {
        Shop = transform.Find("Shop Camera").gameObject;
    }

    public void OpenShop()
    {
        Shop.SetActive(true);
    }
}
