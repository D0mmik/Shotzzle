using System.Timers;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensitivity = 10;
    InputSystem playerInput;
    float xRotation;
    float yRotation;
    Transform playerBody;
    float smoothRef;

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

        yRotation += mouseAxis.x * 12;

        float smoothMouseAxisXLerp = Mathf.Lerp(0, yRotation, 0.1f);
        
        transform.localRotation = Quaternion.Euler(xRotation,0,0);
        playerBody.localRotation = Quaternion.Euler(0,smoothMouseAxisXLerp,0);
    }
}
