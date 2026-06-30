using UnityEngine;

public class AIFleeState : BaseState
{
    [SerializeField] private float fleeMaxSpeed;
    [SerializeField] private float steeringMaxSpeed;
    [SerializeField] private float targetRadius = 3f;

    public override void Transition()
    {
        if(m_enemyAIStateMotor.stateEnum == AIState.Seek)
            m_enemyAIStateMotor.ChangeState(GetComponent<AISeekState>());
    }

    public override void FixedUpdateState()
    {
        var rb = m_enemyAIStateMotor.rb;
        var target = m_enemyAIStateMotor.target;

        var desiredVelocity = transform.position - target.position;
        var dist = desiredVelocity.magnitude;

        if (dist < targetRadius)
        {
            GameManager.Instance.Tag();
            desiredVelocity = Vector3.zero;
        }
        else
        {
            desiredVelocity = desiredVelocity.normalized * fleeMaxSpeed;
        }
    
        var force = desiredVelocity - rb.linearVelocity;

        force = Vector3.ClampMagnitude(force, steeringMaxSpeed);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity + force, fleeMaxSpeed);

        if (rb.linearVelocity.sqrMagnitude > 0.01f) transform.forward = rb.linearVelocity;
    }
}

