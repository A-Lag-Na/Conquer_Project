using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    private Button ControlsBTN;
    private GameObject currentScreen;
    private GameObject controls, headUI;

    // Start is called before the first frame update
    void Start()
    {
        ControlsBTN = GameObject.Find("ControlsBTN").GetComponent<Button>();
        ControlsBTN.onClick.AddListener(OpenControls);
        controls = transform.Find("Controls").gameObject;
        controls.SetActive(false);


        if (GameObject.Find("Pause"))
            headUI = GameObject.Find("Pause");
        else
            headUI = GameObject.Find("Main Menu");
        headUI.SetActive(false);
    }

    private void OnEnable()
    {
        if (headUI != null)
            headUI.SetActive(false);
        controls = transform.Find("Controls").gameObject;
        controls.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseCurrentScreen();
    }

    void OpenControls()
    {
        currentScreen = controls;
        controls.SetActive(true);
    }

    public void CloseCurrentScreen()
    {
        if (currentScreen != null)
        {
            currentScreen.SetActive(false);
            currentScreen = null;
        }
        else
        {
            headUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
