using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        string temp = gameObject.tag;
        switch(temp)
        {
            case "Player Bullet":
                {
                    if (collision.collider.CompareTag("Enemy"))
                    {
                        //The enemy we hit takes damage.
                        collision.collider.GetComponentInParent<EnemyStats>().TakesDamage();
                    }
                    break;
                }
            case "Enemy Bullet":
                {
                    if (collision.collider.CompareTag("Player"))
                    {
                        collision.collider.GetComponentInParent<Player>().TakeDamage();
                    }
                    break;
                }
            default:
                break;
        }
    }
}