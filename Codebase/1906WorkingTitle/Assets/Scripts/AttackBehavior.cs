using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    //Counts frames between attacks
    private int attackTimer;
    [SerializeField] private int attackMin = 120;
    [SerializeField] private int attackMax = 120;

    //List of different attacks this enemy can use
    [SerializeField] List<string> attackTypes;

    //If this enemy's attack behavior is enabled or not.
    [SerializeField] bool attackEnabled = true;

    //What projectile the enemy shoots
    [SerializeField] GameObject projectile;

    private Transform trans;

    // Start is called before the first frame update
    void Start()
    {
        attackTimer = Random.Range(attackMin, attackMax);
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer--;
        if(attackEnabled && attackTimer <= 0)
        {
            //Chooses and stores a random attack from the available attack list.
            string temp = attackTypes[Random.Range(0, attackTypes.Count)];

            if(temp.Equals("bullet"))
            {
                Vector3 vect = Vector3.MoveTowards(GameObject.FindGameObjectWithTag("Player").transform.position, GetComponentInParent<Transform>().position, 99);
                GameObject clone = Instantiate(projectile, transform.position, Quaternion.identity);
                clone.gameObject.SetActive(true);
                clone.GetComponent<Rigidbody>().velocity = trans.TransformDirection(vect * 10);
            }

            else if(temp.Equals("melee"))
            {

            }

            attackTimer = Random.Range(attackMin, attackMax);
        }
    }
}
