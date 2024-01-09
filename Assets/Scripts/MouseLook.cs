using System.Timers;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensitivity = 10;
    InputSystem playerInput;
    float xRotation;
    float yRotation;
    [SerializeField] Transform playerBody;
    float smoothRef;

    void Awake()
    {
        playerInput = new InputSystem();
        playerInput.Player.Enable();
    }

    void Update()
    {
        if (GameManager.Instance.IsPaused) return;
        
        Vector2 mouseAxis = playerInput.Player.Look.ReadValue<Vector2>() * (Time.deltaTime * sensitivity);
        
        yRotation += mouseAxis.x;

        xRotation -= mouseAxis.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerBody.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
