using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip hurt;
    //public AudioClip burn;
    public int bulletDamage;
    public GameObject sparks;
    public GameObject blood;
    public bool iceImmune = false;
    public bool fireImmune = false;
    public bool stunImmune = false;

    private Player player;
    private EnemyStats enemy;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.collider.gameObject;
        audioSource = target.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.volume = 1.0f;
            audioSource.PlayOneShot(hurt);
            audioSource.volume = 0.5f;
        }
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("BulletHell Enemy") || collision.collider.CompareTag("Fire Enemy") || collision.collider.CompareTag("Ice Enemy"))
        {
            if (collision.collider.CompareTag("Player"))
            {
                player = collision.collider.GetComponent<Player>();
                //The enemy we hit takes damage.
                fireImmune = player.fireImmune;
                iceImmune = player.iceImmune;
                stunImmune = player.stunImmune;
            }
            if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("BulletHell Enemy") || collision.collider.CompareTag("Fire Enemy") || collision.collider.CompareTag("Ice Enemy"))
            {
                enemy = collision.collider.GetComponent<EnemyStats>();
                //The enemy we hit takes damage.
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
            if (con != null)
            {
                //Apply extra on-hit effects here:
                switch (gameObject.tag)
                {
                    case "Fire Bullet":
                        {
                            if (!fireImmune)
                            {
                                DamageCheck();
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
                                DamageCheck();
                                con.SubtractSpeed(0.6f);
                                con.TimerAdd("thaw", 90);
                            }
                            break;
                        }
                    case "Stun Bullet":
                        {
                            if (!stunImmune)
                            {
                                DamageCheck();
                                if (collision.collider.CompareTag("BulletHell Enemy"))
                                {

                                }
                                else
                                {
                                    enemy.GetComponent<EnemyAI>().OnPauseGame();
                                }
                                enemy.StopAllCoroutines();
                            }
                            break;
                        }
                    default:
                        {
                            DamageCheck();
                            break;
                        }
                }
            }
        }
        Destroy(gameObject);
    }
    private void DamageCheck()
    {
        if (player != null)
        {
            player.TakeDamage();
            Instantiate(blood, transform.position, blood.transform.rotation);
        }
        if (enemy != null)
        {
            enemy.TakeDamage();
            Instantiate(blood, transform.position, blood.transform.rotation);
        }
    }
}

