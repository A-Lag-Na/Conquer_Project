﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{

    Animator chestAnim; //Animator for the chest;
    GameObject player; //Player Object
    Player playerScript;//Player Script
    CapsuleCollider capsuleCollider; // Capsule Collider
    GameObject chestParticles = null;

    // Use this for initialization
    void Awake()
    {
        //get the Animator component from the chest;
        chestAnim = GetComponent<Animator>();
        //get the Player Component 
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponentInParent<Player>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        if(GetComponentInChildren<ParticleSystem>().gameObject != null)
        chestParticles = GetComponentInChildren<ParticleSystem>().gameObject;
        chestParticles.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //play open animation;  
            chestAnim.SetTrigger("open");
            //Gets Random Coin ammount
            System.Random rand = new System.Random();
            int seed = rand.Next(1, 50);
            //Player gets treasure
            playerScript.AddCoins(seed);
            //Not allow the player to cash out the chest again
            capsuleCollider.enabled = false;
            StartCoroutine(ShowParticle());
        }
    }

    IEnumerator ShowParticle()
    {
        chestParticles.SetActive(true);
        yield return new WaitForSeconds(1);
        chestParticles.SetActive(false);
    }
}