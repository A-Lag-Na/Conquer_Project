using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Dialogue dialogue = null;
    [SerializeField] private Text text = null;
    [SerializeField] private Canvas canvas = null;
    [SerializeField] private Text continuePrompt = null;
    public DialogueTriggerScript dialogueTriggerScript = null;

    private bool enter = false;
    private bool exit = false;
    private int textIndex = 0;

    private void Update()
    {
        //If the player can press enter "Press enter to continue" The continue prompt text will also be turned on
        if (enter)
        {
            continuePrompt.gameObject.SetActive(true);
            if (exit)
            {
                TextConditionsSingle();
            }
            else
                TextConditionsChain();
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
            StartCoroutine(TextWaitChain());
        }
    }

    public void DisplayText(string t)
    {
        text.text = t;
        //Wait time for "Press Enter to continue" to pop up
        StartCoroutine(TextWaitSingle());
    }

    //If Enter pressed the index will go up one and enter value will be restored to false
    private void TextConditionsChain()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            enter = false;
            textIndex++;
            //Wait time for "Press Enter to continue" to pop up
            DisplayText();
        }
    }
    private void TextConditionsSingle()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            enter = false;
            EndSequence();
            exit = false;
        }
    }

    //Wait for player to read if in chain mode
    IEnumerator TextWaitChain()
    {   
        continuePrompt.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        enter = true;
    }

    IEnumerator TextWaitSingle()
    {
        continuePrompt.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        enter = true;
        exit = true;
    }
    //When all text has gone through end the sequence
    private void EndSequence()
    {
        canvas.gameObject.SetActive(false);
        StopAllCoroutines();
        dialogueTriggerScript.OnDialogueEnd();
    }
}