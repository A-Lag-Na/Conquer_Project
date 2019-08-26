using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertTrigger : MonoBehaviour
{
    [SerializeField] private GameObject cactusBarrier = null;
    private GameObject iceEffect;
    bool active;
    
    void Start()
    {
        iceEffect = cactusBarrier.transform.GetChild(1).gameObject;
        active = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ice Bullet" && !active)
        {
            StartCoroutine(FreezePath());
            active = true;
        }
    }

    IEnumerator FreezePath()
    {
        iceEffect.SetActive(true);
        yield return new WaitForSeconds(2);
        cactusBarrier.SetActive(false);
        iceEffect.SetActive(false);
    }
}
