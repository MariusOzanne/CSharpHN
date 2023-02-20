using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float horizontalAxis;
    public float verticalAxis;

    public Transform Ground;

    [Header("speeds")]
    [SerializeField]public float speed = 2f;
    [SerializeField]public float rotationSpeed = 45f;
    [SerializeField]public float force = 1000f;

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
    }

    // Update is called once per frame
    void Update()
    {

        // Player movements
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalAxis, 0, verticalAxis);

        transform.Translate(movement * Time.deltaTime * speed);

        //Jumping animation
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            //playerAnimator.SetBool("isJumping", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerRigidBody.position.Set(playerRigidBody.position.x, playerRigidBody.position.y, playerRigidBody.position.z);
        Debug.Log("collide");
    }
}
