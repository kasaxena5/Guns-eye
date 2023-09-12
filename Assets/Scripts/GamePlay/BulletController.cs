using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class BulletController : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 5f;

    // Required components and global variables
    private CharacterController controller;
    private PlayerInput playerInput;
    private Transform cameraTransform;
    private Vector3 playerVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        controller.Move(cameraTransform.forward.normalized * Time.deltaTime * playerSpeed);



        // Rotate towards camera direction
        float targetAngle = cameraTransform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed);

    }
}
