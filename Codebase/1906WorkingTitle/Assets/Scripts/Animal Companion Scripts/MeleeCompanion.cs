using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCompanion : MonoBehaviour
{
    GameObject player = null;
    GameObject target = null;
    Player playerStats = null;
    Vector3 animalVelocity = Vector3.zero;
    Vector3 playerPositionOffset = Vector3.zero;
    float lastTimeAttacked = 0.0f;
    bool hasAttacked = false;

    // Update is called once per frame
    void Update()
    {
        playerPositionOffset = new Vector3(player.transform.position.x - 3, player.transform.position.y, player.transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        // Lock the rotation around X and Z Axes
        rotation.x = 0.0f;
        rotation.z = 0.0f;
        // Change the companion's tranform's rotation to the rotation Quaternion
        transform.rotation = rotation;
        target = GameObject.FindGameObjectWithTag("Enemy");
        if (target == null)
            target = GameObject.FindGameObjectWithTag("BulletHell Enemy");
        if (target == null)
            transform.position = Vector3.SmoothDamp(transform.position, playerPositionOffset, ref animalVelocity, 0.5f);
        if (target != null)
        {
            if (hasAttacked == true || Vector3.Distance(target.transform.position, player.transform.position) > 10)
                transform.position = Vector3.SmoothDamp(transform.position, playerPositionOffset, ref animalVelocity, 0.5f);
            if (Vector3.Distance(target.transform.position, player.transform.position) <= 10 && hasAttacked == false)
                transform.position = Vector3.SmoothDamp(transform.position, target.transform.position, ref animalVelocity, 0.5f);
        }
        if (Time.time > lastTimeAttacked + 5)
            hasAttacked = false;
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<Player>();
        transform.position = playerPositionOffset;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (target != null)
            target.GetComponent<EnemyStats>().TakeDamage();
        hasAttacked = true;
        lastTimeAttacked = Time.time;
    }
}
