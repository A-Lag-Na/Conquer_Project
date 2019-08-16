using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    public GameObject boxPiece = null;
    public GameObject spawner = null;
    
    void Update()
    {
        
        if (spawner != null && boxPiece != null)
        {
            if (spawner.GetComponent<SpawnScript>().spawnedEnemies.Count > 0)
                boxPiece.SetActive(false);
            else
                boxPiece.SetActive(true);
        }
    }
}
