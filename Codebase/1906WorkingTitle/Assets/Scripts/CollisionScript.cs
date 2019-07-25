﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hurt;
    public int bulletDamage;
    public bool iceImmune = false;
    public bool fireImmune = false;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.collider.gameObject;
        audioSource = target.GetComponent<AudioSource>();
        if(audioSource != null)
        {
            audioSource.enabled = true;
            audioSource.PlayOneShot(hurt);
        }
        if (collision.collider.CompareTag("Enemy"))
        {
            EnemyStats temp = collision.collider.GetComponent<EnemyStats>();
            //The enemy we hit takes damage.
            temp.TakeDamage(bulletDamage);
            fireImmune = temp.fireImmune;
            iceImmune = temp.iceImmune;
        }
        if (collision.collider.CompareTag("Player"))
        {
            Player temp = collision.collider.GetComponent<Player>();
            //The enemy we hit takes damage.
            temp.TakeDamage(bulletDamage);
            fireImmune = temp.fireImmune;
            iceImmune = temp.iceImmune;
            collision.collider.GetComponent<Player>().TakeDamage(bulletDamage);
        }
        if(!(iceImmune && fireImmune))
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
                                con.TimerAdd("fire", 179);
                            }
                            break;
                        }
                    case "Ice Bullet":
                        {
                            if (!iceImmune)
                            {
                                con.SubtractSpeed((float)0.6);
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