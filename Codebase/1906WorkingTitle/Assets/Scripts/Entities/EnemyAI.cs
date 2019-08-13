using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    #region EnemyStats
    [SerializeField] private float attackRate = 0.0f;
    private float bulletSpeed = 0.0f;
    int bulletDamage = 0;
    public bool isStunned = false;
    private bool isPaused, inLove = false;
    bool attackEnabled = true;
    #endregion

    //If this enemy's attack behavior is enabled or not.

    #region UnityComponents
    //What projectile the enemy shoots
    [SerializeField] GameObject projectile = null;
    [SerializeField] GameObject projectilePos = null;

    [SerializeField] AudioClip fire = null;

    Animator anim = null;
    NavMeshAgent agent = null;
    GameObject player = null;
    GameObject target = null;
    AudioSource source = null;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        EnemyStats enemyStats = GetComponent<EnemyStats>();
        attackRate = enemyStats.GetAttackRate();
        bulletSpeed = enemyStats.GetBulletSpeed();
        bulletDamage = GetComponent<EnemyStats>().GetDamage();
        source = GetComponentInParent<AudioSource>();
        source.enabled = true;
        target = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused && !isStunned)
        {
            if (inLove && player != null && target == null)
                SetTarget();
            agent.SetDestination(target.transform.position);
            if (agent.remainingDistance < agent.stoppingDistance || GetComponent<NavMeshAgent>().speed <= 0)
            {
                Vector3 targetPosition = target.transform.position;
                Vector3 relativePosition = targetPosition - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePosition);
                // Lock the rotation around X and Z Axes
                rotation.x = 0.0f;
                rotation.z = 0.0f;
                // Change the enemy's tranform's rotation to the rotation Quaternion
                agent.transform.rotation = rotation;
            }

            if (attackEnabled)
                StartCoroutine(EnemyAttack());
        }
    }

    #region EnemyFunctions
    //Call this function after you change either attackRate or bulletSpeed in EnemyStats while the enemy is active.
    public void RefreshStats()
    {
        EnemyStats enemyStats = GetComponent<EnemyStats>();
        attackRate = enemyStats.GetAttackRate();
        bulletSpeed = enemyStats.GetBulletSpeed();
    }

    public void OnPauseGame()
    {
        isPaused = true;
    }

    public void OnResumeGame()
    {
        isPaused = false;
    }

    public void Stun()
    {
        isStunned = true;
    }

    public void Unstun()
    {
        isStunned = false;
    }

    public IEnumerator FallInLove(float time)
    {
        SetTarget();
        inLove = true;
        yield return new WaitForSeconds(time);
        inLove = false;
        target = player;
    }

    private void SetTarget()
    {
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = float.MaxValue;
        Vector3 position = transform.position;
        foreach (GameObject gameobject in gameObjects)
        {
            if (gameobject != gameObject)
            {
                float curDistance = Vector3.Distance(position, gameobject.transform.position);
                if (curDistance < distance)
                {
                    closest = gameobject;
                    distance = curDistance;
                }
            }
        }
        target = closest;
    }

    public bool InLove()
    {
        return inLove;
    }

    IEnumerator EnemyAttack()
    {
        attackEnabled = false;
        GameObject clone = Instantiate(projectile, projectilePos.transform.position, projectile.transform.rotation);
        clone.GetComponent<CollisionScript>().bulletDamage = bulletDamage;
        clone.gameObject.layer = 12;
        clone.SetActive(true);
        source.PlayOneShot(fire);
        clone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        if (anim != null && !isStunned)
            anim.SetTrigger("Attack");
        yield return new WaitForSeconds(attackRate);
        attackEnabled = true;
    }
    #endregion
}