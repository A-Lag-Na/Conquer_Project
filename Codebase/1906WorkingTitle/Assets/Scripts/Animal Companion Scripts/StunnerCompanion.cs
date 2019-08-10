using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnerCompanion : MonoBehaviour
{
    GameObject player = null;
    GameObject target = null;
    Player playerStats = null;
    Vector3 animalVelocity = Vector3.zero;
    Vector3 playerPositionOffset = Vector3.zero;
    bool canAttack = true;
    [SerializeField] GameObject projectile = null;
    [SerializeField] GameObject projectilePos = null;
    [SerializeField] AudioClip fire = null;
    AudioSource source;

    // Update is called once per frame
    void Update()
    {
        playerPositionOffset = new Vector3(player.transform.position.x - 3, player.transform.position.y, player.transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, playerPositionOffset, ref animalVelocity, 0.5f);
        Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        // Lock the rotation around X and Z Axes
        rotation.x = 0.0f;
        rotation.z = 0.0f;
        // Change the companion's tranform's rotation to the rotation Quaternion
        transform.rotation = rotation;
        target = GameObject.FindGameObjectWithTag("Enemy");
        if (target == null)
            target = GameObject.FindGameObjectWithTag("BulletHell Enemy");
        if (target != null)
        {
            if (Vector3.Distance(target.transform.position, player.transform.position) <= 20)
            {
                rotation = Quaternion.LookRotation(target.transform.position - transform.position);
                // Lock the rotation around X and Z Axes
                rotation.x = 0.0f;
                rotation.z = 0.0f;
                // Change the companion's tranform's rotation to the rotation Quaternion
                transform.rotation = rotation;
                if (canAttack)
                    StartCoroutine(Attack());
            }
        }
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<Player>();
        transform.position = playerPositionOffset;
        source = GetComponent<AudioSource>();
        source.enabled = true;
    }

    IEnumerator Attack()
    {
        canAttack = false;
        GameObject clone = Instantiate(projectile, projectilePos.transform.position, projectile.transform.rotation);
        clone.GetComponent<CollisionScript>().bulletDamage = 0;
        clone.gameObject.layer = 10;
        clone.SetActive(true);
        source.PlayOneShot(fire);
        clone.GetComponent<Rigidbody>().velocity = transform.forward * 15;
        yield return new WaitForSeconds(5);
        canAttack = true;
    }
}
