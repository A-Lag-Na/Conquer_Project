using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject spawner;
    public GameObject[] popUps;


    int popUpIndex = 0;

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
            if (Input.GetButtonDown("Forward") || Input.GetButtonDown("Left") ||
                    Input.GetButtonDown("Backward") || Input.GetButtonDown("Right"))
                popUpIndex++;
        }
        //Attack
        else if (popUpIndex == 1)
        {
            if (Input.GetMouseButtonDown(0))
                popUpIndex++;
        }
        //Enemies
        else if (popUpIndex == 2)
        {
            enemy.SetActive(true);
            if (spawner.GetComponent<SpawnScript>().spawnedEnemies.Count == 0)
                popUpIndex++;
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
            spawner.GetComponent<SpawnScript>().SetDoorLock(false);
        }
    }

}
