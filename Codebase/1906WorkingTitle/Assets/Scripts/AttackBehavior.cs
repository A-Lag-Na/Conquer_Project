using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackBehavior : MonoBehaviour
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
        GameObject clone = Instantiate(projectile, transform.position, projectile.transform.rotation);
        clone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        yield return new WaitForSeconds(attackRate);
        attackEnabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);

        if (attackEnabled)
            StartCoroutine(attack());
    }
}
