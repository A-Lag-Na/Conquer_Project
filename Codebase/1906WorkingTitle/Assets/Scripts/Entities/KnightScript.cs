using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : MonoBehaviour
{
    
    [SerializeField] Vector3 initialPos = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField] Vector3 movePos = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField] GameObject knight = null;
    [SerializeField] private GameObject[] popUps = null;
    [SerializeField] GameObject image = null;
    [SerializeField] GameObject continuePrompt = null;

    GameObject player;
    Player playerScript;
    Animator anim;

    bool walk = false;
    bool enter = false;
    float speed = 1.5f;
    int popUpIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        knight.transform.position = initialPos;
        playerScript = player.GetComponent<Player>();
        anim = knight.GetComponent<Animator>();
    }

    private void Update()
    {
        //Turn on and off text depending on which one is supposed to be shown
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
                popUps[i].SetActive(true);
            else
                popUps[i].SetActive(false);

            if (popUpIndex == popUps.Length)
                EndSequence();
        }
        
       

        //If the player can press enter "Press enter to continue" The continue prompt text will also be turned on
        if (enter)
        {
            continuePrompt.SetActive(true);
            TextConditions();
        }

        //NPC walks toward the player
        if (walk)
        {
            knight.transform.position = Vector3.MoveTowards(knight.transform.position, movePos, speed * Time.deltaTime);

            if (knight.transform.position == movePos)
            {
              walk = false;
              anim.SetTrigger("Idle");
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //Player will be stopped and NPC Knight will walk towards him
        if(other.tag == "Player")
        {
            knight.SetActive(true);
            image.SetActive(true);
            playerScript.isStunned = true;   
            walk = true;
            anim.SetTrigger("Walk");
            //Wait time for "Press Enter to continue" to pop up
            StartCoroutine(TextWait());
        }
    }

    //If Enter pressed the index will go up one and enter value will be restored to false
    private void TextConditions()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            enter = false;
            popUpIndex++;
            //Wait time for "Press Enter to continue" to pop up
            StartCoroutine(TextWait());
        }  
    }
    //Wait for player to read
    IEnumerator TextWait()
    {
        continuePrompt.SetActive(false);
        yield return new WaitForSeconds(2);
        enter = true;
    }

    //Whwn all text has gone through end the sequence
    private void EndSequence()
    {
        image.SetActive(false);
        continuePrompt.SetActive(false);
        StopAllCoroutines();
        playerScript.isStunned = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    


}
