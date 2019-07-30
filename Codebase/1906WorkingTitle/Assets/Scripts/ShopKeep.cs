using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeep : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Shop;

    private void Start()
    {
        Shop = transform.Find("Shop Camera").gameObject;
    }

    public void OpenShop()
    {
        Instantiate(Shop);
    }

}
