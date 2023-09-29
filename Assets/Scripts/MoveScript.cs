using UnityEngine;
using UnityEngine.InputSystem;

public class MoveScript : MonoBehaviour
{
    [SerializeField] float speed = 1000f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float sprintSpeed = 1.5f;
    [SerializeField] float smoothInputSpeed = .4f;
    bool isGrounded;
    Rigidbody rb;
    InputSystem playerInput;
    Vector3 movementVelocity;
    Vector2 currentInputVector;
    Vector2 smoothInputVector;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

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

        Movement();
    }

    void Movement()
    {
        Vector2 inputVector = playerInput.Player.Movement.ReadValue<Vector2>();
        currentInputVector = Vector2.SmoothDamp(currentInputVector, inputVector, ref smoothInputVector, smoothInputSpeed);
        float sprintMultiplier = playerInput.Player.Sprint.ReadValue<float>() != 0 ? sprintSpeed : 1;
        Transform transform1 = transform;
        movementVelocity = (transform1.forward * currentInputVector.y + transform1.right * currentInputVector.x) * (speed * sprintMultiplier);
        movementVelocity.y = rb.velocity.y;
    }

    void FixedUpdate()
    {
        rb.velocity = movementVelocity;
    }
}
