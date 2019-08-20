using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject loadUI = null;
    private Button playAgain, mainMenu = null, continueFromLastSave;

    private void Start()
    {
        playAgain = transform.Find("Play Again").GetComponent<Button>();
        mainMenu = transform.Find("Main Menu").GetComponent<Button>();
        continueFromLastSave = transform.Find("Continue from last save").GetComponent<Button>();

        playAgain.onClick.AddListener(PlayAgain);
        mainMenu.onClick.AddListener(MainMenu);
        continueFromLastSave.onClick.AddListener(Continue);

        Time.timeScale = 0;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
            go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
    }

    private void PlayAgain()
    {
        UnPause();
        SceneManager.LoadScene("Build Scene");
    }

    private void MainMenu()
    {
        UnPause();
        SceneManager.LoadScene("Main Menu");
    }

    private void Continue()
    {
        UnPause();
        Object[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject));
        foreach (GameObject go in objects)
            if (go.CompareTag("Player") && !go.activeSelf)
            {
                go.SetActive(true);
                go.GetComponent<Animator>().SetBool("Death", false);
            }
        gameObject.SetActive(false);
        loadUI.SetActive(true);
    }

    void UnPause()
    {
        Time.timeScale = 1;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
            go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
        if (GameObject.FindGameObjectWithTag("MainCamera"))
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<StopWatch>().ResumeStopWatch();
    }
}
