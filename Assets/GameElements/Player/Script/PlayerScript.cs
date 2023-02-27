using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    bool isOnTheGround = true;

    public float horizontalAxis;
    public float verticalAxis;

    public float maxHealth = 100;
    public float currentHealth;

    public float maxStamina = 100;
    public float currentStamina;

    public float maxHunger = 100;
    public float currentHunger;

    public HealthBar healthBar;
    public StaminaBar staminaBar;
    public HungerBar hungerBar;
    public Transform Ground;

    [Header("speeds")]
    [SerializeField]public float speed = 2f;
    [SerializeField]public float rotationSpeed = 45f;
    [SerializeField]public float force = 1000f;
    [SerializeField] public float jumpForce = 5;

    Rigidbody playerRigidBody;
    AudioSource playerAudioSource;
    BoxCollider playerCollider;
    [SerializeField] private AudioClip playerAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerAudioClip = GetComponent<AudioClip>();
        playerCollider = GetComponent<BoxCollider>();

        //hp bar
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        // stamina bar
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);

        // hunger bar
        currentHunger = maxHunger;
        hungerBar.SetMaxHunger(maxHunger);
    }

    private void FixedUpdate()
    {
        //Running animation
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && (horizontalAxis != 0 || verticalAxis != 0) && currentStamina > 0)
        {
            speed = 20;
            UseStamina(10);
            UseHunger(1f);
        }
        else
        {
            speed = 10;
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Player movements
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalAxis);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalAxis);

        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed * 4 * horizontalAxis);

        //gain stammina 
        if (currentStamina < 100)
        {
            GainStamina(5);
        }

        //Walking animation
        if (horizontalAxis != 0 || verticalAxis != 0)
        {
            UseHunger(0.5f);
        }

        //Jumping animation
        if (Input.GetKeyDown(KeyCode.Space) && currentStamina > 0 && isOnTheGround)
        {
            playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            UseStamina(30);
            UseHunger(5f);
            isOnTheGround = false;
        }

        //damage test
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(20);
        }
        else if (currentHunger < 0)
        {
            TakeDamage(1);
        }

        //heal test
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(20);
        }

        //stam test
        if (Input.GetKey(KeyCode.L))
        {
            UseHunger(50);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerRigidBody.position.Set(playerRigidBody.position.x, playerRigidBody.position.y, playerRigidBody.position.z);
        Debug.Log("collide");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            isOnTheGround = true;
        }
        if (collision.gameObject.CompareTag("OutDoor"))
        {
            isOnTheGround = true;
        }
    }

    //damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    //use stam
    public void UseStamina(float stamina)
    {
        currentStamina -= stamina * Time.deltaTime;

        staminaBar.SetStamina(currentStamina);

    }

    //gain stam
    public void GainStamina(float stamina)
    {
        currentStamina += stamina * Time.deltaTime;

        staminaBar.SetStamina(currentStamina);

    }

    //use hunger
    public void UseHunger(float hunger)
    {
        currentHunger -= hunger * Time.deltaTime;

        hungerBar.SetHunger(currentHunger);

    }

    //gain hunger
    public void HealHunger(float hunger)
    {
        currentHunger += hunger;

        hungerBar.SetHunger(currentHunger);

    }

    //heal
    public void Heal(float heal)
    {
        if (currentHealth < 100)
        {
            currentHealth += heal;
            healthBar.SetHealth(currentHealth);
            Debug.Log("heal2");
        }
        else if (currentHealth > 100)
        {
            currentHealth = 100;
        }
    }
}
