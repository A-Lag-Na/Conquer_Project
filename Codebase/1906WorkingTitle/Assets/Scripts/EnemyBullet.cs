using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //GameObject player = GameObject.FindGameObjectWithTag("Player");
            //Hardcoding for now, will refactor on bullet prefab refactor
            //player.GetComponent<Player>().TakeDamage();
            collision.collider.GetComponentInParent<Player>().TakeDamage();
            //Refactored version
            //player.GetComponent<Player>().TakeDamage(this.GetComponent<BulletStats>().GetDamage());
        }
        Destroy(gameObject);
    }
}