using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private enum Type { Inventory, Coin, Health, EOF};

    [SerializeField] Type type;
    [SerializeField] Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * (100.0f * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            switch (type)
            {
                case Type.Inventory:
                    //player.AddToInventory(this);
                    break;
                case Type.Coin:
                    player.GetComponent<Player>().AddCoins(5);
                    break;
                case Type.Health:
                    player.GetComponent<Player>().AddHealth(10);
                    break;
                case Type.EOF:
                    break;
                default:
                    break;
            }
        }
        Destroy(gameObject);
    }
}
