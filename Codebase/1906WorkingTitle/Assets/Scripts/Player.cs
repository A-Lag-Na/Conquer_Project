using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region PlayerStats
    [SerializeField] float playerMovementSpeed;
    [SerializeField] float playerHealth;
    [SerializeField] float maxPlayerHealth;
    [SerializeField] int playerDefense;
    [SerializeField] float playerAttackSpeed;
    [SerializeField] private int visualAttackSpeed;
    [SerializeField] int playerAttackDamage;
    private float lastTimeFired;
    private float playerExperience;
    private float nextLevelExperience = 10;
    private int playerLevel = 1;
    [SerializeField] private int playerSpendingPoints = 0;
    [SerializeField] private int playerLives;

    //If player is immune to ice or fire conditions
    public bool iceImmune = false;
    public bool fireImmune = false;
    #endregion

    #region UnityComponents
    private Transform playerTransform;
    private CharacterController characterController;
    private Renderer playerRenderer;
    private Color playerColor;
    private AudioSource source;
    private Inventory inventory;
    [SerializeField] private AudioClip fire;
    [SerializeField] Texture2D crosshairs;
    private Animator animator;
    #endregion

    #region PlayerMovementProperties
    private Vector3 moveDirection;
    private Vector3 mousePosition;
    private Vector3 targetPosition;
    [SerializeField] Camera mainCamera;
    private float playerY;
    private bool paused;
    #endregion

    #region Projectiles
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject projectile2;
    [SerializeField] GameObject projectile3;
    [SerializeField] uint bulletVelocity;
    [SerializeField] GameObject projectilePosition;
    #endregion

   
    [SerializeField] GameObject mainUI;
   

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        inventory = GetComponent<Inventory>();
        playerRenderer = GetComponentInChildren<Renderer>();
        animator = GetComponent<Animator>();
        playerColor = playerRenderer.material.color;
        playerY = transform.position.y;
        if (GameObject.Find("Main UI"))
            mainUI = GameObject.Find("Main UI");
        maxPlayerHealth = 10;
        playerHealth = 10;
        visualAttackSpeed = 1;
        lastTimeFired = 0.0f;
        playerLives = 5;
        Cursor.SetCursor(crosshairs, new Vector2(128, 128), CursorMode.Auto);
        source = GetComponent<AudioSource>();
        source.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            #region PlayerMovement
            // Rotate the Player Transform to face the Mouse Position
            mousePosition = Input.mousePosition;
            targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            Vector3 relativePosition = targetPosition - transform.position;

            Quaternion rotation = Quaternion.LookRotation(relativePosition);
            // Lock the rotation around X and Z Axes
            rotation.x = 0.0f;
            rotation.z = 0.0f;
            // Change the player's tranform's rotation to the rotation Quaternion
            transform.rotation = rotation;

            // Move the Player GameObject when the WASD or Arrow Keys are pressed
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= playerMovementSpeed;
            characterController.Move(moveDirection * Time.deltaTime);

            //transform.position = new Vector3(transform.position.x, playerY, transform.position.z);

            //Set animator values
            if (animator != null)
            {
                animator.SetBool("Walk", true);
                animator.SetFloat("Horizontal", moveDirection.x);
                animator.SetFloat("Vertical", moveDirection.z);  
            }

            
            #endregion

            #region PlayerAttack
            // If the right mouse button is clicked call ShootBullet
            if (Input.GetKey(KeyCode.Mouse0))
            {
                ShootBullet(1);
            }
            if (Input.GetKey(KeyCode.Mouse1))
            {
                ShootBullet(2);
            }
            if (Input.GetKey(KeyCode.Mouse2))
            {
                ShootBullet(3);
            }
            #endregion

            #region HitFeedback
            // Player reverting to original color after hit
            if (playerRenderer.material.color != playerColor)
                playerRenderer.material.color = Color.Lerp(playerRenderer.material.color, playerColor, 0.1f);
            #endregion
        }
    }

    public void ShootBullet(int type)
    {
        //Instantiate a projectile and set the projectile's velocity towards the forward vector of the player transform
        if (Time.time > lastTimeFired + playerAttackSpeed)
        {
            GameObject clone;
            switch(type)
            {
                case 1:
                    {
                        clone = Instantiate(projectile2, projectilePosition.transform.position, transform.rotation);
                        break;
                    }
                case 2:
                    {
                        clone = Instantiate(projectile3, projectilePosition.transform.position, transform.rotation);
                        clone.GetComponent<TrailRenderer>().startColor = Color.cyan;
                        clone.GetComponent<TrailRenderer>().endColor = Color.white;
                        break;
                    }
                default:
                    {
                        clone = Instantiate(projectile, projectilePosition.transform.position, transform.rotation);
                        clone.GetComponent<TrailRenderer>().startColor = Color.black;
                        clone.GetComponent<TrailRenderer>().endColor = Color.white;
                        break;
                    }
            }
            clone.GetComponent<CollisionScript>().bulletDamage = playerAttackDamage;
            clone.gameObject.layer = 10;
            clone.gameObject.SetActive(true);
            clone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * bulletVelocity);
            lastTimeFired = Time.time;
            animator.SetTrigger("Attack");
            source.PlayOneShot(fire);
        }
    }

    public void BlinkOnHit()
    {
        animator.SetTrigger("On Hit");
        playerRenderer.material.color = Color.red;
    }

    public void Death()
    {
        animator.SetBool("Death", true);
        gameObject.SetActive(false);
    }

    #region AccessorsAndMutators

    #region Health
    public void TakeDamage(int amountOfDamage = 1)
    {
        //Decrease health by amountOfDamage until 0 or less
        amountOfDamage -= playerDefense;
        if (amountOfDamage <= 0)
        {
            playerRenderer.material.color = Color.yellow;
            return;
        }
        BlinkOnHit();
        playerHealth -= amountOfDamage;
        if (mainUI != null && mainUI.activeSelf)
            mainUI.GetComponent<UpdateUI>().TakeDamage();
        if (playerHealth <= 0)
        {
            playerLives--;
            if (playerLives <= 0)
                Death();
            playerHealth = maxPlayerHealth;
        }
    }

    public void RestoreHealth(float amountOfHealth)
    {
        playerHealth += amountOfHealth;
        if(playerHealth>maxPlayerHealth)
            playerHealth = maxPlayerHealth;
    }

    public float GetHealth()
    {
        return playerHealth;
    }

    public float GetMaxHealth()
    {
        return maxPlayerHealth;
    }

    public void IncreaseHealth()
    {
        playerHealth += 10;
        maxPlayerHealth += 10;
    }
    #endregion

    #region Coins
    public void AddCoins(int amountOfCoins)
    {
        inventory.AddCoins(amountOfCoins);
    }

    public int GetCoins()
    {
        return inventory.GetCoins();
    }
    #endregion

    #region Defense
    public int GetDefense()
    {
        return playerDefense;
    }

    public void AddDefense()
    {
        playerDefense++;
        playerSpendingPoints--;
    }
    #endregion

    #region Damage
    public int GetDamage()
    {
        return playerAttackDamage;
    }

    public void AddDamage()
    {
        playerAttackDamage++;
        playerSpendingPoints--;
    }
    #endregion

    #region AttackSpeed
    public int GetAttackSpeed()
    {
        return visualAttackSpeed;
    }

    public void AddAttackSpeed()
    {
        visualAttackSpeed++;
        playerAttackSpeed -= 0.1f;
        playerSpendingPoints--;
    }
    #endregion

    #region MovementSpeed
    public void SetMovementSpeed(float newMovementSpeed)
    {
        playerMovementSpeed = newMovementSpeed;
    }

    public float GetMovementSpeed()
    {
        return playerMovementSpeed;
    }
    #endregion

    #region LevelAndXP

    public void LevelUp()
    {
        maxPlayerHealth += 10;
        playerHealth = maxPlayerHealth;
        playerMovementSpeed++;
        playerSpendingPoints++;
        playerExperience = 0;
        playerLevel++;
        GetComponent<ConditionManager>().Refresh();
    }

    public float GetExperience()
    {
        return playerExperience;
    }

    public float GetNextLevelExperience()
    {
        return nextLevelExperience;
    }

    public int GetSpendingPoints()
    {
        return playerSpendingPoints;
    }

    public int GetLevel()
    {
        return playerLevel;
    }

    public void GainExperience(int playerEXP)
    {
        playerExperience += playerEXP;
        if (playerExperience >= nextLevelExperience)
            LevelUp();
    }
    #endregion

    #region PlayerLives
    public int GetLives()
    {
        return playerLives;
    }

    public void IncreaseLives()
    {
        playerLives++;
    }

    #endregion

    #region Pause
    void OnPauseGame()
    {
        paused = true;
    }
    
    void OnResumeGame()
    {
        paused = false;
    }
    #endregion

    #region Weapon and Potion
    public Sprite GetWeapon()
    {
        return inventory.WeaponSprite();
    }

    public Sprite GetPotion()
    {
        return inventory.PotionSprite();
    }
    #endregion

    #endregion
}