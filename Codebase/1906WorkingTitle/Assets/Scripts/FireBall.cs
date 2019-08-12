using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] GameObject childType = null;
    [SerializeField] float explosionDamage = 2;
    private void OnCollisionEnter(Collision collision)
    {
        if (childType != null)
        {
            GameObject child = Instantiate(childType, transform.position, childType.transform.rotation);
            child.GetComponent<CreepManager>().SetDamage(explosionDamage/60f);
        }
    }
}
