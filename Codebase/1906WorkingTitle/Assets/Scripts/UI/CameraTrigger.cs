﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    GameObject camPos = null;
    [SerializeField] public GameObject spawner = null;
    SpawnScript spawn = null;
    bool enabled = true;

    // Start is called before the first frame update
    void Start()
    {
        //Finds each camera position object in each room
        camPos = transform.Find("Camera Position").gameObject;
        spawn = spawner.GetComponent<SpawnScript>();
        spawner.SetActive(false);
        enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Changes the cameras position
            Camera.main.transform.position = camPos.transform.position;
            if (enabled)
            {
                spawner.SetActive(true);
                //Spawns enemies and locks rooms
                spawn.SetDoorLock(true);
                spawn.SetEnabled(true);
            }
        }
    }

    public void DisableRoom()
    {
        enabled = false;
    }
}
