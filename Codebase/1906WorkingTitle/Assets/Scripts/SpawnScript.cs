﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    #region SpawnerStats
    //Whether the spawner is spawning or not.
    [SerializeField] private bool spawnEnabled = true;
    [SerializeField] private bool multiSpawnpoint = false;

    [SerializeField] List<GameObject> spawnpoints;

    //List of different enemies the spawner can choose to spawn.
    [SerializeField] private List<GameObject> enemies = null;
    private List<EnemyStats> enemiesClone;

    //list of doors.
    [SerializeField] private List<GameObject> doors = null;

    //How many points worth of enemies the spawner can spawn
    [SerializeField] private int points = 0;
    private int pointsClone;

    //Number of seconds between spawn.
    public float timer = 3;

    private bool spawnAgain = true;

    //Tracks number of externally spawned enemies
    public int remainingChildren;

    //List of enemies spawned by the spawner.
    public List<GameObject> spawnedEnemies;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        pointsClone = points;
        enemiesClone = new List<EnemyStats>();
        for (int i = 0; i < enemies.Count; i++)
            enemiesClone.Add(enemies[i].GetComponent<EnemyStats>());
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnEnabled && spawnAgain)
            StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        spawnAgain = false;
        RefreshSpawnedEnemies();

        if (remainingChildren <= 0 && spawnedEnemies.Count == 0 && pointsClone < 1)
            SetDoorLock(false);

        if (pointsClone > 0)
        {
            for (int i = enemiesClone.Count - 1; i >= 0; i--)
                if (enemiesClone[i].GetPoints() > pointsClone)
                    enemiesClone.Remove(enemiesClone[i]);
            
            int randomNum = Random.Range(0, enemiesClone.Count);
            GameObject enemyClone = null;

            if (!multiSpawnpoint)
            {
                //Spawns an enemy at the spawner's position
                enemyClone = Instantiate(enemiesClone[randomNum].gameObject, transform.position, Quaternion.identity);
            }
            else
            {
                //choose a random spawnpoint
                int randomNum2 = Random.Range(0, spawnpoints.Count);
                Vector3 spawnpoint = spawnpoints[randomNum2].transform.position;
                //Spawns an enemy at the chosen spawnpoint
                enemyClone = Instantiate(enemiesClone[randomNum].gameObject, spawnpoint, Quaternion.identity);
            }
            EnemyStats enemyCloneStats = enemyClone.GetComponent<EnemyStats>();
            enemyCloneStats.SetSpawner(gameObject);

            //Adds the enemy to spawned enemies list
            spawnedEnemies.Add(enemyClone);

            //subtracts enemy points from spawner's
            pointsClone -= enemyCloneStats.GetPoints();

            //Adds children to remainingchildren counter
            remainingChildren += enemyCloneStats.children;

        }

        yield return new WaitForSeconds(timer);
        spawnAgain = true;
    }


    #region SpawnerFunctions

    //Get-setters for enabled
    public bool GetEnabled()
    {
        return spawnEnabled;
    }

    public void SetEnabled(bool _enable)
    {
        spawnEnabled = _enable;
    }
    
    //Function that takes in a bool and sets the doors to be active/inactive if the bool is true/false
    public void SetDoorLock(bool _lock)
    {
        for (int i = 0; i < doors.Count; i++)
            doors[i].SetActive(_lock);
        if (!_lock)
        {
            foreach (Transform child in transform.parent)
                if (child.gameObject.layer == 16)
                    child.gameObject.GetComponent<DartAI>().DisableAttack();
        }
        else if (_lock)
        {
            foreach (Transform childs in transform.parent)
                if (childs.gameObject.layer == 16)
                    childs.gameObject.GetComponent<DartAI>().EnableAttack();
        }
    }

    //Function that changes the locks of some doors but not others
    public void SetDoorRange(bool _lock, int _lower, int _upper)
    {
        for (int i = _lower; i < _upper; i++)
            doors[i].SetActive(_lock);
    }

    //Resets the spawner
    public void ResetSpawner()
    {
        enemiesClone.Clear();
        pointsClone = points;
        if (enemies.Count > 0)
            for (int i = 0; i < enemies.Count; i++)
                enemiesClone.Add(enemies[i].GetComponent<EnemyStats>());
    }

    //For use with splitting enemies.
    public void AddEnemy(GameObject _enemy)
    {
        spawnedEnemies.Add(_enemy);
    }

    //Searches list and removes any null values (dead enemies)
    public void RefreshSpawnedEnemies()
    {
        for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
            if (spawnedEnemies[i] == null)
                spawnedEnemies.RemoveAt(i);
    }

    public void AddRemainingChild()
    {
        remainingChildren++;
    }
    public void SubtractRemainingChild()
    {
        remainingChildren--;
    }

    public int GetNumEnemies()
    {
        return spawnedEnemies.Count;
    }

    public int GetPointsRemaining()
    {
        return pointsClone;
    }
    #endregion
}