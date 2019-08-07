using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    //Whether the spawner is spawning or not.
    [SerializeField] private bool spawnEnabled;

    //List of different enemies the spawner can choose to spawn.
    [SerializeField] private List<GameObject> enemies = null;
    public List<EnemyStats> enemiesClone;

    //list of doors.
    [SerializeField] private List<GameObject> doors = null;

    //How many points worth of enemies the spawner can soawn
    [SerializeField] private int points = 0;
    private int pointsClone;

    //Tracks the number of frames passing
    public int timer;

    bool spawnAgain = true;

    //List of enemies spawned by the spawner.
    public List<GameObject> spawnedEnemies;

    // Start is called before the first frame update
    void Start()
    {
        pointsClone = points;
        enemiesClone = new List<EnemyStats>();
        for (int i = 0; i < enemies.Count; i++)
        {
            enemiesClone.Add(enemies[i].GetComponent<EnemyStats>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnAgain && spawnEnabled)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    //Get-setters for enabled
    public bool GetEnabled()
    {
        return spawnEnabled;
    }

    public void SetEnabled(bool _enable)
    {
        spawnEnabled = _enable;
    }

    IEnumerator SpawnEnemy()
    {
        if (spawnedEnemies.Count == 0 && pointsClone < 1)
        {
            SetDoorLock(false);
        }
        //Searches list and removes any null values (dead enemies)
        for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (spawnedEnemies[i] == null)
            {
                spawnedEnemies.RemoveAt(i);
            }
        }
        spawnAgain = false;
        if (pointsClone > 0)
        {
            for (int i = 0; i < enemiesClone.Count; i++)
            {
                if (enemiesClone[i].GetPoints() > pointsClone)
                {
                    enemiesClone.Remove(enemiesClone[i]);
                }
            }
            //Temp selects 
            int temp = Mathf.RoundToInt(Random.Range(0, enemiesClone.Count));

            //Spawns an enemy
            GameObject enemyClone = Instantiate(enemies[temp], transform.position, Quaternion.identity);

            //Adds the enemy to spawned enemies list
            spawnedEnemies.Add(enemyClone);

            //subtracts enemy points from spawner's
            pointsClone -= enemies[temp].GetComponent<EnemyStats>().GetPoints();
        }
        yield return new WaitForSeconds(timer);
        spawnAgain = true;
    }

    //Function that takes in a bool and sets the doors to be active/inactive if the bool is true/false
    public void SetDoorLock(bool _lock)
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].SetActive(_lock);
        }
        if (!_lock)
        {
            foreach (Transform child in transform.parent)
            {
                if(child.gameObject.layer == 15)
                {
                    child.gameObject.GetComponent<DartAI>().DisableAttack();
                }
            }
        }
    }

    //Function that changes the locks of some doors but not others
    public void SetDoorRange(bool _lock, int _lower, int _upper)
    {
        for (int i = _lower; i < _upper; i++)
        {
            doors[i].SetActive(_lock);
        }
    }

    //Resets the spawner
    public void ResetSpawner()
    {
        enemiesClone.Clear();
        pointsClone = points;
        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemiesClone.Add(enemies[i].GetComponent<EnemyStats>());
            }
        }
    }
}
