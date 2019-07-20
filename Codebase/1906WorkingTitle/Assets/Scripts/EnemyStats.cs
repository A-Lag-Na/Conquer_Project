using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    //How many points the enemy is worth to the spawner
    [SerializeField] private int enemyPoints = 1;

    //How many hits it takes to kill the enemy.
    [SerializeField] private int health = 2;

    //How much damage the enemy deals on hit.
    [SerializeField] private int damage = 2;

    //Pickup the enemy will drop
    [SerializeField] GameObject pickUp;
    //Amount of time enemy blinks on taking damage
    public float blinkTime;

    //Enemy's color and renderer
    private Renderer enemyRender;
    public Color enemyColor;

    //get-setters
    public int GetPoints()
    {
        return enemyPoints;
    }
    public int GetHealth()
    {
        return health;
    }
    public int GetDamage()
    {
        return damage;
    }
    public void SetHealth(int _health)
    {
        health = _health;
    }
    public void SetDamage(int _damage)
    {
        damage = _damage;
    }
    public float GetMovementSpeed()
    {
        return GetComponent<NavMeshAgent>().speed;
    }
    public void SetMovementSpeed(float _speed)
    {
       GetComponent<NavMeshAgent>().speed = _speed;
    }

    //Our enemy is damaged
    public void TakeDamage(int _damage = 1)
    {
        BlinkOnHit();
        health -= damage;
        if(health <= 0)
        {
            Kill();
        }
    }

    //Kill function
    public void Kill()
    {
        if(pickUp != null)
        {
            Vector3 vec = GetComponent<Transform>().position;
            vec = new Vector3(vec.x, vec.y + 0.5f, vec.z);
            Instantiate(pickUp, vec, Quaternion.identity);
        }
        Destroy(gameObject);
    }


    public void Start()
    {
        enemyRender = GetComponentInParent<Renderer>();
        enemyColor = enemyRender.material.color;
    }

    public void Update()
    {
        if (enemyRender.material.color != enemyColor)
        {
            enemyRender.material.color = Color.Lerp(enemyRender.material.color, enemyColor, blinkTime);
        }
    }

    //Enemy feedback on damage taken
    public void BlinkOnHit()
    {
        enemyRender.material.color = Color.red;
    }
}
