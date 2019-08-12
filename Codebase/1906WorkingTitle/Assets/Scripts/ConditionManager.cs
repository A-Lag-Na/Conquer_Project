using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ConditionManager : MonoBehaviour
{
    private int fireTimer = 0;
    private int thawTimer = 0;
    private int stunTimer = 0;
    private int auraTimer = 0;

    [SerializeField] bool isPlayer;
    private Component statsScript;
    private Component aiScript;

    private Renderer enemyRender = null;
    private float speed;
    private float maxSpeed;
    private float minFrozenSpeed;

    //thawIncrement: How much the player's  movement speed increases on a thaw tick
    private float thawIncrement;
    private float fireDamage;
    private float auraDamage;

    GameObject fireParticle;
    GameObject iceParticle;

    private bool isPaused;

    public void Start()
    {
        string tag = gameObject.tag;
        if (tag.Equals("Player"))
        {
            isPlayer = true;
            aiScript = GetComponentInParent<Player>();
            statsScript = GetComponentInParent<Player>();
            fireParticle = GameObject.Find("PlayerFire");
            iceParticle = GameObject.Find("PlayerIce");
        }
        else
        { 
            if(tag.Equals("Enemy"))
            {
                aiScript = GetComponentInParent<EnemyAI>();
            }
            if(tag.Equals("BulletHell Enemy"))
            {
                aiScript = GetComponentInParent<BulletHellEnemy>();
            }
            isPlayer = false;
            statsScript = GetComponentInParent<EnemyStats>();
            enemyRender = ((EnemyStats)statsScript).GetRenderer();
            fireParticle = GameObject.Find("Fire");
            iceParticle = GameObject.Find("Ice");
        }
        if (fireParticle != null)
            fireParticle.SetActive(false);
        if (iceParticle != null)
            iceParticle.SetActive(false);
        if(auraDamage == 0f)
        {
            auraDamage = .017f;
        }
        speed = GetSpeed();
        maxSpeed = GetSpeed();
    }

    public void Update()
    {
        if (!isPaused)
        {
            if (fireTimer > 0 || thawTimer > 0 || stunTimer > 0 || auraTimer > 0)
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
                if(auraTimer > 0)
                {
                    if(!tag.Equals("Player"))
                        enemyRender.material.color = Color.Lerp(enemyRender.material.color, Color.black, .08f);
                    Damage(auraDamage);
                    auraTimer--;
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

    #region TimerManagement
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
            case "aura":
                {
                    auraTimer += ticks;
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
    #endregion

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
    public void Damage(float _damage)
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
    public float GetFireDamage()
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
    public void SetFireDamage(float _fireDamage)
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
    #endregion GetSet
}