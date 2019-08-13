﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    #region EnemyStats
    [SerializeField] private float attackRate = 0.0f;
    private float bulletSpeed = 0.0f;
    int bulletDamage = 0;
    public bool isStunned = false;
    private bool isPaused, inLove = false;
    bool attackEnabled = true;
    #endregion

    //If this enemy's attack behavior is enabled or not.

    #region UnityComponents
    //What projectile the enemy shoots
    [SerializeField] GameObject projectile = null;
    [SerializeField] GameObject projectilePos = null;

    [SerializeField] AudioClip fire = null;

    EnemyStats enemyStats = null;
    SpawnScript spawnScript = null;

    Animator anim = null;
    NavMeshAgent agent = null;
    GameObject player = null;
    GameObject target = null;
    AudioSource source = null;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyStats = GetComponent<EnemyStats>();
        spawnScript = enemyStats.GetSpawner().GetComponent<SpawnScript>();
        attackRate = enemyStats.GetAttackRate();
        bulletSpeed = enemyStats.GetBulletSpeed();
        bulletDamage = enemyStats.GetDamage();
        source = GetComponentInParent<AudioSource>();
        source.enabled = true;
        target = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused && !isStunned)
        {
            if (inLove && spawnScript.GetNumEnemies() > 1 && target == null && player != null)
                SetTarget();
            if(target.transform.position != null)
                agent.SetDestination(target.transform.position);
            if (agent.remainingDistance < agent.stoppingDistance || GetComponent<NavMeshAgent>().speed <= 0)
            {
                Vector3 targetPosition = target.transform.position;
                Vector3 relativePosition = targetPosition - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePosition);
                // Lock the rotation around X and Z Axes
                rotation.x = 0.0f;
                rotation.z = 0.0f;
                // Change the enemy's tranform's rotation to the rotation Quaternion
                agent.transform.rotation = rotation;
            }

            if (attackEnabled)
                StartCoroutine(EnemyAttack());
        }
    }

    #region EnemyFunctions
    //Call this function after you change either attackRate or bulletSpeed in EnemyStats while the enemy is active.
    public void RefreshStats()
    {
        EnemyStats enemyStats = GetComponent<EnemyStats>();
        attackRate = enemyStats.GetAttackRate();
        bulletSpeed = enemyStats.GetBulletSpeed();
    }

    public void OnPauseGame()
    {
        isPaused = true;
    }

    public void OnResumeGame()
    {
        isPaused = false;
    }

    public void Stun()
    {
        isStunned = true;
    }

    public void Unstun()
    {
        isStunned = false;
    }

    public IEnumerator FallInLove(float time)
    {
        SetTarget();
        inLove = true;
        gameObject.layer = 9;
        yield return new WaitForSeconds(5f);
        gameObject.layer = 11;
        inLove = false;
        target = player;
    }

    private void SetTarget()
    {
        List<GameObject> gameObjectList = new List<GameObject>();
        //Switching array to a list for the purpose of accessing list functions
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in gameObjects)
            gameObjectList.Add(go);
        //Using this check to prevent errors from using a love bullet when only one enemy on screen.
        if (gameObjectList.Count > 1)
        {
            GameObject closest = null;
            float distance = float.MaxValue;
            Vector3 position = transform.position;
            foreach (GameObject gameobject in gameObjects)
            {
                if (gameobject != gameObject)
                {
                    float curDistance = Vector3.Distance(position, gameobject.transform.position);
                    if (curDistance < distance)
                    {
                        closest = gameobject;
                        distance = curDistance;
                    }
                }
            }
            target = closest;
        }
    }

    public bool InLove()
    {
        return inLove;
    }

    IEnumerator EnemyAttack()
    {
        attackEnabled = false;
        GameObject clone = Instantiate(projectile, projectilePos.transform.position, projectile.transform.rotation);
        CollisionScript cs = clone.GetComponent<CollisionScript>();
        cs.bulletDamage = bulletDamage;
        cs.SetOwner(gameObject);
        //Need these layer sets so that enemies don't shoot each other outside of love condition.
        if (inLove)
            clone.layer = 10;
        else
            clone.layer = 12;
        clone.SetActive(true);
        source.PlayOneShot(fire);
        clone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        if (anim != null && !isStunned)
            anim.SetTrigger("Attack");
        yield return new WaitForSeconds(attackRate);
        attackEnabled = true;
    }
    #endregion
}