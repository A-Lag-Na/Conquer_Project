using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    #region Stats
    //How many points the enemy is worth to the spawner, and how much experience it grants on kill.
    [SerializeField] private int enemyPoints = 1;

    //How many hits it takes to kill the enemy.
    [SerializeField] private float health = 2;

    //How much damage the enemy deals on hit.
    [SerializeField] private int damage = 2;

    //Amount of seconds between attacks.
    [SerializeField] private float attackRate = 1;

    //Speed at whichc bullets travel
    [SerializeField] private float bulletSpeed = 10;

    public bool isFireImmune;
    public bool isIceImmune;
    public bool isStunImmune;
    //Has this enemy been spawned externally? (through splitter or spawner enemy)
    private bool isChild;
    #endregion

    #region UnityComponents

    //Pickup the enemy will drop
    [SerializeField] GameObject pickUp = null;

    [SerializeField] GameObject childEnemy = null;

    //Need this to notify the spawner to add new enemies on split.
    [SerializeField] GameObject spawnerObject;
    SpawnScript spawnerScript;

    public int children;

    //Enemy's color and renderer
    private Renderer enemyRender;
    public Color enemyColor;

    Animator anim;
    GameObject player;
    Player playerScript;
    #endregion

    public void Start()
    {
        GetComponent<AudioSource>().enabled = true;
        enemyRender = GetComponentInChildren<Renderer>();
        enemyColor = enemyRender.material.color;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponentInParent<Player>();
        anim = GetComponent<Animator>();

        if(spawnerObject != null)
        {
            spawnerScript = spawnerObject.GetComponent<SpawnScript>();
        }
    }

    public void Update()
    {
        if (enemyRender.material.color != enemyColor)
            enemyRender.material.color = Color.Lerp(enemyRender.material.color, enemyColor, 0.1f);
    }
    
    #region Getters and Setters
    public int GetPoints()
    {
        return enemyPoints;
    }
    public float GetHealth()
    {
        return health;
    }
    public int GetDamage()
    {
        return damage;
    }
    public float GetAttackRate()
    {
        return attackRate;
    }
    public float GetMovementSpeed()
    {
        return GetComponent<NavMeshAgent>().speed;
    }
    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }
    public void SetHealth(float _health)
    {
        health = _health;
    }
    public void SetDamage(int _damage)
    {
        damage = _damage;
    }
    public void SetAttackRate(float _attackRate)
    {
        attackRate = _attackRate;
    }
    public void SetMovementSpeed(float _speed)
    {
        GetComponent<NavMeshAgent>().speed = _speed;
    }
    public void SetBulletSpeed(float _speed)
    {
        bulletSpeed = _speed;
    }
    #endregion

    #region EnemyFunctions
    //Our enemy is damaged
    public void TakeDamage(float _damage = 1)
    {
        if(damage > 0f)
        {
            BlinkOnHit();
            health -= _damage;
            if (health <= 0)
            {
                if (anim != null)
                {
                    anim.SetBool("Dead", true);
                }
                Kill();
            }
        }
    }

    //Kill function
    public void Kill()
    {
        if (pickUp != null)
        {
            Vector3 vec = GetComponent<Transform>().position;
            vec = new Vector3(vec.x, vec.y + 0.5f, vec.z);
            Instantiate(pickUp, vec, Quaternion.identity);
        }
        if(childEnemy != null && children > 0)
        {
            Split(children);
        }
        if(isChild)
        {
            spawnerScript.remainingChildren -= 1;
        }
        if (playerScript != null)
            playerScript.GainExperience(enemyPoints);
        Destroy(gameObject);
    }

    public void Split(int children)
    {
        for (int i = 0; i < children; i++)
        {
            GameObject child = Instantiate(childEnemy, transform.position, Quaternion.identity);
            EnemyStats childStats = child.GetComponent<EnemyStats>();

            childStats.isChild = true;

            if(spawnerObject != null)
            { 
                childStats.SetSpawner(spawnerObject);
                spawnerScript.AddEnemy(child);
                spawnerScript.remainingChildren += childStats.children;
            }
        }
    }

    //Color feedback on damage taken
    public void BlinkOnHit()
    {
        if (anim != null)
            anim.SetTrigger("On Hit");
        enemyRender.material.color = Color.red;
    }

    public void SetSpawner(GameObject _spawner)
    {
        spawnerObject = _spawner;
    }
    public Renderer GetRenderer()
    {
        return enemyRender;
    }
    #endregion
}