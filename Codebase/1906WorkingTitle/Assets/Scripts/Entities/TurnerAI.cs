using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurnerAI : MonoBehaviour
{
    CharacterController characterController = null;
    Vector3 moveDirection = Vector3.zero;
    float enemyY = 0.0f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        enemyY = transform.position.y;
    }

    private void Update()
    {
        moveDirection = transform.forward / 5;
        characterController.Move(moveDirection);
        transform.position = new Vector3(transform.position.x, enemyY, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.Rotate(0, 90, 0);
    }
}

