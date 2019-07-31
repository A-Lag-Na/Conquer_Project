using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    private Button startBTN, howToPlayBTN, optionsBTN, exitBTN;

    private GameObject howToPlay, options;

    void Start()
    {
        howToPlay = GameObject.Find("How to play");
        howToPlay.SetActive(false);

        options = GameObject.Find("Options");
        options.SetActive(false);

        //assign buttons
        startBTN = GameObject.Find("Start Game").GetComponent<Button>();
        howToPlayBTN = GameObject.Find("How to Play").GetComponent<Button>();
        optionsBTN = GameObject.Find("OptionsBTN").GetComponent<Button>();
        exitBTN = GameObject.Find("Exit Game").GetComponent<Button>();

        //add function listeners
        startBTN.onClick.AddListener(StartGame);
        howToPlayBTN.onClick.AddListener(HowToPlay);
        optionsBTN.onClick.AddListener(OptionsMenu);
        exitBTN.onClick.AddListener(ExitGame);
        
    }

    private void OnEnable()
    {
        if(howToPlay!=null)
            howToPlay.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(howToPlay.activeSelf)
                howToPlay.SetActive(false);
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Alegna's Sandbox");
    }
    private void HowToPlay()
    {
        howToPlay.SetActive(true);
    }
    private void OptionsMenu()
    {
        //SceneManager.LoadScene("Options");
        options.SetActive(true);
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
