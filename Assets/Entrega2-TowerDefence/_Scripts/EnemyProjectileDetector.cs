using UnityEngine;

public class EnemyProjectileDetector : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 4f;
    [SerializeField] private LayerMask projectileLayer;
    private EnemyAIStateMotor _motor;

    private void Awake() => _motor = GetComponent<EnemyAIStateMotor>();

    private void Update()
    {
        var hits = Physics.OverlapSphere(transform.position, detectionRadius, projectileLayer);
        if (hits.Length > 0)
        {
            _motor.target = hits[0].transform;
            _motor.targetRb = hits[0].GetComponent<Rigidbody>();
            _motor.stateEnum = AIState.Evade;
        }
        else if (_motor.stateEnum == AIState.Evade)
        {
            _motor.target = null;
            _motor.targetRb = null;
            _motor.stateEnum = AIState.FollowPath;
        }
    }
}