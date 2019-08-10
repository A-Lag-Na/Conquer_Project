using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    Button playAgain, mainMenu;

    private void Start()
    {
        playAgain = transform.Find("Play Again").GetComponent<Button>();
        mainMenu = transform.Find("Main Menu").GetComponent<Button>();

        playAgain.onClick.AddListener(PlayAgain);
        mainMenu.onClick.AddListener(MainMenu);

        Time.timeScale = 0;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
            go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
    }

    void PlayAgain()
    {
        UnPause();
        SceneManager.LoadScene("Build Scene");
    }

    void MainMenu()
    {
        UnPause();
        SceneManager.LoadScene("Main Menu");
    }

    void UnPause()
    {
        Time.timeScale = 1;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
            go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
    }
}
