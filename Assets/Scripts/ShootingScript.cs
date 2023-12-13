using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] TMP_Text ammoText;
    [SerializeField] int ammoCount = 30;
    [SerializeField] GameObject bulletImpact;
    Transform shootPoint;
    InputSystem playerInput;
    RaycastHit hit;
    Animator animationComponent;
    AudioSource audioSource;
    void Awake()
    {
        playerInput = new InputSystem();
        playerInput.Player.Enable();
        playerInput.Player.Shoot.performed += Shoot;
        playerInput.Player.Reload.performed += Reload;
        ammoText.text = ammoCount.ToString();
        animationComponent = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        shootPoint = Camera.main?.transform;
    }

    private void OnDisable()
    {
        playerInput.Player.Shoot.performed -= Shoot;
        playerInput.Player.Reload.performed -= Reload;
    }

    private void Reload(InputAction.CallbackContext obj)
    {
        ammoCount = 30;
        ammoText.text = ammoCount.ToString();
    }

    public void AddAmmo(int count)
    {
        ammoCount += count;
        ammoText.text = ammoCount.ToString();
    }

    void Shoot(InputAction.CallbackContext obj)
    {
        if (ammoCount == 0) return;
        animationComponent.SetTrigger("shooting");
        audioSource.Play();
        ammoCount--;
        ammoText.text = ammoCount.ToString();
        if (!Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, 100f)) return;
        
        hit.transform.GetComponent<JumpPad>()?.ActivateJumpPad();
        hit.transform.GetComponent<DoorButton>()?.Activate();
        hit.transform.GetComponent<DoorPuzzle>()?.OpenDoor();
            
        GameObject clone = Instantiate(bulletImpact, hit.point, Quaternion.identity);
        Destroy(clone, 3f);
        if (hit.rigidbody)
        {
            //hit.rigidbody.maxLinearVelocity = 20;
            //hit.rigidbody.AddForce(shootPoint.forward * 500);
        }
    }
}
