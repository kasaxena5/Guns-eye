using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float cooldownRate;
    [SerializeField] private CinemachineVirtualCamera shootVirtualCamera;
    [SerializeField] private Transform barrelTransform;
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private ParticleSystem barrelExplosion;
    [SerializeField] private Event bulletSpawned;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] BulletStats bulletStats;


    // Required components and global variables
    private CharacterController controller;
    private PlayerInput playerInput;
    private Transform cameraTransform;
    
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Coroutine cooldownCoroutine;

    // PlayerControl Actions
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];
    }

    private void OnEnable()
    {
        shootAction.performed += _ => ShootGun();
    }

    private void OnDisable()
    {
        shootAction.performed -= _ => ShootGun();
    }

    private void ShootGun()
    {
        if (bulletStats.cooldown > 0.9f)
        {
            barrelExplosion.Play();
            BulletController bullet = Instantiate(bulletPrefab, barrelTransform);
            shootVirtualCamera.Follow = bullet.gameObject.transform;
            shootVirtualCamera.LookAt = bullet.gameObject.transform;
            bulletSpawned.RaiseEvent();
            if(cooldownCoroutine != null)
                StopCoroutine(cooldownCoroutine);
            cooldownCoroutine = StartCoroutine(ResetCooldown());
        }
    }

    IEnumerator ResetCooldown()
    {
        bulletStats.cooldown = 0;
        while(bulletStats.cooldown < 1)
        {
            bulletStats.cooldown += Time.deltaTime * cooldownRate;
            yield return null;
        }
        bulletStats.cooldown = 1;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);


        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Rotate towards camera direction
        float targetAngle = cameraTransform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed);
    }

    public void InflictDamage(float damage)
    {
        playerStats.health = Mathf.Max(playerStats.health - damage, 0);
        if (playerStats.health <= 0)
        {
            if (SceneLoader.Instance.currentScene == "SandboxScene")
            {
                SceneLoader.Instance.currentScene = "GameOverMenuScene";
                SceneLoader.Instance.StartScene();
            }
        }
    }
}