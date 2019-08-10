using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostCompanion : MonoBehaviour
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
        Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        // Lock the rotation around X and Z Axes
        rotation.x = 0.0f;
        rotation.z = 0.0f;
        // Change the companion's tranform's rotation to the rotation Quaternion
        transform.rotation = rotation;
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<Player>();
        transform.position = playerPositionOffset;
        playerStats.ModifySpeed(3);
    }

    private void OnDisable()
    {
        playerStats.ModifySpeed(-3);
    }
}
