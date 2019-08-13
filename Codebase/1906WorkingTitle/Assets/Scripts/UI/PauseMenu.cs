using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region PauseMenuProperties
    Button ResumeBTN, OptionsBTN, ExitBTN = null;
    GameObject mainUI, optionsMenu = null;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Main UI"))
        {
            mainUI = GameObject.Find("Main UI");
            mainUI.SetActive(false);
        }

        optionsMenu = transform.Find("Options").gameObject;
        optionsMenu.SetActive(false);

        ResumeBTN = transform.Find("Pause").Find("Resume").gameObject.GetComponent<Button>();
        OptionsBTN = transform.Find("Pause").Find("OptionsBTN").gameObject.GetComponent<Button>();
        ExitBTN = transform.Find("Pause").Find("Exit Game").gameObject.GetComponent<Button>();

        ResumeBTN.onClick.AddListener(Resume);
        OptionsBTN.onClick.AddListener(Options);
        ExitBTN.onClick.AddListener(ExitGame);

        Time.timeScale = 0;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
            if ((go.name != "Shop UI" && go.name != "Main UI" && go.name != "Pause Menu"))
                go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
    }

    // Update is called once per frame
    void Update()
    {
        //exit pause menu and reenable main ui
        if (Input.GetKeyDown(KeyCode.Escape) && !transform.Find("Options").gameObject.activeSelf)
            Resume();
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

        optionsMenu = transform.Find("Options").gameObject;
        optionsMenu.SetActive(false);
        if (transform.Find("Pause").gameObject)
            transform.Find("Pause").gameObject.SetActive(true);
    }

    #region PauseMenuFunctions
    void Resume()
    {
        UnPause();
        if (mainUI != null)
            mainUI.SetActive(true);
    }

    void Options()
    {
        optionsMenu.SetActive(true);
    }

    void UnPause()
    {
        Time.timeScale = 1;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
            go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
    }

    void ExitGame()
    {
        UnPause();
        SceneManager.LoadScene("Main Menu");
    }
    #endregion
}
