using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    #region OptionsProperties
    private Button loadSave = null;
    private GameObject headUI = null;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (transform.Find("LoadSave"))
        {
            loadSave = transform.Find("LoadSave").GetComponent<Button>();
            loadSave.onClick.AddListener(LoadSave);
        }

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseCurrentScreen();
    }

    #region OptionsFunctions
    void LoadSave()
    {
        if (loadSave != null)
        {
            if(headUI.CompareTag("PauseMenu"))
                GameObject.FindGameObjectWithTag("Player").GetComponent<SaveScript>().Load();
        }
    }

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
