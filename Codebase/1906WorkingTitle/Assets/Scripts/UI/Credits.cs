using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    GameObject text = null;
    
    void Start()
    {
        text = transform.Find("Credit").gameObject;
    }

    private void OnEnable()
    {
        StartCoroutine(Continue());
    }

    IEnumerator Continue()
    {
        yield return new WaitForSeconds(22f);
        GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenu>().CloseCredits();
    }
}
