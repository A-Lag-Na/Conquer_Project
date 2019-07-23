using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float attackRate;
    //[SerializeField] private int attackMax = 120;

    [SerializeField] private float bulletSpeed;

    //If this enemy's attack behavior is enabled or not.
    [SerializeField] bool attackEnabled = true;

    //What projectile the enemy shoots
    [SerializeField] GameObject projectile;

    NavMeshAgent agent;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        EnemyStats temp = GetComponent<EnemyStats>();
        attackRate = temp.GetAttackRate();
        bulletSpeed = temp.GetBulletSpeed();
    }

    IEnumerator attack()
    {
        attackEnabled = false;
        Quaternion temp = projectile.transform.rotation;
        temp.x = 0;
        temp.z = 0;
        GameObject clone = Instantiate(projectile, transform.position, temp);
        clone.GetComponent<CollisionScript>().bulletDamage = GetComponent<EnemyStats>().GetDamage();
        clone.gameObject.layer = 12;
        clone.SetActive(true);

        clone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        yield return new WaitForSeconds(attackRate);
        attackEnabled = true;
    }


    // Update is called once per frame
    void Update()
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

    //Call this function after you change either attackRate or bulletSpeed in EnemyStats while the enemy is active.
    public void RefreshStats()
    {
        EnemyStats temp = GetComponent<EnemyStats>();
        attackRate = temp.GetAttackRate();
        bulletSpeed = temp.GetBulletSpeed();
    }
}
