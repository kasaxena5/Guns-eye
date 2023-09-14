using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class BulletController : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private Event bulletDestroyed;
    

    // Required components
    private CharacterController controller;
    private Transform cameraTransform;
    private Canvas bulletCanvas;
    private float remainingLife;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        StartCoroutine(UpdateBulletLifetime());
    }

    IEnumerator UpdateBulletLifetime()
    {
        remainingLife = lifetime;
        while(remainingLife >= 0)
        {
            remainingLife -= Time.deltaTime;
            yield return null;
        }

        bulletDestroyed.RaiseEvent();
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject != null)
        {
            bulletDestroyed.RaiseEvent();
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        // Move the player forward
        controller.Move(cameraTransform.forward.normalized * Time.deltaTime * playerSpeed);

        // Rotate towards camera direction
        float targetAngle = cameraTransform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed);

    }
}
