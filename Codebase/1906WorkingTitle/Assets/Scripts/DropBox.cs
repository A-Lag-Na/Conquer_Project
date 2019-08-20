using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    [SerializeField] private GameObject boxPiece = null, bullet = null;
    [SerializeField] private GameObject spawner = null;
    [SerializeField] private GameObject dialogueTrigger = null;
 
    void DropLoot()
    {
        boxPiece.SetActive(true);
        bullet.SetActive(true);
        dialogueTrigger.SetActive(true);
    }
}
