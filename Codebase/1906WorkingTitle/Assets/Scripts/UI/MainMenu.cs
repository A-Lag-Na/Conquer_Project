using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region MainMenuProperties
    private Button startBTN, loadBTN, tutorialBTN, creditsBTN, optionsBTN, exitBTN = null;
    private GameObject credits, options = null;
    #endregion
    
    void Start()
    {

        //assign buttons
        startBTN = transform.Find("Start Game").GetComponent<Button>();
        loadBTN = transform.Find("Load Game").GetComponent<Button>();
        tutorialBTN = transform.Find("Tutorial").GetComponent<Button>();
        creditsBTN = transform.Find("CreditsBTN").GetComponent<Button>();
        optionsBTN = transform.Find("OptionsBTN").GetComponent<Button>();
        exitBTN = transform.Find("Exit Game").GetComponent<Button>();

        //add function listeners
        startBTN.onClick.AddListener(StartGame);
        loadBTN.onClick.AddListener(LoadGame);
        tutorialBTN.onClick.AddListener(Tutorial);
        creditsBTN.onClick.AddListener(Credits);
        optionsBTN.onClick.AddListener(OptionsMenu);
        exitBTN.onClick.AddListener(ExitGame);

        credits = GameObject.Find("Credits");
        credits.SetActive(false);

        options = GameObject.Find("Options");
        options.SetActive(false);
    }

    private void OnEnable()
    {
        if (credits != null)
            credits.SetActive(false);
        if (options != null)
            options.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseCredits();
    }

    #region MainMenuFunctions

    private void StartGame()
    {
        GameObject clone = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SceneLoader"));
        StartCoroutine(clone.GetComponent<SceneLoader>().LoadNewScene(2));
    }

    private void LoadGame()
    {
        GameObject clone = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SceneLoader"));
        StartCoroutine(clone.GetComponent<SceneLoader>().LoadSceneandSettings(2));
    }

    private void Tutorial()
    {
        GameObject clone = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SceneLoader"));
        StartCoroutine(clone.GetComponent<SceneLoader>().LoadNewScene(1));
    }

    private void Credits()
    {
        credits.SetActive(true);
    }

    public void CloseCredits()
    {
        if (credits.activeSelf)
            credits.SetActive(false);
    }

    private void OptionsMenu()
    {
        options.SetActive(true);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
    #endregion
}
