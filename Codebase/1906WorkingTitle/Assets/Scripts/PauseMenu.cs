﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    Button ResumeBTN, OptionsBTN, ExitBTN;
    GameObject mainUI;
    // Start is called before the first frame update
    void Start()
    {
        ResumeBTN = GameObject.Find("Resume").GetComponent<Button>();
        OptionsBTN = GameObject.Find("Options").GetComponent<Button>();
        ExitBTN = GameObject.Find("Exit Game").GetComponent<Button>();

        ResumeBTN.onClick.AddListener(Resume);
        OptionsBTN.onClick.AddListener(Options);
        ExitBTN.onClick.AddListener(ExitGame);
        
        Time.timeScale = 0;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //exit pause menu and reenable main ui
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }


    void Resume()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Main UI"));
        Time.timeScale = 1;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
        }
        Destroy(gameObject);    
    }

    void Options()
    {

    }

    void ExitGame()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
