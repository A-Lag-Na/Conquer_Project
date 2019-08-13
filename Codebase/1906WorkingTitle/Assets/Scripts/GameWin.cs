using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{
    #region GameWinProperties
    Text text = null;
    float time, delay = 0.0f;
    int minutes, seconds;
    Text title, content, btnTxt1, btnTxt2 = null;
    Image fadeIn, btnBack1, btnBack2 = null;
    Button playAgain, mainMenu = null;
    Color white, red, black = Color.clear;
    #endregion

    void Start()
    {
        time = Time.realtimeSinceStartup;
        delay = 0.02f;
        white = new Color(1f, 1f, 1f, 1f);
        red = new Color(1f, 0f, 0f, 1f);
        black = new Color(0f, 0f, 0f, 1f);

        #region Grabs
        text = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        fadeIn = transform.GetChild(0).GetComponent<Image>();
        content = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        title = transform.GetChild(0).GetChild(1).GetComponent<Text>();
        btnTxt1 = transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>();
        btnBack1 = transform.GetChild(0).GetChild(2).GetComponent<Image>();
        btnTxt2 = transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>();
        btnBack2 = transform.GetChild(0).GetChild(3).GetComponent<Image>();

        playAgain = transform.Find("Play Again").GetComponent<Button>();
        mainMenu = transform.Find("Main Menu").GetComponent<Button>();

        playAgain.onClick.AddListener(PlayAgain);
        mainMenu.onClick.AddListener(MainMenu);
        #endregion

        Time.timeScale = 0;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
            go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn.color != black)
            fadeIn.color = Color.Lerp(fadeIn.color, black, delay);
        if (content.color != white)
            content.color = Color.Lerp(content.color, white, delay);
        if (btnBack1.color != white)
            btnBack1.color = Color.Lerp(btnBack1.color, white, delay);
        if (btnBack2.color != white)
            btnBack2.color = Color.Lerp(btnBack2.color, white, delay);
        if (btnTxt1.color != black)
            btnTxt1.color = Color.Lerp(btnTxt1.color, black, delay);
        if (btnTxt2.color != black)
            btnTxt2.color = Color.Lerp(btnTxt2.color, black, delay);
        if (title.color != red)
            title.color = Color.Lerp(title.color, red, delay);
        text.text = $"It took you {(int)time / 60} minutes and {(int)time % 60} seconds!";
    }

    void PlayAgain()
    {
        UnPause();
        GameObject clone = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SceneLoader"));
        StartCoroutine(clone.GetComponent<SceneLoader>().LoadNewScene(2));
    }

    void MainMenu()
    {
        UnPause();
        GameObject clone = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SceneLoader"));
        StartCoroutine(clone.GetComponent<SceneLoader>().LoadNewScene(0));
    }

    void UnPause()
    {
        Time.timeScale = 1;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
            go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
    }
}
