using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public int health;


    private Transform form;
    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector3 mousePosition;
    private Vector3 middleOfScreen;
    private Vector3 cameraVector;
    [SerializeField] GameObject projectile;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        form = GetComponent<Transform>();
        middleOfScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the Player Transform to face the Mouse Position
        cameraVector = Input.mousePosition - middleOfScreen;
        mousePosition = new Vector3(cameraVector.x, 0f, cameraVector.y);
        form.LookAt(mousePosition);

        // Move the Player GameObject when the WASD or Arrow Keys are pressed
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection *= speed;
        characterController.Move(moveDirection * Time.deltaTime);

        // If the right mouse button is clicked Instantiate a projectile and set the projectile's velocity towards the forward vector of the player transform
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject clone = Instantiate(projectile, form.position, form.rotation);
            clone.gameObject.SetActive(true);
            clone.GetComponent<Rigidbody>().velocity = form.TransformDirection(Vector3.forward * 10);
        }
    }

    public void TakeDamage()
    {
        //Decrement health until 0 or less
        health--;
        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }
}
