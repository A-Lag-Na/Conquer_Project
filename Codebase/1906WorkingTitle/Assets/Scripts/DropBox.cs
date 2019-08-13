using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    public GameObject boxPiece;
    public GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawner.GetComponent<SpawnScript>().spawnedEnemies.Count == 0)
        {
            boxPiece.SetActive(true);
        }
    }
}
