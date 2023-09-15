using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] float forceMultiplier;

    [Header("Prefabs")]
    [SerializeField] ParticleSystem explodePrefab;

   
    Transform target;
    Vector3 velocity;

    void Awake()
    {
    }

    public void Initialize(Vector3 direction, float speed, Transform target)
    {
        velocity = direction.normalized * speed;
        this.speed = speed;
        this.target = target;
    }

    void Update()
    {
        Vector3 seekDirection = (target.position - transform.position).normalized * speed;
        
        Vector3 steerDirection = seekDirection - velocity;
        steerDirection.y = 0;
        velocity += steerDirection * forceMultiplier * Time.deltaTime;

        transform.Translate(velocity * Time.deltaTime);
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
