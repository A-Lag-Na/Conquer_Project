using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleEntrance : MonoBehaviour
{
    [SerializeField] private GameObject castleGate = null;
    private Inventory playerInventory = null;
    private bool open = false;
    
    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    
    void Update()
    {
        if(playerInventory.GetBoxPieces() == 3)
            open = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && open)
        {
            StartCoroutine(OpenGate());
        }
    }

    IEnumerator OpenGate()
    {
        yield return new WaitForSeconds(.5f);
        castleGate.SetActive(false);
    }
}
