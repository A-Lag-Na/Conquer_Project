using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //The enemy we hit takes damage.
            collision.collider.GetComponentInParent<EnemyStats>().TakesDamage();
        }
        Destroy(gameObject);
    }
}