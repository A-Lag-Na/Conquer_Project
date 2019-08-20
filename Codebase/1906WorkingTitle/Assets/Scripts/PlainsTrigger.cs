using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainsTrigger : MonoBehaviour
{
    [SerializeField] private GameObject cactusBarrier;
    [SerializeField] private GameObject iceEffect;
    
    void Start()
    {
        cactusBarrier = GameObject.Find("Desert Cactus Blockade");
        iceEffect = cactusBarrier.transform.GetChild(1).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ice Bullet")
            StartCoroutine(FreezePath());
    }

    IEnumerator FreezePath()
    {
        iceEffect.SetActive(true);
        yield return new WaitForSeconds(2);
        cactusBarrier.SetActive(false);
        iceEffect.SetActive(false);
    }
}
