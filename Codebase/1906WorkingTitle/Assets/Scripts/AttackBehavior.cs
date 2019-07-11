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

    // Start is called before the first frame update
    void Start()
    {
        attackTimer = Random.Range(attackMin, attackMax);
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer--;
        if(attackEnabled && attackTimer <= 0)
        {
            //Chooses and stores a random attack from the available attack list.
            string temp = attackTypes[Random.Range(0, attackTypes.Count - 1)];

            if(temp.Equals("bullet"))
            {

            }

            else if(temp.Equals("melee"))
            {

            }
        }
    }
}
