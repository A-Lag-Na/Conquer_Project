using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    #region CompanionStats
    int bulletDamage = 0;
    GameObject player = null;
    GameObject target = null;
    Player playerStats = null;
    Vector3 animalVelocity = Vector3.zero;
    Vector3 playerPositionOffset = Vector3.zero;
    public bool isFollowing = false;
    SphereCollider animalCollider = null;
    Inventory playerInventory = null;
    Pickup pickup = null;
    float lastTimeAttacked = 0.0f;
    bool hasAttacked = false;
    bool canAttack = true;
    [SerializeField] GameObject projectile = null;
    [SerializeField] GameObject projectilePos = null;
    [SerializeField] AudioClip fire = null;
    AudioSource source = null;
    [SerializeField] GameObject potion = null;
    float previousExp = 0.0f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<Player>();
        animalCollider = GetComponent<SphereCollider>();
        animalCollider.isTrigger = true;
        playerInventory = player.GetComponent<Inventory>();
        gameObject.layer = 0;
        if (name == "Shooter Companion" || name == "Stunner Companion")
        {
            source = GetComponent<AudioSource>();
            source.enabled = true;
            canAttack = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing == true)
        {
            if (name != "Item Grabber Companion" && name != "Melee Companion")
                transform.position = Vector3.SmoothDamp(transform.position, playerPositionOffset, ref animalVelocity, 0.5f);
            playerPositionOffset = new Vector3(player.transform.position.x - 3, player.transform.position.y + 1, player.transform.position.z);
            Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position);
            // Lock the rotation around X and Z Axes
            rotation.x = 0.0f;
            rotation.z = 0.0f;
            // Change the companion's tranform's rotation to the rotation Quaternion
            transform.rotation = rotation;
            if (name == "Health Regen Companion" && playerStats.GetisRegenerating() == false)
                StartCoroutine(playerStats.HealthRegen());
            else if (name == "Item Grabber Companion")
            {
                target = GameObject.FindGameObjectWithTag("Pickups");
                if (target == null)
                    transform.position = Vector3.SmoothDamp(transform.position, playerPositionOffset, ref animalVelocity, 0.5f);
                if (target != null)
                {
                    pickup = target.GetComponent<Pickup>();
                    if (Vector3.Distance(target.transform.position, player.transform.position) > 10)
                        transform.position = Vector3.SmoothDamp(transform.position, playerPositionOffset, ref animalVelocity, 0.5f);
                    if (Vector3.Distance(target.transform.position, player.transform.position) <= 10)
                        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position, ref animalVelocity, 0.5f);
                }
            }
            else if (name == "Melee Companion")
            {
                target = GameObject.FindGameObjectWithTag("Enemy");
                if (target == null)
                    target = GameObject.FindGameObjectWithTag("BulletHell Enemy");
                if (target == null)
                    transform.position = Vector3.SmoothDamp(transform.position, playerPositionOffset, ref animalVelocity, 0.5f);
                if (target != null)
                {
                    if (hasAttacked == true || Vector3.Distance(target.transform.position, player.transform.position) > 10)
                        transform.position = Vector3.SmoothDamp(transform.position, playerPositionOffset, ref animalVelocity, 0.5f);
                    if (Vector3.Distance(target.transform.position, player.transform.position) <= 10 && hasAttacked == false)
                        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position, ref animalVelocity, 0.5f);
                }
                if (Time.time > lastTimeAttacked + 5)
                    hasAttacked = false;
            }
            else if (name == "Shooter Companion" || name == "Stunner Companion")
            {
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
            else if (name == "Scavenger Companion")
                if (playerStats.GetExperience() > previousExp)
                {
                    Instantiate(potion, transform.position, transform.rotation);
                    previousExp = playerStats.GetExperience();
                }
        }
    }

    #region CompanionFunctions
    public void Activate()
    {
        if (name == "Attack Companion")
            playerStats.ModifyDamage(1);
        else if (name == "Coin Booster Companion")
            playerStats.CoinModifier(1);
        else if (name == "Defense Companion")
            playerStats.ModifyDefense(1);
        else if (name == "Fire Resist Companion")
            playerStats.isFireImmune = true;
        else if (name == "Health Regen Companion")
            playerStats.ModifyHealth(10);
        else if (name == "Ice Resist Companion")
            playerStats.isIceImmune = true;
        else if (name == "Item Grabber Companion")
            animalCollider.isTrigger = false;
        else if (name == "Melee Companion")
            animalCollider.isTrigger = false;
        else if (name == "Movement Speed Companion")
            playerStats.ModifySpeed(3);
        else if (name == "Stun Resist Companion")
            playerStats.isStunImmune = true;
        else if (name == "Shooter Companion")
            bulletDamage = 1;
        else if (name == "Stunner Companion")
            bulletDamage = 0;
        else if (name == "XP Companion")
            playerStats.XPModifier(0.5f);
        isFollowing = true;
        gameObject.layer = 19;
        playerStats.SetCompanion(this);
        previousExp = playerStats.GetExperience();
    }

    public void Deactivate()
    {
        playerStats.ResetCompanion();
        if (name == "Attack Companion")
            playerStats.ModifyDamage(-1);
        else if (name == "Coin Booster Companion")
            playerStats.CoinModifier(-1);
        else if (name == "Defense Companion")
            playerStats.ModifyDefense(-1);
        else if (name == "Fire Resist Companion")
            playerStats.isFireImmune = false;
        else if (name == "Health Regen Companion")
            playerStats.ModifyHealth(-10);
        else if (name == "Ice Resist Companion")
            playerStats.isIceImmune = false;
        else if (name == "Item Grabber Companion")
            animalCollider.isTrigger = true;
        else if (name == "Melee Companion")
            animalCollider.isTrigger = true;
        else if (name == "Movement Speed Companion")
            playerStats.ModifySpeed(-3);
        else if (name == "Stun Resist Companion")
            playerStats.isStunImmune = false;
        else if (name == "XP Companion")
            playerStats.XPModifier(-0.5f);
        isFollowing = false;
        gameObject.layer = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (name == "Item Grabber Companion")
            if (collision.collider.CompareTag("Pickups"))
                if (pickup.type == Pickup.Type.Coin)
                    playerInventory.AddCoins(1);
        if (name == "Melee Companion")
            if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("BulletHell Enemy"))
            {
                if (target != null)
                    target.GetComponent<EnemyStats>().TakeDamage();
                hasAttacked = true;
                lastTimeAttacked = Time.time;
            }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        GameObject clone = Instantiate(projectile, projectilePos.transform.position, projectilePos.transform.rotation);
        clone.GetComponent<CollisionScript>().bulletDamage = bulletDamage;
        clone.gameObject.layer = 10;
        clone.SetActive(true);
        source.PlayOneShot(fire);
        clone.GetComponent<Rigidbody>().velocity = transform.forward * 15;
        yield return new WaitForSeconds(5);
        canAttack = true;
    }
    #endregion
}
