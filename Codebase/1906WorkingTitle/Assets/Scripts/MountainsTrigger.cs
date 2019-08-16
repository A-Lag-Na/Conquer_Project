using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainsTrigger : MonoBehaviour
{
    #region MountainsTriggerProperties
    //Obstacle
    GameObject iceBarrier = null;
    //Particle effect for the fire
    GameObject fireOne = null;
    #endregion
    
    void Start()
    {
        //Getting the GO in the script
        iceBarrier = GameObject.Find("Mountain Ice Barrier");
        fireOne = iceBarrier.transform.GetChild(1).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        // the coroutine wont start unless triggered by the fire bullet, ice bullet and normal bullet will not have an effect on the barrier
        if (other.tag == "Fire Bullet")
            StartCoroutine(BurnPath());
    }

    //Coroutine to melt the ice
    IEnumerator BurnPath()
    {
        fireOne.SetActive(true);
        yield return new WaitForSeconds(2);
        iceBarrier.SetActive(false);
        fireOne.SetActive(false);
    }
}
