using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollisionScript : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip hurt;
    //public AudioClip burn;
    public int bulletDamage;
    public GameObject sparks;
    public GameObject blood;
    public bool isIceImmune = false;
    public bool isFireImmune = false;
    public bool isStunImmune = false;

    private Player player;
    private EnemyStats enemy;
    private NavMeshAgent nav;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.collider.gameObject;
        nav = target.GetComponent<NavMeshAgent>();

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
                isFireImmune = player.isFireImmune;
                isIceImmune = player.isIceImmune;
                isStunImmune = player.isStunImmune;
            }
            if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("BulletHell Enemy") || collision.collider.CompareTag("Fire Enemy") || collision.collider.CompareTag("Ice Enemy"))
            {
                enemy = collision.collider.GetComponent<EnemyStats>();
                //The enemy we hit takes damage.
                isFireImmune = enemy.fireImmune;
                isIceImmune = enemy.iceImmune;
                isStunImmune = enemy.stunImmune;
            }
        }
        else
            Instantiate(sparks, transform.position, sparks.transform.rotation);
        if (!(isIceImmune && isFireImmune))
        {
            ConditionManager con = target.GetComponent<ConditionManager>();
            if (con != null)
            {
                //Apply extra on-hit effects here:
                switch (gameObject.tag)
                {
                    case "Fire Bullet":
                        {
                            if (!isFireImmune)
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
                            if (!isIceImmune)
                            {
                                DamageCheck();
                                con.SubtractSpeed(0.6f);
                                con.TimerAdd("thaw", 90);
                            }
                            break;
                        }
                    case "Stun Bullet":
                        {
                            if (!isStunImmune)
                            {
                                DamageCheck();
                                if (target.CompareTag("BulletHell Enemy"))
                                {
                                    BulletHellEnemy bulletHellAI = enemy.GetComponent<BulletHellEnemy>();
                                    bulletHellAI.Stun();
                                    nav.enabled = false;
                                }
                                else if (!target.CompareTag("Player"))
                                {
                                    EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
                                    enemyAI.Stun();
                                    nav.enabled = false;
                                }
                                else
                                    player.Stun();
                                con.TimerAdd("stun", 31);
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