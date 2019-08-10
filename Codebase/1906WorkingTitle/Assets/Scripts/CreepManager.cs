using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepManager : MonoBehaviour
{
    Player player;
    EnemyStats enemy;
    bool isFireImmune;
    bool isIceImmune;

    private void OnTriggerStay(Collider collide)
    {
        string colTag = collide.gameObject.tag;
        ConditionManager con = collide.gameObject.GetComponentInParent<ConditionManager>();

        //Burn sound effect
        //audioSource.PlayOneShot(burn);

        if (colTag == "Player")
        {
            player = collide.GetComponent<Player>();
            isFireImmune = player.isFireImmune;
            isIceImmune = player.isIceImmune;
        }
        else if (colTag == "Enemy" || colTag == "BulletHell Enemy" || colTag == "Fire Enemy" || colTag == "Ice Enemy")
        {
            enemy = collide.GetComponent<EnemyStats>();
                isFireImmune = enemy.isFireImmune;
                isIceImmune = enemy.isIceImmune;
        }
        else { }
        if(con!=null)
        {
            switch (gameObject.tag)
            {
                case "FirePot":
                    {
                        if ((player != null || enemy != null) && !isFireImmune)
                        {
                            con.TimerAdd("fire", 3);
                        }
                        break;
                    }
                case "IcePot":
                    {
                        if ((player != null || enemy != null) && !isIceImmune)
                        {
                            con.SubtractSpeed(0.006f);
                            con.TimerAdd("thaw", 1);
                        }
                        break;
                    }
                case "Aura":
                    {
                        if(player != null || enemy != null)
                        {
                            con.TimerAdd("aura", 1);
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
}
