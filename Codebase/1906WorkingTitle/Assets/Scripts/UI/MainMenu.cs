using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region MainMenuProperties
    private Button startBTN, howToPlayBTN, optionsBTN, exitBTN = null;

    private GameObject howToPlay, options = null;
    #endregion

    //Start is called before the first frame update
    void Start()
    {
        howToPlay = GameObject.Find("How to playText");
        howToPlay.SetActive(false);

        options = GameObject.Find("Options");
        options.SetActive(false);

        //assign buttons
        startBTN = GameObject.Find("Start Game").GetComponent<Button>();
        howToPlayBTN = GameObject.Find("How to PlayBTN").GetComponent<Button>();
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
        if (howToPlay != null)
            howToPlay.SetActive(false);
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

    private void HowToPlay()
    {
        howToPlay.SetActive(true);
    }

    public void CloseHowTo()
    {
        if (howToPlay.activeSelf)
            howToPlay.SetActive(false);
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
