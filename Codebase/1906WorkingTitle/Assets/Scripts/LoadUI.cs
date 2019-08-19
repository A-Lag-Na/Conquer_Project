using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadUI : MonoBehaviour
{
    Button loadOne = null;
    Button loadTwo = null;
    Button loadThree = null;
    SaveScript saveScript = null;
    Text loadOneText = null;
    Text loadTwoText = null;
    Text loadThreeText = null;

    // Start is called before the first frame update
    void Start()
    {
        loadOne = GameObject.Find("Load File 1").GetComponent<Button>();
        loadTwo = GameObject.Find("Load File 2").GetComponent<Button>();
        loadThree = GameObject.Find("Load File 3").GetComponent<Button>();
        saveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SaveScript>();
        loadOne.onClick.AddListener(SelectOne);
        loadTwo.onClick.AddListener(SelectTwo);
        loadThree.onClick.AddListener(SelectThree);
        loadOneText = loadOne.gameObject.GetComponentInChildren<Text>();
        loadTwoText = loadTwo.gameObject.GetComponentInChildren<Text>();
        loadThreeText = loadThree.gameObject.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey($"Level{1}"))
            loadOneText.text = $"Load 1\nLevel: {PlayerPrefs.GetInt($"Level{1}")}";
        if (PlayerPrefs.HasKey($"Level{2}"))
            loadTwoText.text = $"Load 2\nLevel: {PlayerPrefs.GetInt($"Level{2}")}";
        if (PlayerPrefs.HasKey($"Level{3}"))
            loadThreeText.text = $"Load 3\nLevel: {PlayerPrefs.GetInt($"Level{3}")}";
    }

    public void SelectOne()
    {
        saveScript.SetSaveSlot(1);
        saveScript.Load();
    }

    public void SelectTwo()
    {
        saveScript.SetSaveSlot(2);
        saveScript.Load();
    }

    public void SelectThree()
    {
        saveScript.SetSaveSlot(3);
        saveScript.Load();
    }
}
