using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region MainMenuProperties
    private Button startBTN, creditsBTN, optionsBTN, exitBTN = null;

    private GameObject credits, options = null;
    #endregion

    //Start is called before the first frame update
    void Start()
    {
        credits = GameObject.Find("Credits");
        credits.SetActive(false);

        options = GameObject.Find("Options");
        options.SetActive(false);

        //assign buttons
        startBTN = GameObject.Find("Start Game").GetComponent<Button>();
        creditsBTN = GameObject.Find("CreditsBTN").GetComponent<Button>();
        optionsBTN = GameObject.Find("OptionsBTN").GetComponent<Button>();
        exitBTN = GameObject.Find("Exit Game").GetComponent<Button>();

        //add function listeners
        startBTN.onClick.AddListener(StartGame);
        creditsBTN.onClick.AddListener(Credits);
        optionsBTN.onClick.AddListener(OptionsMenu);
        exitBTN.onClick.AddListener(ExitGame);

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
            CloseHowTo();
    }

    #region MainMenuFunctions

    private void StartGame()
    {
        GameObject clone = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SceneLoader"));
        StartCoroutine(clone.GetComponent<SceneLoader>().LoadNewScene(1));
    }

    private void Credits()
    {
        credits.SetActive(true);
    }

    public void CloseHowTo()
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
