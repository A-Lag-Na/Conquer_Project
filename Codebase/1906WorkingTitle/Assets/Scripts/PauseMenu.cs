using System.Collections;
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
        mainUI = GameObject.Find("Main UI");
        mainUI.SetActive(false);
        ResumeBTN = GameObject.Find("Resume").GetComponent<Button>();
        OptionsBTN = GameObject.Find("Options").GetComponent<Button>();
        ExitBTN = GameObject.Find("Exit Game").GetComponent<Button>();

        ResumeBTN.onClick.AddListener(Resume);
        OptionsBTN.onClick.AddListener(Options);
        ExitBTN.onClick.AddListener(ExitGame);
        
        
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
        UnPause();
        //Instantiate(Resources.Load<GameObject>("Prefabs/Main UI"));
        mainUI.SetActive(true);
        //Destroy(gameObject);
    }

    void Options()
    {
        SceneManager.LoadScene("Options");
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
