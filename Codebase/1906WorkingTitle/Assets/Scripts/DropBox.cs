using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    public GameObject boxPiece = null, bullet = null;
    public GameObject spawner = null;
    
    void Update()
    {
        
        if (spawner != null && boxPiece != null)
        {
            if (spawner.GetComponent<SpawnScript>().spawnedEnemies.Count > 0)
            {
                boxPiece.SetActive(false);
                bullet.SetActive(false);
            }
            else
            {
                boxPiece.SetActive(true);
                bullet.SetActive(true);
            }
        }
    }
}
