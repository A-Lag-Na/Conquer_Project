using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BulletHellEnemy : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private float attackRate = 1;
    //[SerializeField] private int attackMax = 120;

    [SerializeField] private int bulletSpeed = 2;

    //If this enemy's attack behavior is enabled or not.
    [SerializeField] bool attackEnabled = true;

    //What projectile the enemy shoots
    [SerializeField] GameObject projectile;

    [SerializeField] float rotationRate = 2;
    #endregion

    //Counts frames between attacks
    private int attackTimer;

    [SerializeField] int rotationSpeed;
    int rotationCap;
    bool rotateEnabled = true;

    NavMeshAgent agent;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        rotationSpeed = 60;
        rotationCap = 1200;
    }

    IEnumerator attack()
    {
        attackEnabled = false;
        GameObject clone = Instantiate(projectile, transform.position, projectile.transform.rotation);
        clone.gameObject.layer = 12;
        clone.SetActive(true);

        clone.GetComponent<TrailRenderer>().startColor = Color.red;
        clone.GetComponent<TrailRenderer>().endColor = Color.white;

        clone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;

        yield return new WaitForSeconds(attackRate);
        attackEnabled = true;
    }

    IEnumerator RotateFaster()
    {
        rotateEnabled = false;
        if (rotationSpeed < rotationCap)
            rotationSpeed += 60;
        yield return new WaitForSeconds(rotationRate);
        rotateEnabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
        agent.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        if (rotateEnabled)
            StartCoroutine(RotateFaster());
        if (attackEnabled)
            StartCoroutine(attack());
    }
}
