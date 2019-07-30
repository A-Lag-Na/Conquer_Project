using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{

    private Button Controls;
    private GameObject currentScreen;

    // Start is called before the first frame update
    void Start()
    {
        Controls = GameObject.Find("Controls").GetComponent<Button>();
        Controls.onClick.AddListener(OpenControls);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCurrentScreen();
        }
    }

    void OpenControls()
    {
        currentScreen = Instantiate(Resources.Load<GameObject>("Prefabs/Controls"));
    }

    void CloseCurrentScreen()
    {
        if(currentScreen != null)
        {
            Destroy(currentScreen);
        }
        else
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
