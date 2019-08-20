using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTriggerScript : MonoBehaviour
{
    [SerializeField] private BaseNPC baseNPC;
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private Player playerScript;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private string dialogueName;
    private bool debouncer = true;

    private void OnTriggerEnter(Collider other)
    {
        //Player will be stopped and NPC Knight will walk towards him
        if (other.tag == "Player" && debouncer)
        {        
            debouncer = false;
            dialogueManager.dialogue = baseNPC.GetDialogue(dialogueName);
            baseNPC.DoAction();
            dialogueCanvas.gameObject.SetActive(true);
            playerScript.isStunned = true;
            dialogueManager.DisplayText();
            debouncer = true;
        }
    }

    public void OnDialogueEnd()
    {
        playerScript.isStunned = false;
        baseNPC.OnDialogueEnd();
    }
}