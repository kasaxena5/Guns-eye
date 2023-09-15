using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] float speed;
    [SerializeField] float damage;

    [Header("Prefabs")]
    [SerializeField] ParticleSystem explodePrefab;

   
    Transform target;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Initialize(Vector3 direction, float speed, Transform target)
    {
        rb.velocity = direction.normalized * speed;
        this.speed = speed;
        this.target = target;
    }

    void Update()
    {
        Vector3 seekDirection = (target.position - transform.position).normalized * speed;
        
        Vector3 steerDirection = seekDirection - rb.velocity;
        steerDirection.y = 0;
        rb.AddForce(steerDirection);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"))
        {
            DeployParticles();
            if (collision.gameObject.TryGetComponent(out PlayerController player))
            {
                player.InflictDamage(damage);
            }
        }
    }

    protected void DeployParticles()
    {
        if (explodePrefab != null)
        {
            ParticleSystem ps = Instantiate(explodePrefab, transform.position, Quaternion.identity);
            ps.Play();
        }
        Destroy(gameObject);
    }
}
