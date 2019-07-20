using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        int temp = gameObject.layer;
        Destroy(gameObject);
        GameObject target = collision.collider.gameObject;
        if (collision.collider.CompareTag("Enemy"))
        {
            //The enemy we hit takes damage.
            collision.collider.GetComponentInParent<EnemyStats>().TakeDamage(1);
        }
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponentInParent<Player>().TakeDamage(1);
        }
        //Apply extra on-hit effects here:
        switch (gameObject.tag)
        {
            case "Fire Bullet":
                {
                    target.GetComponentInParent<ConditionManager>().TimerAdd("fire", 179);
                    break;
                }
            case "Ice Bullet":
                {
                    target.GetComponentInParent<ConditionManager>().TimerAdd("ice", 179);
                    break;
                }
        }
    }
}