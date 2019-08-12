using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollisionScript : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip hurt = null;
    //public AudioClip burn;
    public float bulletDamage;
    [SerializeField] GameObject sparks = null;
    [SerializeField] GameObject blood = null;
    private bool isIceImmune = false;
    private bool isFireImmune = false;
    private bool isStunImmune = false;

    private Player player;
    private EnemyStats enemy;
    private EnemyAI ai;
    private NavMeshAgent nav;

    [SerializeField] GameObject fireCreep = null;
    [SerializeField] GameObject iceCreep = null;

    [SerializeField] GameObject owner = null;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.collider.gameObject;
        if (target != owner)
        {
            nav = target.GetComponent<NavMeshAgent>();

            audioSource = target.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = 1.0f;
                audioSource.PlayOneShot(hurt);
                audioSource.volume = 0.5f;
            }
            if (target.CompareTag("Player") || target.CompareTag("Enemy") || target.CompareTag("BulletHell Enemy"))
            {
                if (target.CompareTag("Player"))
                {
                    player = target.GetComponent<Player>();
                    isFireImmune = player.isFireImmune;
                    isIceImmune = player.isIceImmune;
                    isStunImmune = player.isStunImmune;
                }
                if (target.CompareTag("Enemy") || target.CompareTag("BulletHell Enemy")) 
                {
                    if(!target.CompareTag("BulletHell Enemy"))
                        ai = target.GetComponent<EnemyAI>();
                    enemy = target.GetComponent<EnemyStats>();
                    isFireImmune = enemy.isFireImmune;
                    isIceImmune = enemy.isIceImmune;
                    isStunImmune = enemy.isStunImmune;
                }
            }
            else
            {
                Instantiate(sparks, transform.position, sparks.transform.rotation);
                {
                    if (gameObject.CompareTag("FirePot"))
                    {
                        Instantiate(fireCreep, transform.position, fireCreep.transform.rotation);
                    }
                    if (gameObject.CompareTag("IcePot"))
                    {
                        Instantiate(iceCreep, transform.position, iceCreep.transform.rotation);
                    }
                }
            }
            if (!(isIceImmune && isFireImmune && isStunImmune))
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
                                        if (enemyAI != null)
                                            enemyAI.Stun();
                                        else
                                        {
                                            BulletHellEnemy bulletHellAI = enemy.GetComponent<BulletHellEnemy>();
                                            bulletHellAI.Stun();
                                        }
                                        nav.enabled = false;
                                    }
                                    else
                                        player.Stun();
                                    con.TimerAdd("stun", 16);
                                }
                                break;
                            }
                        case "Love Bullet":
                            {
                                if(ai != null)
                                {
                                    DamageCheck();
                                    ai.FallInLove(5.0f);
                                }
                                break;
                            }
                        case "FirePot":
                            {
                                DamageCheck();
                                Instantiate(sparks, transform.position, sparks.transform.rotation);
                                Instantiate(fireCreep, transform.position, fireCreep.transform.rotation);
                                break;
                            }
                        case "IcePot":
                            {
                                DamageCheck();
                                Instantiate(sparks, transform.position, sparks.transform.rotation);
                                Instantiate(iceCreep, transform.position, iceCreep.transform.rotation);
                                break;
                            }
                        case "Hex":
                            {
                                DamageCheck();
                                if (!isFireImmune)
                                {
                                    //Burn sound effect
                                    //audioSource.PlayOneShot(burn);
                                    con.TimerAdd("fire", 179);
                                }
                                if (!isIceImmune)
                                {
                                    con.SubtractSpeed(0.6f);
                                    con.TimerAdd("thaw", 90);
                                }
                                if (!isStunImmune)
                                {
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
                                    con.TimerAdd("stun", 16);
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
                Destroy(gameObject);
            }
        }
    }
    private void DamageCheck()
    {
        if (player != null)
        {
            player.TakeDamage(bulletDamage);
            Instantiate(blood, transform.position, blood.transform.rotation);
        }
        if (enemy != null)
        {
            enemy.TakeDamage(bulletDamage);
            Instantiate(blood, transform.position, blood.transform.rotation);
        }
    }
    public GameObject GetOwner()
    {
        return owner;
    }
    public void SetOwner(GameObject _owner)
    {
        owner = _owner;
    }
}