using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    public AudioSource audioSource;
        
    int playerDamage;
    GameObject player;
    Player playerScript;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
    }

    private void Update()
    {
        playerDamage = playerScript.GetDamage();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.collider.gameObject;
        audioSource = target.GetComponent<AudioSource>();
        if(audioSource != null)
        {
            audioSource.enabled = true;
            audioSource.Play();
        }
        if (collision.collider.CompareTag("Enemy"))
        {
            //The enemy we hit takes damage.
            collision.collider.GetComponent<EnemyStats>().TakeDamage(playerDamage);
        }
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().TakeDamage(1);
        }
        ConditionManager con = target.GetComponent<ConditionManager>();
        if (gameObject.tag != "Untagged" && con != null)
        {
            //Apply extra on-hit effects here:
            switch (gameObject.tag)
            {
                case "Fire Bullet":
                    {
                        con.TimerAdd("fire", 179);
                        break;
                    }
                case "Ice Bullet":
                    {
                        con.SubtractSpeed((float)0.6);
                        con.TimerAdd("thaw", 90);
                        break;
                    }
            }
        }
        Destroy(gameObject);
    }
}