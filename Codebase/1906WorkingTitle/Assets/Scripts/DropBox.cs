using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    [SerializeField] GameObject boxPiece = null, bullet = null;
    [SerializeField] GameObject dialogueTrigger;
 
    void DropLoot()
    {
        boxPiece.SetActive(true);
        bullet.SetActive(true);
        dialogueTrigger.SetActive(true);
    }
}
