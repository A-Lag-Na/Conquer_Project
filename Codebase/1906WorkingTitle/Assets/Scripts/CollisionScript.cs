using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hurt;
    //public AudioClip burn;
    public int bulletDamage;
    public GameObject sparks;
    public GameObject blood;
    public bool iceImmune = false;
    public bool fireImmune = false;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.collider.gameObject;
        audioSource = target.GetComponent<AudioSource>();
        if(audioSource != null)
        {
            audioSource.volume = 1.0f;
            audioSource.PlayOneShot(hurt);
            audioSource.volume = 0.5f;
        }
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("BulletHell Enemy") || collision.collider.CompareTag("Fire Enemy") || collision.collider.CompareTag("Ice Enemy"))
        {
            Instantiate(blood, transform.position, blood.transform.rotation);
            if (collision.collider.CompareTag("Player"))
            {
                Player player = collision.collider.GetComponent<Player>();
                //The enemy we hit takes damage.
                player.TakeDamage(bulletDamage);
                fireImmune = player.fireImmune;
                iceImmune = player.iceImmune;
            }
            if(collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("BulletHell Enemy") || collision.collider.CompareTag("Fire Enemy") || collision.collider.CompareTag("Ice Enemy"))
            {
                EnemyStats enemy = collision.collider.GetComponent<EnemyStats>();
                //The enemy we hit takes damage.
                enemy.TakeDamage(bulletDamage);
                fireImmune = enemy.fireImmune;
                iceImmune = enemy.iceImmune;
            }
        }
        else
        {
            Instantiate(sparks, transform.position, sparks.transform.rotation);
        }
        if (!(iceImmune && fireImmune))
        {
            ConditionManager con = target.GetComponent<ConditionManager>();
            if (gameObject.tag != "Untagged" && con != null)
            {
                //Apply extra on-hit effects here:
                switch (gameObject.tag)
                {
                    case "Fire Bullet":
                        {
                            if (!fireImmune)
                            {
                                //Burn sound effect
                                //audioSource.PlayOneShot(burn);
                                con.TimerAdd("fire", 179);
                            }
                            break;
                        }
                    case "Ice Bullet":
                        {
                            if (!iceImmune)
                            {
                                con.SubtractSpeed(0.6f);
                                con.TimerAdd("thaw", 90);
                            }
                            break;
                        }
                }
            }
        }
        Destroy(gameObject);
    }
}