using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    Button ResumeBTN, OptionsBTN, ExitBTN;
    GameObject mainUI, optionsMenu;
    // Start is called before the first frame update
    void Start()
    {
        mainUI = GameObject.Find("Main UI");
        mainUI.SetActive(false);
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
        {
            if ((go.name != "Shop UI(Clone)" && go.name != "Main UI(Clone)" && go.name != "Pause Menu(Clone)"))
                go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnEnable()
    {
        if (mainUI != null)
        {
            Time.timeScale = 0;
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
            {
                if ((go.name != "Shop UI(Clone)" && go.name != "Main UI(Clone)" && go.name != "Pause Menu(Clone)"))
                    go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
            }
            mainUI.SetActive(false);
        }

        optionsMenu = transform.Find("Options").gameObject;
        //if (optionsMenu != null)
            optionsMenu.SetActive(false);
        if(transform.Find("Pause").gameObject)
            transform.Find("Pause").gameObject.SetActive(true);
    }
    

    // Update is called once per frame
    void Update()
    {
        //exit pause menu and reenable main ui
        if (Input.GetKeyDown(KeyCode.Escape) && !transform.Find("Options").gameObject.activeSelf)
        {
            Resume();
        }
    }


    void Resume()
    {
        UnPause();
        //Instantiate(Resources.Load<GameObject>("Prefabs/Main UI"));
        mainUI.SetActive(true);
        //Destroy(gameObject);
    }

    void Options()
    {
        //SceneManager.LoadScene("Options");
        optionsMenu.SetActive(true);
    }

    void UnPause()
    {
        Time.timeScale = 1;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
        }
    }

    void ExitGame()
    {
        UnPause();
        SceneManager.LoadScene("Main Menu");
    }

}
