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
        yield return new WaitForSeconds(20f);
        text.GetComponent<RectTransform>().position = new Vector3();
        text.GetComponent<Text>().text = "Press escape";
    }
}
