using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MoveScript : MonoBehaviour
{
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
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        playerInput = new InputSystem();
        playerInput.Player.Enable();
        playerInput.Player.Jump.performed += Jump;
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
        if (other.transform.CompareTag("JumpPad"))
        {
            rb.AddForce(Vector3.up * 23, ForceMode.Impulse);
        }

        if (other.transform.CompareTag("Teleport"))
        {
            this.gameObject.transform.position = other.gameObject.GetComponent<TeleportLocation>().teleportPosition.position;
        }
        
        if (other.transform.CompareTag("Lava"))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
