using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    #region OptionsProperties
    private GameObject headUI = null;
    #endregion
    
    void Start()
    {
        if (GameObject.Find("Pause"))
            headUI = GameObject.Find("Pause");
        else
            headUI = GameObject.Find("Main Menu");
        if(headUI != null)
            headUI.SetActive(false);
    }

    private void OnEnable()
    {
        if (headUI != null)
            headUI.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseCurrentScreen();
    }

    #region OptionsFunctions

    public void CloseCurrentScreen()
    {
        if (headUI != null)
        {
            headUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    #endregion
}
