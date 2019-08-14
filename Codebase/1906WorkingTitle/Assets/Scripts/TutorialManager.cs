using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    private Player playerScript = null;
    [SerializeField] GameObject spawner = null;
    private SpawnScript spawnScript = null;
    [SerializeField] GameObject[] popUps = null;
    private GameObject door;


    int popUpIndex = 0;

    public void Start()
    {
        spawnScript = spawner.GetComponent<SpawnScript>();
        playerScript = player.GetComponent<Player>();
        door = GameObject.FindGameObjectWithTag("Door");
        spawner.SetActive(false);
        spawnScript.SetEnabled(false);
    }

    private void Update()
    {
        //Keeps track of what popup is on and which ones are off
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
                popUps[i].SetActive(true);
            else
                popUps[i].SetActive(false);
        }
       
        //Movement 
        if (popUpIndex == 0)
        {
            if (Input.GetAxis("Vertical") > 0f || Input.GetAxis("Horizontal") < 0f ||
                    Input.GetAxis("Vertical") < 0f || Input.GetAxis("Horizontal") > 0f)
                popUpIndex++;
        }
        //Attack
        else if (popUpIndex == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
                spawner.SetActive(true);
                spawnScript.SetEnabled(true);
            }
        }
        //Enemies
        else if (popUpIndex == 2)
        {
            if(spawnScript.GetPointsRemaining() < 1 && spawnScript.GetNumEnemies() == 0)
            {
                playerScript.GainExperience(playerScript.GetNextLevelExperience());
                popUpIndex++;
            }
        }
        //Activating the stats screen
        else if (popUpIndex == 3)
        {
            if (Input.GetButtonDown("Open Stats"))
                popUpIndex++;
        }
        //Leveling up
        else if (popUpIndex == 4)
        {
            if ((player.GetComponent<Player>().GetDamage() > 1 || player.GetComponent<Player>().GetDefense() > 1
                || player.GetComponent<Player>().GetAttackSpeed() > 1) && Input.GetButtonDown("Open Stats"))
                popUpIndex++;
        }
        //Move towards game
        else if (popUpIndex == 5)
        {
            if(door.activeSelf)
                door.SetActive(false);
        }
    }
}
