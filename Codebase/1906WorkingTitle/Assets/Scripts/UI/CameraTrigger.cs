using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    GameObject camPos = null;
    [SerializeField] public GameObject spawner = null;
    SpawnScript spawn = null;
    [SerializeField] bool hasVisited = false;
    [SerializeField] GameObject knight  = null;

    // Start is called before the first frame update
    void Start()
    {
        //Finds each camera position object in each room
        camPos = transform.Find("Camera Position").gameObject;
        spawn = spawner.GetComponent<SpawnScript>();
        spawner.SetActive(false);
        hasVisited = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Changes the cameras position
            Camera.main.transform.position = camPos.transform.position;
            if (!hasVisited)
            {
                spawner.SetActive(true);
                //Spawns enemies and locks rooms
                spawn.SetDoorLock(true);
                spawn.SetEnabled(true);
            }
            if (other.gameObject.GetComponent<Player>().enemyRespawn)
                spawn.ResetSpawner();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if(knight != null)
            {
                knight.SetActive(false);
            }
        }

    }

    public void DisableRoom()
    {
        hasVisited = true;
    }
}
