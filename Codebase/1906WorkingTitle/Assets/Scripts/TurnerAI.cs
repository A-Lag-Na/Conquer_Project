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

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(transform.forward * 12);
    }
    

    private void OnCollisionStay(Collision collisionInfo)
    {
        Collider col = collisionInfo.collider;
        GameObject obj = col.gameObject;
        if (obj.CompareTag("Player") || obj.CompareTag("Untagged"))
        {
            agent.SetDestination(transform.right * 12);
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

