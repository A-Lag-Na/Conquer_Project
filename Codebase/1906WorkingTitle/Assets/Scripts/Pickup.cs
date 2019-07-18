using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private enum Type { Inventory, Coin, Health, EOF};

    [SerializeField] Type type;

    private void Start()
    {

    }

    private void Update()
    {
        if(Type.Coin == type)
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
                    collision.collider.GetComponentInParent<Player>().AddCoins(1);
                    break;
                case Type.Health:
                    collision.collider.GetComponentInParent<Player>().RestoreHealth(10);
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
