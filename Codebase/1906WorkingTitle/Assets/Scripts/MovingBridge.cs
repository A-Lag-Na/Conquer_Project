using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBridge : MonoBehaviour
{
    public float bridgeVelocity = 5;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3 (bridgeVelocity,0,0);
        
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "BridgeCollider")
            rb.velocity = -rb.velocity;
        if (other.gameObject.tag == "Player")
            other.gameObject.transform.SetParent(gameObject.transform);
    }
}
