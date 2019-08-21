using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    [SerializeField] private GameObject boxPiece = null, bullet = null;
    [SerializeField] private GameObject dialogueTrigger = null;
    [SerializeField] private GameObject spawner = null;

    private bool buffer = true;

    private void Update()
    {
        if (buffer)
        {
            if (spawner.GetComponent<SpawnScript>().GetPointsRemaining() <= 0 && spawner.activeSelf)
            {
                if (spawner.GetComponent<SpawnScript>().spawnedEnemies.Count <= 0 && spawner.activeSelf)
                {
                    DropLoot();
                    buffer = false;
                }
            }


        }
    }
    void DropLoot()
    {
        boxPiece.SetActive(true);
        bullet.SetActive(true);
        dialogueTrigger.SetActive(true);
    }
}
