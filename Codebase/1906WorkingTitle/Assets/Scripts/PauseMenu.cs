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
        ResumeBTN = GameObject.Find("Resume").GetComponent<Button>();
        OptionsBTN = GameObject.Find("Options").GetComponent<Button>();
        ExitBTN = GameObject.Find("Exit Game").GetComponent<Button>();

        ResumeBTN.onClick.AddListener(Resume);
        OptionsBTN.onClick.AddListener(Options);
        ExitBTN.onClick.AddListener(ExitGame);
        
        //grab main ui if active and existing
        if (GameObject.Find("Main UI"))
        {
            mainUI = GameObject.Find("Main UI");
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
        if (mainUI != null)
            mainUI.SetActive(true);
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
