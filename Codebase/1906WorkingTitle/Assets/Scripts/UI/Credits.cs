using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    GameObject text = null;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.Find("Credit").gameObject;
    }

    private void OnEnable()
    {
        StartCoroutine(Continue());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Continue()
    {
        yield return new WaitForSeconds(20f);
        text.GetComponent<RectTransform>().position = new Vector3();
        text.GetComponent<Text>().text = "Press ";
    }
}
