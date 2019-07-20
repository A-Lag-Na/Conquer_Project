using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        int temp = gameObject.layer;
        switch(temp)
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
                                    enemy.GetComponentInParent<ConditionManager>().TimerAdd("fire", 30);
                                    break;
                                }
                            case "Ice Bullet":
                                {

                                    break;
                                }
                        }
                        //The enemy we hit takes damage.
                        collision.collider.GetComponentInParent<EnemyStats>().TakesDamage();
                        
                    }
                    break;
                }
            case 12:
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