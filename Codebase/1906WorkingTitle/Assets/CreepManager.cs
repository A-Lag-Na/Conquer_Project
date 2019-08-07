using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepManager : MonoBehaviour
{
    Player player;
    EnemyStats enemy;
    bool fireImmune;
    bool iceImmune;
    private void OnTriggerStay(Collider other)
    {
        string colTag = other.gameObject.tag;
        ConditionManager con = other.gameObject.GetComponentInParent<ConditionManager>();

        //Burn sound effect
        //audioSource.PlayOneShot(burn);

        if (colTag == "Player")
        {
            player = other.GetComponent<Player>();
            fireImmune = player.isFireImmune;
            iceImmune = player.isIceImmune;
        }
        else if (colTag == "Enemy" || colTag == "BulletHell Enemy" || colTag == "Fire Enemy" || colTag == "Ice Enemy")
        {
            enemy = other.GetComponent<EnemyStats>();
            fireImmune = enemy.fireImmune;
            iceImmune = enemy.iceImmune;
        }
        else { }

        switch (gameObject.tag)
        {
            case "FirePot":
                {
                    if ((player != null || enemy != null) && !fireImmune)
                    {
                        con.TimerAdd("fire", 3);
                    }
                    break;
                }
            case "GluePot":
                {
                    if ((player != null || enemy != null) && !iceImmune)
                    {
                        con.SubtractSpeed(0.006f);
                        con.TimerAdd("thaw", 1);
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
