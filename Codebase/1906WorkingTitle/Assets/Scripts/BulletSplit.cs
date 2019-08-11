using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSplit : MonoBehaviour
{
    [SerializeField] private float boltSpeed = 5;
    [SerializeField] private int numChildren = 0;
    [SerializeField] GameObject bolts = null;
    private int layer = 0;

    private void Start()
    {
        layer = gameObject.layer;
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.collider.tag;
        if(tag.Equals("Enemy") || tag.Equals("BulletHell Enemy"))
        {

            List<Vector3> EnemyPositions = new List<Vector3>();
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                EnemyPositions.Add(enemy.GetComponent<Transform>().position);
            }

            if(EnemyPositions.Count > 0)
            {
                Collider collider = collision.collider;
                GameObject hit = collider.gameObject;

                //Sets enemy to a new layer so they do not collide with bolts.
                //hit.layer = 10;


                Vector3 currentPosition = hit.GetComponent<Transform>().position;
                EnemyPositions.Remove(currentPosition);
                
                for (int i = 0; i < numChildren; i++)
                {
                    if (EnemyPositions.Count > 0 && EnemyPositions[i] != null)
                    {
                        Vector3 target = EnemyPositions[i];
                        GameObject bolt = Instantiate(bolts, collider.transform.position, bolts.transform.rotation);
                        bolt.layer = layer;
                        Rigidbody rb = bolt.GetComponent<Rigidbody>();
                        rb.velocity = Vector3.Normalize((new Vector3 (target.x - currentPosition.x, 0.0f, target.z - currentPosition.z))) * boltSpeed;
                        //Zap sound effect could go here
                    }
                }
            }
        }
    }
}
