using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum Type {Coin, Health, Potion, Box, EOF};
    public Type type = Type.Coin;
    [SerializeField] AudioClip clip = null;

    private void Update()
    {
        if(Type.Coin == type)
            transform.Rotate(Vector3.forward * (100.0f * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponentInParent<Player>();
            AudioSource source = collision.collider.GetComponentInParent<AudioSource>();
            if(clip != null)
                source.PlayOneShot(clip);
            switch (type)
            {
                case Type.Coin:
                    collision.collider.GetComponentInParent<Player>().AddCoins(1);
                    break;
                case Type.Health:
                    collision.collider.GetComponentInParent<Player>().RestoreHealth(10);
                    break;
                case Type.Potion:
                    collision.collider.GetComponentInParent<Inventory>().AddConsumable(GetComponent<Consumable>());
                    break;
                case Type.Box:
                    collision.collider.GetComponentInParent<Inventory>().AddBoxPiece();
                    break;
                case Type.EOF:
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponentInParent<Player>();
            AudioSource source = other.GetComponentInParent<AudioSource>();
            if (clip != null)
                source.PlayOneShot(clip);
            switch (type)
            {
                case Type.Coin:
                    other.GetComponentInParent<Player>().AddCoins(1);
                    break;
                case Type.Health:
                    other.GetComponentInParent<Player>().RestoreHealth(10);
                    break;
                case Type.Potion:
                    other.GetComponentInParent<Inventory>().AddConsumable(GetComponent<Consumable>());
                    break;
                case Type.Box:
                    other.GetComponentInParent<Inventory>().AddBoxPiece();
                    break;
                case Type.EOF:
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}
