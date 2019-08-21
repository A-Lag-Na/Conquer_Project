﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region PauseMenuProperties
    private Button ResumeBTN, OptionsBTN, ExitBTN = null;
    private GameObject mainUI, optionsMenu = null;
    #endregion
    
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
            if (go.name != "Pause Menu")
                go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
        if (GameObject.FindGameObjectWithTag("MainCamera"))
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<StopWatch>().PauseStopWatch();
    }
    
    void Update()
    {
        //exit pause menu and reenable main ui
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && !transform.Find("Options").gameObject.activeSelf)
            Resume();
    }

    private void OnEnable()
    {
        if (mainUI != null)
        {
            Time.timeScale = 0;
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
                if (go.name != "Pause Menu")
                    go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
            if (GameObject.FindGameObjectWithTag("MainCamera"))
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<StopWatch>().PauseStopWatch();
            mainUI.SetActive(false);
        }

        optionsMenu = transform.Find("Options").gameObject;
        optionsMenu.SetActive(false);
        if (transform.Find("Pause").gameObject)
            transform.Find("Pause").gameObject.SetActive(true);
    }

    #region PauseMenuFunctions
    private void Resume()
    {
        UnPause();
        if (mainUI != null)
            mainUI.SetActive(true);
        if (mainUI != null)
            mainUI.GetComponent<UpdateUI>().ResumeGame();
    }

    private void Options()
    {
        optionsMenu.SetActive(true);
    }

    private void UnPause()
    {
        Time.timeScale = 1;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
            go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
        if (GameObject.FindGameObjectWithTag("MainCamera"))
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<StopWatch>().ResumeStopWatch();
    }

    private void ExitGame()
    {
        UnPause();
        SceneManager.LoadScene("Main Menu");
    }
    #endregion
}