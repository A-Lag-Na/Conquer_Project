using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region PlayerStats
    [SerializeField] float playerMovementSpeed;
    [SerializeField] float playerHealth;
    [SerializeField] float maxPlayerHealth;
    [SerializeField] int playerCoins;
    [SerializeField] int playerDefense;
    [SerializeField] float playerAttackSpeed;
    private int visualAttackSpeed;
    [SerializeField] int playerAttackDamage;
    private float lastTimeFired;
    [SerializeField] private float playerExperience;
    private float nextLevelExperience;
    private int playerLevel;
    private int playerSpendingPoints;
    #endregion

    #region UnityComponents
    private Transform playerTransform;
    private CharacterController characterController;
    private Renderer playerRenderer;
    private Color playerColor;
    #endregion

    #region PlayerMovementProperties
    private Vector3 moveDirection;
    private Vector3 mousePosition;
    private Vector3 targetPosition;
    [SerializeField] Camera mainCamera;
    private float playerY;
    #endregion

    #region Projectiles
    [SerializeField] GameObject projectile;
    [SerializeField] uint bulletVelocity;
    #endregion

    [SerializeField] GameObject mainUI;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerTransform = GetComponent<Transform>();
        playerRenderer = GetComponent<Renderer>();
        playerColor = playerRenderer.material.color;
        playerY = playerTransform.position.y;
        if (GameObject.Find("Main UI"))
            mainUI = GameObject.Find("Main UI");
        playerMovementSpeed = 3;
        maxPlayerHealth = 10;
        playerHealth = 10;
        playerCoins = 0;
        playerDefense = 0;
        playerAttackSpeed = 1.0f;
        visualAttackSpeed = 1;
        playerAttackDamage = 1;
        lastTimeFired = 0.0f;
        playerExperience = 0;
        playerLevel = 1;
        nextLevelExperience = 10;
        playerSpendingPoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        #region PlayerMovement
        // Rotate the Player Transform to face the Mouse Position
        mousePosition = Input.mousePosition;
        targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        Vector3 relativePosition = targetPosition - playerTransform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePosition);
        // Lock the rotation around X and Z Axes
        rotation.x = 0.0f;
        rotation.z = 0.0f;
        // Change the player's tranform's rotation to the rotation Quaternion
        playerTransform.rotation = rotation;

        // Move the Player GameObject when the WASD or Arrow Keys are pressed
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection *= playerMovementSpeed;
        characterController.Move(moveDirection * Time.deltaTime);

        playerTransform.position = new Vector3(playerTransform.position.x, playerY, playerTransform.position.z);
        #endregion

        #region PlayerAttack
        // If the right mouse button is clicked call ShootBullet
        if (Input.GetKey(KeyCode.Mouse0))
        {
            ShootBullet();
        }
        #endregion

        #region HitFeedback
        // Player reverting to original color after hit
        if (playerRenderer.material.color != playerColor)
            playerRenderer.material.color = Color.Lerp(playerRenderer.material.color, playerColor, 0.1f);
        #endregion

        #region Leveling
        if (playerExperience >= nextLevelExperience)
            LevelUp();
        #endregion
    }

    public void ShootBullet()
    {
        //Instantiate a projectile and set the projectile's velocity towards the forward vector of the player transform
        if (Time.time > lastTimeFired + playerAttackSpeed)
        {
            GameObject clone = Instantiate(projectile, playerTransform.position, playerTransform.rotation);
            clone.gameObject.tag = "Player Bullet";
            clone.gameObject.layer = 10;
            clone.gameObject.SetActive(true);
            clone.GetComponent<TrailRenderer>().startColor = Color.black;
            clone.GetComponent<TrailRenderer>().endColor = Color.white;
            clone.GetComponent<Rigidbody>().velocity = playerTransform.TransformDirection(Vector3.forward * bulletVelocity);
            lastTimeFired = Time.time;
        }
    }

    public void BlinkOnHit()
    {
        playerRenderer.material.color = Color.red;
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

    public void LevelUp()
    {
        maxPlayerHealth += 10;
        playerHealth = maxPlayerHealth;
        playerMovementSpeed++;
        playerSpendingPoints++;
        nextLevelExperience += 10;
        playerLevel++;
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
            Death();
    }

    public void RestoreHealth(float amountOfHealth)
    {
        playerHealth += amountOfHealth;
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
        playerCoins += amountOfCoins;
    }

    public int GetCoins()
    {
        return playerCoins;
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
    public float GetMovementSpeed()
    {
        return playerMovementSpeed;
    }
    #endregion

    #region LevelAndXP
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
    #endregion

    #endregion
}
