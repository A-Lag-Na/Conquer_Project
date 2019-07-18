using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //Counts frames between attacks
    private int attackTimer;

    [SerializeField] private int attackRate = 1;
    //[SerializeField] private int attackMax = 120;

    [SerializeField] private int bulletSpeed = 2;

    //List of different attacks this enemy can use
    [SerializeField] List<string> attackTypes;

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
        //attackTimer = Random.Range(attackMin, attackMax); 
    }

    IEnumerator attack()
    {
        attackEnabled = false;
        Quaternion temp = projectile.transform.rotation;
        temp.x = 0;
        temp.z = 0;
        GameObject clone = Instantiate(projectile, transform.position, temp);
        clone.gameObject.tag = "Enemy Bullet";
        clone.gameObject.layer = 12;
        clone.SetActive(true);

        //clone.GetComponent<TrailRenderer>().startColor = Color.cyan;
        //clone.GetComponent<TrailRenderer>().endColor = Color.white;

        clone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        yield return new WaitForSeconds(attackRate);
        attackEnabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
        if(agent.remainingDistance < agent.stoppingDistance)
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
