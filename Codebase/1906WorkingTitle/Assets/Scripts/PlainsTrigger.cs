using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainsTrigger : MonoBehaviour
{
    [SerializeField] GameObject cactusBarrier;
    [SerializeField] GameObject iceEffect;

    // Start is called before the first frame update
    void Start()
    {
        cactusBarrier = GameObject.Find("Desert Cactus Blockade");
        iceEffect = cactusBarrier.transform.GetChild(1).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ice Bullet")
        {
            StartCoroutine(FreezePath());
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
