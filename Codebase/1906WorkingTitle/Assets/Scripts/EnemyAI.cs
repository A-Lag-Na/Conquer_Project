using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float attackRate;
    //[SerializeField] private int attackMax = 120;

    private float bulletSpeed;
    int bulletDamage;

    //If this enemy's attack behavior is enabled or not.
    [SerializeField] bool attackEnabled = true;

    //What projectile the enemy shoots
    [SerializeField] GameObject projectile = null;
    [SerializeField] GameObject projectilePos = null;

    [SerializeField] AudioClip fire = null;

    Animator anim;
    NavMeshAgent agent;
    GameObject player;
    AudioSource source;

    public bool stunned;
    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        EnemyStats temp = GetComponent<EnemyStats>();
        attackRate = temp.GetAttackRate();
        bulletSpeed = temp.GetBulletSpeed();
        bulletDamage = GetComponent<EnemyStats>().GetDamage();
        source = GetComponentInParent<AudioSource>();
        source.enabled = true;
    }

    IEnumerator attack()
    {
        attackEnabled = false;
        Quaternion temp = projectile.transform.rotation;
        temp.x = 0;
        temp.z = 0;
        GameObject clone = Instantiate(projectile, projectilePos.transform.position, temp);
        clone.GetComponent<CollisionScript>().bulletDamage = bulletDamage;
        clone.gameObject.layer = 12;
        clone.SetActive(true);
        source.PlayOneShot(fire);
        clone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        if(anim != null && !stunned)
        {
            anim.SetTrigger("Attack");
        }
        yield return new WaitForSeconds(attackRate);
        attackEnabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (!paused && !stunned)
        {
            agent.SetDestination(player.transform.position);
            if (agent.remainingDistance < agent.stoppingDistance || GetComponent<NavMeshAgent>().speed <= 0)
            {
                Vector3 targetPosition = player.transform.position;
                Vector3 relativePosition = targetPosition - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePosition);
                // Lock the rotation around X and Z Axes
                rotation.x = 0.0f;
                rotation.z = 0.0f;
                // Change the player's tranform's rotation to the rotation Quaternion
                agent.transform.rotation = rotation;
            }

            if (attackEnabled)
            {
                StartCoroutine(attack());
            }
        }
    }

    #region stuff
    //Call this function after you change either attackRate or bulletSpeed in EnemyStats while the enemy is active.
    public void RefreshStats()
    {
        EnemyStats temp = GetComponent<EnemyStats>();
        attackRate = temp.GetAttackRate();
        bulletSpeed = temp.GetBulletSpeed();
    }
    public void OnPauseGame()
    {
        paused = true;
    }
    public void OnResumeGame()
    {
        paused = false;
    }
    public void Stun()
    {
        stunned = true;
    }
    public void Unstun()
    {
        stunned = false;
    }
    #endregion stuff
}