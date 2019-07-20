using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        int temp = gameObject.layer;
        Destroy(gameObject);
        switch (temp)
        {
            case 10:
                {
                    if (collision.collider.CompareTag("Enemy"))
                    {
                        GameObject enemy = collision.collider.gameObject;

                        //Apply extra on-hit effects here:
                        switch(gameObject.tag)
                        {
                            case "Fire Bullet":
                                {
                                    enemy.GetComponentInParent<ConditionManager>().TimerAdd("fire", 179);
                                    break;
                                }
                            case "Ice Bullet":
                                {
                                    enemy.GetComponentInParent<ConditionManager>().TimerAdd("ice", 179);
                                    break;
                                }
                        }
                        //The enemy we hit takes damage.
                        collision.collider.GetComponentInParent<EnemyStats>().TakeDamage(1);
                    }
                    break;
                }
            case 12:
                {
                    if (collision.collider.CompareTag("Player"))
                    {
                        collision.collider.GetComponentInParent<Player>().TakeDamage(1);
                    }
                    break;
                }
            default:
                break;
        }
    }
}