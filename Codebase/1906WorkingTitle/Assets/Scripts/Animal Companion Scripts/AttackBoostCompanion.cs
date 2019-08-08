using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoostCompanion : MonoBehaviour
{
    GameObject player = null;
    Player playerStats = null;
    Vector3 animalVelocity = Vector3.zero;
    Vector3 playerPositionOffset = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        playerPositionOffset = new Vector3(player.transform.position.x - 3, player.transform.position.y, player.transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, playerPositionOffset, ref animalVelocity, 0.5f);
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<Player>();
        transform.position = playerPositionOffset;
        playerStats.ModifyDamage(1);
    }

    private void OnDisable()
    {
        playerStats.ModifyDamage(-1);
    }
}
