using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;

    private Rigidbody _rb;
    private Transform _target; // solo para comprobar el impacto

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
    }

    public void Launch(Vector3 explicitTarget, Transform target, float speed)
    {
        _target = target;

        var direction = (explicitTarget - transform.position).normalized;
        _rb.linearVelocity = direction * speed;
        transform.forward = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform != _target && (_target == null || !other.transform.IsChildOf(_target))) return;
        
        var health = other.GetComponent<EnemyHealth>();
        if (health != null)
            health.TakeDamage(1);

        Destroy(gameObject);
    }
}