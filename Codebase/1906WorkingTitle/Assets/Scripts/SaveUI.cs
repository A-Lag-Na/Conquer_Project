using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveUI : MonoBehaviour
{
    Button saveOne = null;
    Button saveTwo = null;
    Button saveThree = null;
    SaveScript saveScript = null;
    Text saveOneText = null;
    Text saveTwoText = null;
    Text saveThreeText = null;

    // Start is called before the first frame update
    void Start()
    {
        saveOne = GameObject.Find("Save File 1").GetComponent<Button>();
        saveTwo = GameObject.Find("Save File 2").GetComponent<Button>();
        saveThree = GameObject.Find("Save File 3").GetComponent<Button>();
        saveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SaveScript>();
        saveOne.onClick.AddListener(SelectOne);
        saveTwo.onClick.AddListener(SelectTwo);
        saveThree.onClick.AddListener(SelectThree);
        saveOneText = saveOne.gameObject.GetComponentInChildren<Text>();
        saveTwoText = saveTwo.gameObject.GetComponentInChildren<Text>();
        saveThreeText = saveThree.gameObject.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey($"Level{1}"))
            saveOneText.text = $"Save 1\nLevel: {PlayerPrefs.GetInt($"Level{1}")}";
        if (PlayerPrefs.HasKey($"Level{2}"))
            saveTwoText.text = $"Save 2\nLevel: {PlayerPrefs.GetInt($"Level{2}")}";
        if (PlayerPrefs.HasKey($"Level{3}"))
            saveThreeText.text = $"Save 3\nLevel: {PlayerPrefs.GetInt($"Level{3}")}";
    }

    public void SelectOne()
    {
        saveScript.SetSaveSlot(1);
        saveScript.Save();
        if (PlayerPrefs.HasKey($"Level{saveScript.GetSaveSlot()}"))
            saveOneText.text = $"Save 1\nLevel: {PlayerPrefs.GetInt($"Level{saveScript.GetSaveSlot()}")}";
    }

    public void SelectTwo()
    {
        saveScript.SetSaveSlot(2);
        saveScript.Save();
        if (PlayerPrefs.HasKey($"Level{saveScript.GetSaveSlot()}"))
            saveTwoText.text = $"Save 2\nLevel: {PlayerPrefs.GetInt($"Level{saveScript.GetSaveSlot()}")}";
    }

    public void SelectThree()
    {
        saveScript.SetSaveSlot(3);
        saveScript.Save();
        if (PlayerPrefs.HasKey($"Level{saveScript.GetSaveSlot()}"))
            saveThreeText.text = $"Save 3\nLevel: {PlayerPrefs.GetInt($"Level{saveScript.GetSaveSlot()}")}";
    }
}
