using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseNPC : MonoBehaviour
{
    public Dialogue[] dialogues;
    public DialogueManager dialogueManager;

    public virtual Dialogue GetDialogue()
    {
        return dialogues[0];
    }

    public virtual Dialogue GetDialogue(string dialogue)
    {
        return dialogues[0];
    }

    public abstract void DoAction();

    public virtual void OnDialogueEnd() { }
    
}
