using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    //This script is for the box piece drop after finishing the boss room
    public GameObject boxPiece;
    public GameObject spawner;

  
    // Update is called once per frame
    void Update()
    {
        if (spawner.GetComponent<SpawnScript>().spawnedEnemies.Count == 0)
        {
            boxPiece.SetActive(true);
        }
    }
}
