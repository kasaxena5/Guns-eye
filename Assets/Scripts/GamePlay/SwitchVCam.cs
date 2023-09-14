using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchVCam : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private int priorityBoostAmount = 10;

    [SerializeField] private Canvas thirdPersonCanvas;
    [SerializeField] private Canvas aimCanvas;

    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera thirdPersonVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera shootVirtualCamera;

    private InputAction aimAction;
    private InputAction shootAction;


    private void Awake()
    {
        aimAction = playerInput.actions["Aiming"];
        shootAction = playerInput.actions["Shoot"];
    }

    private void OnEnable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
    }

    private void OnDisable()
    {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => CancelAim();
    }

    private void SetCanvas()
    {
        int maxPriority = Mathf.Max(shootVirtualCamera.Priority, aimVirtualCamera.Priority, thirdPersonVirtualCamera.Priority);

        if(maxPriority == shootVirtualCamera.Priority)
        {
            aimCanvas.enabled = false;
            thirdPersonCanvas.enabled = false;
        } else if(maxPriority == aimVirtualCamera.Priority)
        {
            aimCanvas.enabled = true;
            thirdPersonCanvas.enabled = false;
        }
        else
        {
            aimCanvas.enabled = false;
            thirdPersonCanvas.enabled = true;
        }

    }
    
    public void StartShoot()
    {
        shootVirtualCamera.Priority += 3 * priorityBoostAmount;
        SetCanvas();
    }

    public void CancelShoot()
    {
        shootVirtualCamera.Priority -= 3 * priorityBoostAmount;
        SetCanvas();
    }

    private void StartAim()
    {
        aimVirtualCamera.Priority += priorityBoostAmount;
        SetCanvas();
    }

    private void CancelAim()
    {
        aimVirtualCamera.Priority -= priorityBoostAmount;
        SetCanvas();
    }
}
