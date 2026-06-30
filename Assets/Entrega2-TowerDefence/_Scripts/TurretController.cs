using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float detectionRadius = 8f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Shooting")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float projectileSpeed = 15f;

    private float _cooldown;
    private Transform _currentTarget;
    private Rigidbody _currentTargetRb;

    private void Update()
    {
        _cooldown -= Time.deltaTime;
        FindTarget();

        if (_currentTarget == null) return;
        transform.LookAt(_currentTarget.position);

        if (!(_cooldown <= 0f)) return;
        Shoot();
        _cooldown = 1f / fireRate;
    }

    private void FindTarget()
    {
        var hits = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
        if (hits.Length == 0)
        {
            _currentTarget = null;
            _currentTargetRb = null;
            return;
        }

        Transform closest = null;
        var closestSqr = float.MaxValue;
        foreach (var hit in hits)
        {
            var sqr = (hit.transform.position - transform.position).sqrMagnitude;
            if (!(sqr < closestSqr)) continue;
            
            closestSqr = sqr;
            closest = hit.transform;
        }
        _currentTarget = closest;
        if (closest != null) _currentTargetRb = closest.GetComponent<Rigidbody>();
    }

    private void Shoot()
    {
        var explicitTarget = PredictTargetPosition();

        var projGo = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        var proj = projGo.GetComponent<Projectile>();
        proj.Launch(explicitTarget, _currentTarget, projectileSpeed);
    }

    private Vector3 PredictTargetPosition()
    {
        var distanceToTarget = _currentTarget.position - firePoint.position;
        var distance = distanceToTarget.magnitude;

        if (_currentTargetRb == null || _currentTargetRb.linearVelocity.sqrMagnitude < 0.01f)
            return _currentTarget.position; // sin movimiento, Seek normal

        var prediction = distance / projectileSpeed;
        return _currentTarget.position + _currentTargetRb.linearVelocity * prediction;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}