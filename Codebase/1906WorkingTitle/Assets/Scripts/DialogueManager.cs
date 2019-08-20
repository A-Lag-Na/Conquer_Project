using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Dialogue dialogue;
    [SerializeField] private Text text;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Text continuePrompt;
    [SerializeField] private DialogueTriggerScript dialogueTriggerScript;

    private bool enter = false;
    private int textIndex = 0;

    private void Update()
    {
        //If the player can press enter "Press enter to continue" The continue prompt text will also be turned on
        if (enter)
        {
            continuePrompt.gameObject.SetActive(true);
            TextConditions();
        }
    }

    public void DisplayText()
    {
        if (textIndex >= dialogue.textArray.Length)
        {
            textIndex = 0;
            EndSequence();
        }
        else
        {
            text.text = dialogue.textArray[textIndex];
            //Wait time for "Press Enter to continue" to pop up
            StartCoroutine(TextWait());
        }
    }

    //If Enter pressed the index will go up one and enter value will be restored to false
    private void TextConditions()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            enter = false;
            textIndex++;
            //Wait time for "Press Enter to continue" to pop up
            DisplayText();
        }
    }

    //Wait for player to read
    IEnumerator TextWait()
    {
        continuePrompt.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        enter = true;
    }

    //When all text has gone through end the sequence
    private void EndSequence()
    {
        canvas.gameObject.SetActive(false);
        StopAllCoroutines();
        dialogueTriggerScript.OnDialogueEnd();
    }
}