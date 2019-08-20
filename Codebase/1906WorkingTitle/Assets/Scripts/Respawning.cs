using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawning : MonoBehaviour
{
    [SerializeField] SpawnScript forestBoss = null;
    [SerializeField] SpawnScript mountainBoss = null;
    [SerializeField] SpawnScript desertBoss = null;
    bool forestNotRespawned = false;
    bool mountainsNotRespawned = false;
    bool desertNotRespawned = false;
    int forestSpawnedNumber = 0;
    int mountainSpawnedNumber = 0;
    int desertSpawnedNumber = 0;
    Player player = null;
    [SerializeField] Camera mainCamera = null;

    // Start is called before the first frame update
    void Start()
    {
        forestNotRespawned = true;
        mountainsNotRespawned = true;
        desertNotRespawned = true;
        forestSpawnedNumber = 0;
        mountainSpawnedNumber = 0;
        desertSpawnedNumber = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (forestBoss.GetNumEnemies() > 0)
            forestSpawnedNumber = forestBoss.GetNumEnemies();
        if (forestSpawnedNumber > forestBoss.GetNumEnemies())
            ResetForest();
        if (mountainBoss.GetNumEnemies() > 0)
            mountainSpawnedNumber = mountainBoss.GetNumEnemies();
        if (mountainSpawnedNumber > mountainBoss.GetNumEnemies())
            ResetMountains();
        if (desertBoss.GetNumEnemies() > 0)
            desertSpawnedNumber = desertBoss.GetNumEnemies();
        if (desertSpawnedNumber > desertBoss.GetNumEnemies())
            ResetDesert();
        if (mainCamera.transform.position.x == -0.3599968f && mainCamera.transform.position.y == 0 && mainCamera.transform.position.z == -17)
            player.enemyRespawn = false;
        else if (mainCamera.transform.position.x == 79.64001f && mainCamera.transform.position.y == 0 && mainCamera.transform.position.z == -55)
            player.enemyRespawn = false;
        else if (mainCamera.transform.position.x == -80.36f && mainCamera.transform.position.y == 0 && mainCamera.transform.position.z == -55)
            player.enemyRespawn = false;
    }

    void ResetForest()
    {
        if (forestNotRespawned)
        {
            player.enemyRespawn = true;
            forestNotRespawned = false;
        }
    }

    void ResetMountains()
    {
        if (mountainsNotRespawned)
        {
            player.enemyRespawn = true;
            mountainsNotRespawned = false;
        }
    }

    void ResetDesert()
    {
        if (desertNotRespawned)
        {
            player.enemyRespawn = true;
            desertNotRespawned = false;
        }
    }
}
