using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleEntrance : MonoBehaviour
{
    [SerializeField] GameObject castleGate = null;
    Inventory playerInventory = null;
    private bool open = false;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
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
