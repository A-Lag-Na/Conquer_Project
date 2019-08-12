using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapDemo : MonoBehaviour {

    

    public Animator spikeTrapAnim; //Animator for the SpikeTrap;
    bool onTrap = false;
    GameObject player; //Player Object
    Player playerScript;//Player Script

    // Use this for initialization
    void Awake()
    {
        //get the Player Component 
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponentInParent<Player>();
        //get the Animator component from the trap;
        spikeTrapAnim = GetComponent<Animator>();
        //start opening and closing the trap for demo purposes;
        StartCoroutine(OpenCloseTrap());
       
    }


    IEnumerator OpenCloseTrap()
    {
        //play open animation;
        spikeTrapAnim.SetTrigger("open");
        if (onTrap)
        {
            playerScript.TakeDamage(5);
        }
        //wait 2 seconds;
        yield return new WaitForSeconds(2);
        //play close animation;
        spikeTrapAnim.SetTrigger("close");
        //wait 2 seconds;
        yield return new WaitForSeconds(2);
        //Do it again;
        StartCoroutine(OpenCloseTrap());

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("olo");
        if (other.tag == "Player")
        {
            Debug.Log("Player Enter");
            onTrap = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player Leave");
            onTrap = false;
        }
    }


}