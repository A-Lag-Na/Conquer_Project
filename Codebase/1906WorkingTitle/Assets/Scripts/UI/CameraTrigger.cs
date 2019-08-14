using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    GameObject camPos = null;
    [SerializeField] public GameObject spawner = null;
    SpawnScript spawn = null;

    // Start is called before the first frame update
    void Start()
    {
        //Finds each camera position object in each room
        camPos = transform.Find("Camera Position").gameObject;
        spawn = spawner.GetComponent<SpawnScript>();
        spawner.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Changes the cameras position
            Camera.main.transform.position = camPos.transform.position;
            spawner.SetActive(true);
            //Spawns enemies and locks rooms
            spawn.SetDoorLock(true);
            spawn.SetEnabled(true);
        }
    }
}
