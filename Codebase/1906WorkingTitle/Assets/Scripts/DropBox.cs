using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    //This script is for the box piece drop after finishing the boss room
    public GameObject boxPiece = null;
    public GameObject spawner = null;

    // Update is called once per frame
    void Update()
    {
        if (boxPiece != null)
        {
            if (spawner.GetComponent<SpawnScript>().spawnedEnemies.Count > 0)
                boxPiece.SetActive(false);
            else
                boxPiece.SetActive(true);
        }
    }
}
