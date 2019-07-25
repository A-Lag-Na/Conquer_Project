using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    public int fireTimer = 0;
    public int thawTimer = 0;

    [SerializeField] bool isPlayer;
    private Component statsScript;
    private float speed;
    [SerializeField] private float minFrozenSpeed;
    private float maxSpeed;
    //thawIncrement: How much the player's  movement speed increases on a thaw tick
    [SerializeField] private float thawIncrement;
    [SerializeField] private int fireDamage;
    GameObject fireParticle;
    GameObject iceParticle;

    public void Start()
    {
        if (gameObject.tag.Equals("Player"))
        {
            isPlayer = true;
            statsScript = GetComponentInParent<Player>();
            fireParticle = GameObject.Find("PlayerFire");
            iceParticle = GameObject.Find("PlayerIce");
        }
        else if (gameObject.tag.Equals("Enemy"))
        {
            isPlayer = false;
            statsScript = GetComponentInParent<EnemyStats>();
            fireParticle = GameObject.Find("RangedFire");
            iceParticle = GameObject.Find("RangedIce");
        }
        else if (gameObject.tag.Equals("BulletHell Enemy"))
        {
            isPlayer = false;
            statsScript = GetComponentInParent<EnemyStats>();
            fireParticle = GameObject.Find("BulletHell Fire");
            iceParticle = GameObject.Find("BulletHell Ice");
        }
        else if (gameObject.tag.Equals("Fire Enemy"))
        {
            isPlayer = false;
            statsScript = GetComponentInParent<EnemyStats>();
            iceParticle = GameObject.Find("FireIce");
        }
        else if (gameObject.tag.Equals("Ice Enemy"))
        {
            isPlayer = false;
            statsScript = GetComponentInParent<EnemyStats>();
            fireParticle = GameObject.Find("IceFire");
        }
        if (fireParticle != null)
            fireParticle.SetActive(false);
        if (iceParticle != null)
            iceParticle.SetActive(false);
        speed = GetSpeed();
        maxSpeed = GetSpeed();
    }

    public void Update()
    {
        if (fireTimer > 0 || thawTimer > 0)
        {
            if (fireTimer > 0)
            {
                if (fireParticle != null)
                    fireParticle.SetActive(true);
                fireTimer--;
                if (fireTimer % 60 == 0)
                {
                    Damage(fireDamage);
                }
            }
            if (thawTimer > 0)
            {
                if (iceParticle != null)
                    iceParticle.SetActive(true);
                thawTimer--;
                if (thawTimer % 30 == 0 && Mathf.Clamp(GetSpeed() + thawIncrement, minFrozenSpeed, maxSpeed - thawIncrement) <= maxSpeed)
                {
                    SetSpeed(Mathf.Clamp(GetSpeed() + thawIncrement, minFrozenSpeed, maxSpeed));
                }
            }
        }
        if (fireTimer <= 0)
            if (fireParticle != null)
                fireParticle.SetActive(false);
        if (thawTimer <= 0)
            if (iceParticle != null)
                iceParticle.SetActive(false);

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
            case "thaw":
                {
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
            case "thaw":
                {
                    thawTimer = ticks;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    #region GetSetters
    //Cast get-setters (This get-set speed workaround feels silly and wrong)
    public float GetSpeed()
    {
        if (isPlayer)
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
    public void SubtractSpeed(float _speed)
    {
        if (GetSpeed() - _speed >= 0)
        {
            SetSpeed(GetSpeed() - _speed);
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
    public float GetThawIncrement()
    {
        return thawIncrement;
    }
    public float GetMinFrozenSpeed()
    {
        return minFrozenSpeed;
    }
    public int GetFireDamage()
    {
        return fireDamage;
    }
    public void SetThawIncrement(float _thawIncrement)
    {
        thawIncrement = _thawIncrement;
    }
    public void SetMinFrozenSpeed(float _minFrozenSpeed)
    {
        minFrozenSpeed = _minFrozenSpeed;
    }
    public void SetFireDamage(int _fireDamage)
    {
        fireDamage = _fireDamage;
    }
    public void Refresh()
    {
        maxSpeed = GetSpeed();
    }
    #endregion GetSet
}
