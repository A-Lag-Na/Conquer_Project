using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region PlayerStats
    [SerializeField] float playerMovementSpeed = 8.0f;
    [SerializeField] float playerHealth = 10.0f;
    [SerializeField] float maxPlayerHealth = 10.0f;
    [SerializeField] int playerDefense = 1;
    [SerializeField] float playerAttackSpeed = 1.0f;
    [SerializeField] private int visualAttackSpeed = 1;
    [SerializeField] int playerAttackDamage = 1;
    private float lastTimeFired = 0.0f;
    private float playerExperience = 0.0f;
    private float nextLevelExperience = 10.0f;
    private int playerLevel = 1;
    [SerializeField] private int playerSpendingPoints = 0;
    [SerializeField] private int playerLives = 5;

    //If player is immune to status conditions
    public bool isIceImmune = false;
    public bool isFireImmune = false;
    public bool isStunImmune = false;

    //If player is currently stunned
    public bool isStunned = false;

    private bool isRotated = false;
    private bool isDashing = false;
    public bool isRegenerating = false;
    #endregion

    #region UnityComponents
    private CharacterController characterController = null;
    private Renderer playerRenderer = null;
    private Color playerColor = Color.black;
    private AudioSource source = null;
    private Inventory inventory = null;
    [SerializeField] private AudioClip fire = null;
    [SerializeField] private Texture2D crosshairs = null;
    private Animator animator = null;
    private GameObject dashTrail = null;
    [SerializeField] private GameObject mainUI = null;
    #endregion

    #region PlayerMovementProperties
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 mousePosition = Vector3.zero;
    private Vector3 targetPosition = Vector3.zero;
    [SerializeField] private Camera mainCamera = null;
    private float playerY = 0.0f;
    private bool paused = false;
    #endregion

    #region Projectiles
    [SerializeField] private GameObject projectile0 = null;
    [SerializeField] private GameObject projectile1 = null;
    [SerializeField] private GameObject projectile2 = null;
    [SerializeField] private GameObject projectile3 = null;
    [SerializeField] private uint bulletVelocity = 0;
    [SerializeField] private GameObject projectilePosition = null;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        dashTrail = GameObject.Find("DashTrail");
        dashTrail.SetActive(false);

        characterController = GetComponent<CharacterController>();

        inventory = GetComponent<Inventory>();

        playerRenderer = GetComponentInChildren<Renderer>();
        playerColor = playerRenderer.material.color;

        animator = GetComponent<Animator>();

        playerY = transform.position.y;

        Cursor.SetCursor(crosshairs, new Vector2(128, 128), CursorMode.Auto);

        source = GetComponent<AudioSource>();
        source.enabled = true;

        if (GameObject.Find("Main UI"))
            mainUI = GameObject.Find("Main UI");

        lastTimeFired = 0.0f;
        isRotated = false;
        isDashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            #region PlayerRotation

            // Rotate the Player Transform to face the Mouse Position
            mousePosition = Input.mousePosition;
            targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            Vector3 relativePosition = targetPosition - transform.position;


            Quaternion rotation = Quaternion.LookRotation(relativePosition);
            // Lock the rotation around X and Z Axes
            rotation.x = 0.0f;
            rotation.z = 0.0f;


            // Change the player's tranform's rotation to the rotation Quaternion
            if (isRotated == false)
                transform.rotation = rotation;

            #endregion

            #region HitFeedback

            // Player reverting to original color after hit
            if (playerRenderer.material.color != playerColor)
                playerRenderer.material.color = Color.Lerp(playerRenderer.material.color, playerColor, 0.1f);

            #endregion

            if (!isStunned)
            {
                #region PlayerMovement

                // Move the Player GameObject when the WASD or Arrow Keys are pressed
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                moveDirection *= playerMovementSpeed;
                characterController.Move(moveDirection * Time.deltaTime);


                // Player Dash if Spacebar is pressed
                if (Input.GetKeyDown(KeyCode.Space) && moveDirection != Vector3.zero)
                    if (isDashing == false)
                        StartCoroutine(PlayerDash());


                //Set animator values
                if (animator != null)
                {
                    animator.SetBool("Walk", true);
                    animator.SetFloat("Horizontal", moveDirection.x);
                    animator.SetFloat("Vertical", moveDirection.z);
                }

                #endregion

                #region PlayerAttack

                // If the corresponding button is clicked call ShootBullet
                if (Input.GetKey(KeyCode.Mouse0))
                    ShootBullet(3);
                if (Input.GetKey(KeyCode.Mouse1))
                    ShootBullet(1);
                if (Input.GetKey(KeyCode.Mouse2))
                    ShootBullet(2);
                if (Input.GetKey(KeyCode.Alpha1))
                    ShootBullet(0);

                #endregion
            }
        }
        transform.position = new Vector3(transform.position.x, playerY, transform.position.z);
    }

    #region PlayerFunctions

    public void ShootBullet(int type)
    {
        //Instantiate a projectile and set the projectile's velocity towards the forward vector of the player transform
        if (Time.time > lastTimeFired + playerAttackSpeed)
        {
            GameObject clone;
            switch (type)
            {
                case 0:
                    {
                        clone = Instantiate(projectile0, projectilePosition.transform.position, transform.rotation);
                        clone.GetComponent<TrailRenderer>().startColor = Color.black;
                        clone.GetComponent<TrailRenderer>().endColor = Color.black;
                        break;
                    }
                case 1:
                    {
                        clone = Instantiate(projectile1, projectilePosition.transform.position, transform.rotation);
                        break;
                    }
                case 2:
                    {
                        clone = Instantiate(projectile2, projectilePosition.transform.position, transform.rotation);
                        clone.GetComponent<TrailRenderer>().startColor = Color.cyan;
                        clone.GetComponent<TrailRenderer>().endColor = Color.white;
                        break;
                    }
                case 3:
                    {
                        clone = Instantiate(projectile3, projectilePosition.transform.position, transform.rotation);
                        break;
                    }
                default:
                    {
                        clone = null;
                        break;
                    }
            }
            clone.GetComponent<TrailRenderer>().time = .1125f;
            clone.GetComponent<CollisionScript>().bulletDamage = playerAttackDamage;
            clone.gameObject.layer = 10;
            clone.gameObject.SetActive(true);
            clone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * bulletVelocity);
            lastTimeFired = Time.time;
            source.PlayOneShot(fire);
            StartCoroutine(ShootRotation());
        }
    }

    IEnumerator ShootRotation()
    {
        isRotated = true;
        animator.SetTrigger("Attack");
        transform.Rotate(0, 90, 0);
        yield return new WaitForSeconds(.5f);
        isRotated = false;
    }

    IEnumerator PlayerDash()
    {
        dashTrail.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        isDashing = true;
        gameObject.layer = 15;
        characterController.Move(moveDirection);
        yield return new WaitForSeconds(0.1f);
        gameObject.layer = 9;
        yield return new WaitForSeconds(0.3f);
        dashTrail.SetActive(false);
        yield return new WaitForSeconds(0.6f);
        isDashing = false;
    }

    public void BlinkOnHit()
    {
        animator.SetTrigger("On Hit");
        playerRenderer.material.color = Color.red;
    }

    public void Death()
    {
        animator.SetBool("Death", true);
        Instantiate(Resources.Load<GameObject>("Prefabs/Game Over Screen"));
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Fire Enemy") || collision.collider.CompareTag("Ice Enemy") || collision.collider.CompareTag("BulletHell Enemy"))
            TakeDamage();
    }

    #endregion

    #region AccessorsAndMutators

    #region Health
    public void TakeDamage(float amountOfDamage = 1)
    {
        //Decrease health by amountOfDamage until 0 or less
        amountOfDamage /= playerDefense;
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
        if (playerHealth > maxPlayerHealth)
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

    public void ModifyHealth(float _playerHealth)
    {
        maxPlayerHealth += _playerHealth;
    }

    public IEnumerator HealthRegen()
    {
        isRegenerating = true;
        if (playerHealth < maxPlayerHealth)
            playerHealth += 0.5f;
        yield return new WaitForSeconds(2.5f);
        isRegenerating = false;
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

    public void ModifyDefense(int _playerDefense)
    {
        playerDefense += _playerDefense;
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

    public void ModifyDamage(int _playerDamage)
    {
        playerAttackDamage += _playerDamage;
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

    public void ModifySpeed(float _playerSpeed)
    {
        GetComponent<ConditionManager>().Refresh(_playerSpeed);
    }
    #endregion

    #region LevelAndXP

    public void LevelUp()
    {
        maxPlayerHealth += 10;
        playerHealth = maxPlayerHealth;
        playerMovementSpeed++;
        playerSpendingPoints++;
        playerExperience -= 10;
        playerLevel++;
        GetComponent<ConditionManager>().Refresh();
        if (mainUI != null && mainUI.activeSelf)
            mainUI.GetComponent<UpdateUI>().LevelUp();
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
    public void OnPauseGame()
    {
        paused = true;
    }

    public void OnResumeGame()
    {
        paused = false;
    }
    #endregion

    #region Stun
    public void Stun()
    {
        isStunned = true;
    }
    public void Unstun()
    {
        isStunned = false;
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