using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //The enemy we hit takes damage.
            collision.collider.GetComponentInParent<EnemyStats>().TakesDamage();
            //collision.collider.GetComponentInParent<MeshRenderer>().SetP
        }
        Destroy(gameObject);
    }
}
