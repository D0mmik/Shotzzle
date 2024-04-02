using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MoveScript : MonoBehaviour
{
    public ShootingScript shootingScript;
    [SerializeField] Transform player;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float groundDrag;
    bool isGrounded;
    float horizontalInput;
    float verticalInput;
    Rigidbody rb;
    InputSystem playerInput;

    void Awake()
    {
        AudioListener.volume = Settings.volume;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        playerInput = new InputSystem();
        playerInput.Player.Enable();
        playerInput.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        playerInput.Player.Jump.performed -= Jump;
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (!isGrounded) return;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        PlayerInput();
        SpeedControl();

        rb.drag = isGrounded ? groundDrag : 1;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void PlayerInput()
    {
        Vector2 inputVector = playerInput.Player.Movement.ReadValue<Vector2>();
        horizontalInput = inputVector.x;
        verticalInput = inputVector.y;
    }

    void Movement()
    {
        if (GameManager.Instance.IsPaused) return;
        Vector3 moveDirection = player.forward * verticalInput + player.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * (speed * 10f * (isGrounded ? 1 : 2)), ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (!(flatVel.magnitude > speed)) return;
        
        Vector3 limitedVel = flatVel.normalized * speed;
        rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);

    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.transform.tag)
        {
            case "JumpPad":
                rb.AddForce(Vector3.up * 23, ForceMode.Impulse);
                break;
            case "Teleport":
                this.gameObject.transform.position =
                    other.gameObject.GetComponent<TeleportLocation>().teleportPosition.position;
                break;
            case "Lava":
                GameManager.Instance.ShowDeathScreen();
                break;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.transform.tag)
        {
            case "AmmoPowerUp":
                shootingScript.AddAmmo(10);
                Destroy(other.gameObject);
                break;
            case "Lava":
                GameManager.Instance.ShowDeathScreen();
                break;
            case "GuidedSpawner":
                GameObject[] guidedSpheres = GameObject.FindGameObjectsWithTag("Lava");
                foreach (var item in guidedSpheres)
                    Destroy(item);
                break;
            case "Border":
                GameManager.Instance.ShowDeathScreen();
                break;
            case "Finish":
                GameManager.Instance.ShowFinishScreen();
                break;
        }
    }
}
