using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ConditionManager : MonoBehaviour
{
    public int fireTimer = 0;
    public int thawTimer = 0;
    public int stunTimer = 0;

    [SerializeField] bool isPlayer;
    private Component statsScript;
    private Component aiScript;
    private float speed;
    private float maxSpeed;
    [SerializeField] private float minFrozenSpeed;

    //thawIncrement: How much the player's  movement speed increases on a thaw tick
    [SerializeField] private float thawIncrement;
    [SerializeField] private int fireDamage;
    GameObject fireParticle;
    GameObject iceParticle;
    private bool isPaused;

    public void Start()
    {
        if (gameObject.tag.Equals("Player"))
        {
            isPlayer = true;
            aiScript = GetComponentInParent<Player>();
            statsScript = GetComponentInParent<Player>();
            fireParticle = GameObject.Find("PlayerFire");
            iceParticle = GameObject.Find("PlayerIce");
        }
        else if (gameObject.tag.Equals("Enemy"))
        {
            isPlayer = false;
            statsScript = GetComponentInParent<EnemyStats>();
            aiScript = GetComponentInParent<EnemyAI>();
            fireParticle = GameObject.Find("RangedFire");
            iceParticle = GameObject.Find("RangedIce");
        }
        else if (gameObject.tag.Equals("BulletHell Enemy"))
        {
            isPlayer = false;
            statsScript = GetComponentInParent<EnemyStats>();
            aiScript = GetComponentInParent<BulletHellEnemy>();
            fireParticle = GameObject.Find("BulletHell Fire");
            iceParticle = GameObject.Find("BulletHell Ice");
        }
        else if (gameObject.tag.Equals("Fire Enemy"))
        {
            isPlayer = false;
            statsScript = GetComponentInParent<EnemyStats>();
            aiScript = GetComponentInParent<EnemyAI>();
            iceParticle = GameObject.Find("DragonIce");
        }
        else if (gameObject.tag.Equals("Ice Enemy"))
        {
            isPlayer = false;
            statsScript = GetComponentInParent<EnemyStats>();
            aiScript = GetComponentInParent<EnemyAI>();
            fireParticle = GameObject.Find("SpiderFire");
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
        if (!isPaused)
        {
            if (fireTimer > 0 || thawTimer > 0 || stunTimer > 0)
            {
                if (fireTimer > 0)
                {
                    if (fireParticle != null)
                        fireParticle.SetActive(true);
                    fireTimer--;
                    if (fireTimer % 60 == 0)
                        Damage(fireDamage);
                }
                if (thawTimer > 0)
                {
                    if (iceParticle != null)
                        iceParticle.SetActive(true);
                    thawTimer--;
                    if (Mathf.Clamp(GetSpeed() + thawIncrement, minFrozenSpeed, maxSpeed - thawIncrement) <= maxSpeed)
                        SetSpeed(Mathf.Clamp(GetSpeed() + thawIncrement/30f, minFrozenSpeed, maxSpeed));
                }
                if (stunTimer > 0)
                {
                    stunTimer--;
                    if (stunTimer == 0)
                        Unstun();
                }
            }
            if (fireTimer <= 0)
                if (fireParticle != null)
                    fireParticle.SetActive(false);
            if (thawTimer <= 0)
            {
                SetSpeed(maxSpeed);
                if (iceParticle != null)
                    iceParticle.SetActive(false);
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
            case "thaw":
                {
                    thawTimer += ticks;
                    break;
                }
            case "stun":
                {
                    stunTimer += ticks;
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
            case "stun":
                {
                    stunTimer = ticks;
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
    public void Refresh(float _amountToIncrease = 1)
    {
        maxSpeed += _amountToIncrease;
    }
    void OnPauseGame()
    {
        isPaused = true;
        if (isPlayer)
        {
            ((Player)aiScript).OnPauseGame();
        }
        else if (gameObject.CompareTag("BulletHell Enemy"))
        {
            ((BulletHellEnemy)aiScript).OnPauseGame();
        }
        else
        {
            ((EnemyAI)aiScript).OnPauseGame();
        }
    }
    void OnResumeGame()
    {
        isPaused = false;

    }
    #endregion GetSet

    public void Unstun()
    {
        if (gameObject.CompareTag("Player"))
        {
            ((Player)aiScript).Unstun();
        }
        else
        {
            NavMeshAgent nav = GetComponentInParent<NavMeshAgent>();
            nav.enabled = true;
            if (gameObject.CompareTag("BulletHell Enemy"))
            {
                ((BulletHellEnemy)aiScript).Unstun();
            }
            else
            {
                ((EnemyAI)aiScript).Unstun();
            }
        }
    }
}