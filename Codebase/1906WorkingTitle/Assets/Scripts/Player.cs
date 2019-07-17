using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    [SerializeField] float playerHealth;
    [SerializeField] float maxPlayerHealth;
    [SerializeField] int playerCoins;

    private Transform playerTransform;
    private CharacterController characterController;
    private Renderer playerRenderer;
    private Color playerColor;

    private Vector3 moveDirection;
    private Vector3 mousePosition;
    private Vector3 targetPosition;

    [SerializeField] GameObject projectile;
    [SerializeField] uint bulletVelocity;
    [SerializeField] Camera mainCamera;
    [SerializeField] float blinkTime = 0.1f;
    private float playerY;

    [SerializeField] GameObject mainUI;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerTransform = GetComponent<Transform>();
        playerRenderer = GetComponent<Renderer>();
        playerColor = playerRenderer.material.color;
        blinkTime = 0.1f;
        playerY = playerTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
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
        moveDirection *= movementSpeed;
        characterController.Move(moveDirection * Time.deltaTime);

        playerTransform.position = new Vector3(playerTransform.position.x, playerY, playerTransform.position.z);

        // If the right mouse button is clicked Instantiate a projectile and set the projectile's velocity towards the forward vector of the player transform
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject clone = Instantiate(projectile, playerTransform.position, playerTransform.rotation);
            clone.gameObject.SetActive(true);
            clone.GetComponent<Rigidbody>().velocity = playerTransform.TransformDirection(Vector3.forward * bulletVelocity);
        }

        if (playerRenderer.material.color != playerColor)
        {
            playerRenderer.material.color = Color.Lerp(playerRenderer.material.color, playerColor, blinkTime);
        }
    }

    public void BlinkOnHit()
    {
        playerRenderer.material.color = Color.red;
    }

    public void TakeDamage()
    {
        BlinkOnHit();
        mainUI.GetComponent<UpdateUI>().TakeDamage();
        //Decrement health until 0 or less
        playerHealth--;
        if (playerHealth <= 0)
        {
            Death();
        }
    }

    public float GetHealth()
    {
        return playerHealth;
    }

    public float GetMaxHealth()
    {
        return maxPlayerHealth;
    }

    public void AddHealth(float amountOfHealth)
    {
        playerHealth += amountOfHealth;
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

    public void AddCoins(int amountOfCoins)
    {
        playerCoins += amountOfCoins;
    }

    public int GetCoins()
    {
        return playerCoins;
    }

    public void IncreaseHealth()
    {
        playerHealth+=10;
        maxPlayerHealth+=10;
    }
}
