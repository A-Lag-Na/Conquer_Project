using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    GameObject camPos;
    [SerializeField] public GameObject spawner;
   
    // Start is called before the first frame update
    void Start()
    {
        //Finds each camera position object in each room
        camPos = transform.Find("Camera Position").gameObject;
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            //Changes the cameras position
            Camera.main.transform.position = camPos.transform.position;

            //Spawns enemies and locks rooms
            spawner.GetComponent<SpawnScript>().SetDoorLock(true);
            spawner.GetComponent<SpawnScript>().SetEnabled(true);
        }
        
    }
}
