using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    private Button start, options, exit;
    void Start()
    {
        //assign buttons
        start = GameObject.Find("Start Game").GetComponent<Button>();
        options = GameObject.Find("Options").GetComponent<Button>();
        exit = GameObject.Find("Exit Game").GetComponent<Button>();

        //add function listeners
        start.onClick.AddListener(StartGame);
        options.onClick.AddListener(OptionsMenu);
        exit.onClick.AddListener(ExitGame);
        
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Alegna's Sandbox");
    }
    private void OptionsMenu()
    {
        Debug.Log("Open Options");
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
