using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    //How many points the enemy is worth to the spawner
    [SerializeField] private int enemyPoints = 1;

    //How many hits it takes to kill the enemy.
    [SerializeField] private int health = 2;

    //How much damage the enemy deals on hit.
    [SerializeField] private int damage = 2;

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

    //Our enemy is damaged
    public void TakesDamage(int _damage = 1)
    {
        health -= damage;
        if(health <= 0)
        {
            Kill();
        }
    }

    //Kill function
    public void Kill()
    {
        GameObject.Destroy(this);
    }
}
