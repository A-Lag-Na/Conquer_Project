using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTriggerScript : MonoBehaviour
{
    [SerializeField] private BaseNPC baseNPC = null;
    [SerializeField] private Canvas dialogueCanvas = null;
    [SerializeField] private Player playerScript = null;
    [SerializeField] private DialogueManager dialogueManager = null;
    [SerializeField] private string dialogueName = null;
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