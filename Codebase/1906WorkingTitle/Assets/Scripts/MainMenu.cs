using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    private Button start, howToPlay, options, exit;
    void Start()
    {
        //assign buttons
        start = GameObject.Find("Start Game").GetComponent<Button>();
        howToPlay = GameObject.Find("How to Play").GetComponent<Button>();
        options = GameObject.Find("Options").GetComponent<Button>();
        exit = GameObject.Find("Exit Game").GetComponent<Button>();

        //add function listeners
        start.onClick.AddListener(StartGame);
        howToPlay.onClick.AddListener(HowToPlay);
        options.onClick.AddListener(OptionsMenu);
        exit.onClick.AddListener(ExitGame);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameObject.Find("How to play(Clone)"))
                Destroy(GameObject.Find("How to play(Clone)"));
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Alegna's Sandbox");
    }
    private void HowToPlay()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/How to play"));
    }
    private void OptionsMenu()
    {
        SceneManager.LoadScene("Options");
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
