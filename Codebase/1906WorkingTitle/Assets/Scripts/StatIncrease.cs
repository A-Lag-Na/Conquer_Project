using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatIncrease : MonoBehaviour
{
    [SerializeField] public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<Player>().AddHealth(10);
        }
    }
}
