using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    public int fireTimer = 0;
    public int iceTimer = 0;
    public int thawTimer = 0;

    [SerializeField] bool isPlayer;
    private Component statsScript;
    private float speed;

    public void Start()
    {
        if (gameObject.tag.Equals("Player"))
        {
            isPlayer = true;
            statsScript = GetComponentInParent<Player>();
        }
        else
        {
            isPlayer = false;
            statsScript = GetComponentInParent<EnemyStats>();
        }
        speed = GetSpeed();
    }

    public void Update()
    {
        if (fireTimer > 0 || iceTimer > 0 || thawTimer > 0)
        {
            if (fireTimer > 0)
            {
                fireTimer--;
                if (fireTimer % 60 == 0)
                {
                   Damage(1);
                }
            }
            if (iceTimer > 0)
            {

            }
        }
    }

    public void TimerAdd(string condition, int ticks)
    {
        switch (condition)
        {
            case "fire":
                {
                    fireTimer += ticks;
                    break;
                }
            case "ice":
                {
                    iceTimer += ticks;
                    thawTimer += ticks;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void TimerSet(string condition, int ticks)
    {
        switch (condition)
        {
            case "fire":
                {
                    fireTimer = ticks;
                    break;
                }
            case "ice":
                {
                    iceTimer = ticks;
                    thawTimer = ticks;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    //Cast get-setters (This workaround feels silly and wrong)
    public float GetSpeed()
    {
        if(isPlayer)
        {
            return ((Player)statsScript).GetMovementSpeed();
        }
        else
        {
            return ((EnemyStats)statsScript).GetMovementSpeed();
        }
    }
    public void SetSpeed(float _speed)
    {
        if (isPlayer)
        {
            ((Player)statsScript).SetMovementSpeed(_speed);
        }
        else
        {
            ((EnemyStats)statsScript).SetMovementSpeed(_speed);
        }
    }
    public void Damage(int _damage)
    {
        if (isPlayer)
        {
            ((Player)statsScript).TakeDamage(_damage);
        }
        else
        {
            ((EnemyStats)statsScript).TakeDamage(_damage);
        }
    }
}
