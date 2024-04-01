using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] TMP_Text ammoText;
    [SerializeField] int ammoCount;
    [SerializeField] int maxAmmo = 30;
    [SerializeField] GameObject bulletImpact;
    Transform shootPoint;
    [SerializeField] ParticleSystem particleSystemComponent;
    InputSystem playerInput;
    RaycastHit hit;
    Animator animationComponent;
    AudioSource audioSource;
    private CubePattern cubePattern;
    private static readonly int Shooting = Animator.StringToHash("shooting");
    private int differenceCount;

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
        if (ammoCount == 0 || GameManager.Instance.IsPaused) return;
        animationComponent.SetTrigger(Shooting);
        audioSource.Play();
        particleSystemComponent.Play();
        AddAmmo(-1);
        if (!Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, 200f)) return;
        
        if (hit.transform.gameObject.CompareTag("Fake"))
            Destroy(hit.transform.gameObject);

        if (hit.transform.gameObject.CompareTag("RigidbodyExplosive"))
        {
            if (!hit.rigidbody) return;
            hit.rigidbody.maxLinearVelocity = 20;
            hit.rigidbody.AddForce(shootPoint.forward * 500);
        }

        if (hit.transform.gameObject.CompareTag("Difference"))
        {
            Debug.Log("difference");
            differenceCount++;
            if (differenceCount > 5) return;
            GameObject.FindGameObjectWithTag("DifferenceDoor").SetActive(true);
        }

        hit.transform.GetComponent<JumpPad>()?.ActivateJumpPad();
        hit.transform.GetComponent<DoorButton>()?.Activate();
        hit.transform.GetComponent<DoorPuzzle>()?.OpenDoor();
        hit.transform.GetComponent<Sphere>()?.DestroySphere();
        
        CubeInfo cubeInfo = hit.transform.GetComponent<CubeInfo>();
        if (cubeInfo == null) return;
        
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
}
