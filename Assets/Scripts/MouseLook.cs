using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensitivity = 10;
    InputSystem playerInput;
    float xRotation;
    Transform playerBody;

    void Awake()
    {
        playerInput = new InputSystem();
        playerInput.Player.Enable();
        playerBody = transform.parent;
    }

    void Update()
    {
        Vector2 mouseAxis = playerInput.Player.Look.ReadValue<Vector2>() * (Time.deltaTime * sensitivity);
        
        xRotation -= mouseAxis.y;
        xRotation = Mathf.Clamp(xRotation, -65f, 65f);
        
        transform.localRotation = Quaternion.Euler(xRotation,0,0);
        playerBody.Rotate(Vector3.up * mouseAxis.x);
    }
}
