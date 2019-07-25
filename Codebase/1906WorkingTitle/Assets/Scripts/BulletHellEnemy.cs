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
    float lastTimeFired;

    NavMeshAgent agent;
    GameObject player;
    private bool paused;
    int bulletDamage;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        rotationSpeed = 120;
        bulletDamage = GetComponent<EnemyStats>().GetDamage();
    }

    public void ShootBullet()
    {
        //Instantiate a projectile and set the projectile's velocity towards the forward vector of the player transform
        if (Time.time > lastTimeFired + attackRate)
        {
            if (Time.time >= 15)
            {
                GameObject clone4 = Instantiate(projectile, transform.position, transform.rotation);
                clone4.GetComponent<CollisionScript>().bulletDamage = bulletDamage;
                clone4.gameObject.layer = 12;
                clone4.gameObject.SetActive(true);
                clone4.GetComponent<TrailRenderer>().startColor = Color.red;
                clone4.GetComponent<TrailRenderer>().endColor = Color.white;
                clone4.GetComponent<Rigidbody>().velocity = transform.right * -bulletSpeed;
                GameObject clone3 = Instantiate(projectile, transform.position, transform.rotation);
                clone3.GetComponent<CollisionScript>().bulletDamage = bulletDamage;
                clone3.gameObject.layer = 12;
                clone3.gameObject.SetActive(true);
                clone3.GetComponent<TrailRenderer>().startColor = Color.red;
                clone3.GetComponent<TrailRenderer>().endColor = Color.white;
                clone3.GetComponent<Rigidbody>().velocity = transform.right * bulletSpeed;
                GameObject clone2 = Instantiate(projectile, transform.position, transform.rotation);
                clone2.GetComponent<CollisionScript>().bulletDamage = bulletDamage;
                clone2.gameObject.layer = 12;
                clone2.gameObject.SetActive(true);
                clone2.GetComponent<TrailRenderer>().startColor = Color.red;
                clone2.GetComponent<TrailRenderer>().endColor = Color.white;
                clone2.GetComponent<Rigidbody>().velocity = transform.forward * -bulletSpeed;
            }
            else if (Time.time >= 10)
            {
                GameObject clone3 = Instantiate(projectile, transform.position, transform.rotation);
                clone3.GetComponent<CollisionScript>().bulletDamage = bulletDamage;
                clone3.gameObject.layer = 12;
                clone3.gameObject.SetActive(true);
                clone3.GetComponent<TrailRenderer>().startColor = Color.red;
                clone3.GetComponent<TrailRenderer>().endColor = Color.white;
                clone3.GetComponent<Rigidbody>().velocity = transform.right * bulletSpeed;
                GameObject clone2 = Instantiate(projectile, transform.position, transform.rotation);
                clone2.GetComponent<CollisionScript>().bulletDamage = bulletDamage;
                clone2.gameObject.layer = 12;
                clone2.gameObject.SetActive(true);
                clone2.GetComponent<TrailRenderer>().startColor = Color.red;
                clone2.GetComponent<TrailRenderer>().endColor = Color.white;
                clone2.GetComponent<Rigidbody>().velocity = transform.forward * -bulletSpeed;
            }
            else if (Time.time >= 5)
            {
                GameObject clone2 = Instantiate(projectile, transform.position, transform.rotation);
                clone2.GetComponent<CollisionScript>().bulletDamage = bulletDamage;
                clone2.gameObject.layer = 12;
                clone2.gameObject.SetActive(true);
                clone2.GetComponent<TrailRenderer>().startColor = Color.red;
                clone2.GetComponent<TrailRenderer>().endColor = Color.white;
                clone2.GetComponent<Rigidbody>().velocity = transform.forward * -bulletSpeed;
            }
            GameObject clone = Instantiate(projectile, transform.position, transform.rotation);
            clone.GetComponent<CollisionScript>().bulletDamage = bulletDamage;
            clone.gameObject.layer = 12;
            clone.gameObject.SetActive(true);
            clone.GetComponent<TrailRenderer>().startColor = Color.red;
            clone.GetComponent<TrailRenderer>().endColor = Color.white;
            clone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            lastTimeFired = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            agent.SetDestination(player.transform.position);
            agent.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            ShootBullet();
        }
    }
    void OnPauseGame()
    {
        paused = true;
    }

    void OnResumeGame()
    {
        paused = false;
    }
}
