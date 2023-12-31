using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] TMP_Text ammoText;
    [SerializeField] int ammoCount;
    [SerializeField] int maxAmmo = 30;
    [SerializeField] GameObject bulletImpact;
    Transform shootPoint;
    InputSystem playerInput;
    RaycastHit hit;
    Animator animationComponent;
    AudioSource audioSource;
    private CubePattern cubePattern;
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
        AddAmmo(maxAmmo);
        cubePattern = FindObjectOfType<CubePattern>();
    }

    private void OnDisable()
    {
        playerInput.Player.Shoot.performed -= Shoot;
        playerInput.Player.Reload.performed -= Reload;
    }

    private void Reload(InputAction.CallbackContext obj)
    {
        AddAmmo(maxAmmo);
    }

    public void AddAmmo(int count)
    {
        ammoCount += count;
        ammoCount = ammoCount > maxAmmo ? maxAmmo : ammoCount;
        ammoText.text = ammoCount.ToString();
    }

    void Shoot(InputAction.CallbackContext obj)
    {
        if (ammoCount == 0) return;
        animationComponent.SetTrigger("shooting");
        audioSource.Play();
        AddAmmo(-1);
        if (!Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, 100f)) return;
        
        hit.transform.GetComponent<JumpPad>()?.ActivateJumpPad();
        hit.transform.GetComponent<DoorButton>()?.Activate();
        hit.transform.GetComponent<DoorPuzzle>()?.OpenDoor();
        hit.transform.GetComponent<Sphere>()?.DestroySphere();
        
        CubeInfo cubeInfo = hit.transform.GetComponent<CubeInfo>();
        if (cubeInfo != null)
        {
            AddToNumber addToNumber = hit.transform.GetComponent<AddToNumber>();
            if (addToNumber)
            {
                hit.transform.GetComponent<AddToNumber>()?.AddNumber(cubeInfo.cubeNumber);
                return;
            }

            if (!cubePattern.isPlayed)
                cubePattern.StartCoroutinePatternGame();
            else
            {
                int order = cubeInfo.cubeNumber;
                cubePattern.ActivateCube(hit.transform, order + 1);
            }
        }
    
        
        GameObject clone = Instantiate(bulletImpact, hit.point, Quaternion.identity);
        Destroy(clone, 3f);
        if (hit.rigidbody)
        {
            //hit.rigidbody.maxLinearVelocity = 20;
            //hit.rigidbody.AddForce(shootPoint.forward * 500);
        }
    }
}
