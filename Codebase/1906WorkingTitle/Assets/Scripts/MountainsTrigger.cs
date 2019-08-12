using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainsTrigger : MonoBehaviour
{
    GameObject iceBarrier;
    GameObject fireOne;
    GameObject fireTwo;
    // Start is called before the first frame update
    void Start()
    {
        iceBarrier = GameObject.Find("Mountain Ice Barrier");
        fireOne = iceBarrier.transform.GetChild(1).gameObject;
        fireTwo = iceBarrier.transform.GetChild(2).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fire Bullet")
        {
            StartCoroutine(BurnPath());
        }

    }

    IEnumerator BurnPath()
    {
        fireOne.SetActive(true);
        fireTwo.SetActive(true);
        yield return new WaitForSeconds(2);
        iceBarrier.SetActive(false);
        fireOne.SetActive(false);
        fireTwo.SetActive(false);
       
    }

}
