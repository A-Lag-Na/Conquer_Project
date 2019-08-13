﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainsTrigger : MonoBehaviour
{
    //Obstacle
    GameObject iceBarrier;
    //Particle effect for the fire
    GameObject fireOne;
    GameObject fireTwo;
    // Start is called before the first frame update
    void Start()
    {
        //Getting the GO in the script
        iceBarrier = GameObject.Find("Mountain Ice Barrier");
        fireOne = iceBarrier.transform.GetChild(1).gameObject;
        fireTwo = iceBarrier.transform.GetChild(2).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        // the coroutine wont start unless triggered by the fire bullet, ice bullet and normal bullet will not have an effect on the barrier
        if (other.tag == "Fire Bullet")
        {
            StartCoroutine(BurnPath());
        }

    }

    //Coroutine to melt the ice
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
