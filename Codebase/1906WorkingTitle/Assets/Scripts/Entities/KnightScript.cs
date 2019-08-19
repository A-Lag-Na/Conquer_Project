using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : MonoBehaviour
{
    
    [SerializeField] Vector3 initialPos = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField] Vector3 movePos = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField] GameObject knight = null;
    [SerializeField] private GameObject[] popUps = null;
    [SerializeField] GameObject image;
    [SerializeField] GameObject continuePrompt;

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
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
                popUps[i].SetActive(true);
            else
                popUps[i].SetActive(false);

            if(i++ >= popUps.Length)
                EndSequence();
        }

        StartCoroutine(TextWait());

        if (enter)
        {
            continuePrompt.SetActive(true);
            TextConditions();
        }

        if (walk)
        {
            knight.transform.position = Vector3.MoveTowards(knight.transform.position, movePos, speed * Time.deltaTime);

            if (knight.transform.position == movePos)
            {
              walk = false;
              anim.SetTrigger("Idle");
              movePos = initialPos;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            image.SetActive(true);
            playerScript.isStunned = true;
            walk = true;
            anim.SetTrigger("Walk");
        }
    }

    private void TextConditions()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            enter = false;
            popUpIndex++;
        }  
    }

    IEnumerator TextWait()
    {
        yield return new WaitForSeconds(2);
        enter = true;
    }

    private void EndSequence()
    {
        image.SetActive(false);
        popUps[popUpIndex].SetActive(false);
        walk = true;
        playerScript.isStunned = false;
    }
    


}
