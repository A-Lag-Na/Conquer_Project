using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawning : MonoBehaviour
{
    [SerializeField] private SpawnScript forestBoss = null;
    [SerializeField] private SpawnScript mountainBoss = null;
    [SerializeField] private SpawnScript desertBoss = null;
    [SerializeField] private Camera mainCamera = null;

    private bool forestNotRespawned = false;
    private bool mountainsNotRespawned = false;
    private bool desertNotRespawned = false;
    private int forestSpawnedNumber = 0;
    private int mountainSpawnedNumber = 0;
    private int desertSpawnedNumber = 0;
    private Player player = null;

    private void Start()
    {
        forestNotRespawned = true;
        mountainsNotRespawned = true;
        desertNotRespawned = true;
        forestSpawnedNumber = 0;
        mountainSpawnedNumber = 0;
        desertSpawnedNumber = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
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

    private void ResetForest()
    {
        if (forestNotRespawned)
        {
            player.enemyRespawn = true;
            forestNotRespawned = false;
        }
    }

    private void ResetMountains()
    {
        if (mountainsNotRespawned)
        {
            player.enemyRespawn = true;
            mountainsNotRespawned = false;
        }
    }

    private void ResetDesert()
    {
        if (desertNotRespawned)
        {
            player.enemyRespawn = true;
            desertNotRespawned = false;
        }
    }
}
