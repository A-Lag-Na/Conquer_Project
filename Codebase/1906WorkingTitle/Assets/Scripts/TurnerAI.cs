using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurnerAI : MonoBehaviour
{
    public bool isStunned;
    private bool isPaused;

    Animator anim = null;
    NavMeshAgent agent = null;
    AudioSource source = null;
    Rigidbody rb = null;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        agent.destination = transform.forward * 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider col = collision.collider;
        GameObject obj = col.gameObject;
        if(obj.CompareTag("Player") || obj.CompareTag("Default"))
        {
            agent.destination = transform.right * 10;
        }
    }

    #region EnemyFunctions
    //Call this function after you change either attackRate or bulletSpeed in EnemyStats while the enemy is active.
    public void RefreshStats()
    {
        EnemyStats temp = GetComponent<EnemyStats>();
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
    #endregion
}

