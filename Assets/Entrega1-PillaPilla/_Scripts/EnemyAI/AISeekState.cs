using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISeekState : BaseState
{
    [SerializeField] private float seekMaxSpeed;
    [SerializeField] private float steeringMaxSpeed;
    [SerializeField] private float targetRadius = 3f;
    
    public override void Construct()
    {
        
    }

    public override void Transition()
    {
        if(m_enemyAIStateMotor.stateEnum == AIState.Flee)
            m_enemyAIStateMotor.ChangeState(GetComponent<AIFleeState>());
        /*if (m_enemyAIStateMotor.isPlayerOnSight) return;
        m_enemyAIStateMotor.ChangeState(GetComponent<AIPatrolState>());*/
    }

    public override void FixedUpdateState()
    {
        var rb = m_enemyAIStateMotor.rb;
        var target = m_enemyAIStateMotor.target;

        var desiredVelocity = target.position - transform.position;
        var dist = desiredVelocity.magnitude;

        if (dist < targetRadius)
        {
            GameManager.Instance.Tag();
            desiredVelocity = Vector3.zero;
        }
        else
        {
            desiredVelocity = desiredVelocity.normalized * seekMaxSpeed;
        }
    
        var force = desiredVelocity - rb.linearVelocity;

        force = Vector3.ClampMagnitude(force, steeringMaxSpeed);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity + force, seekMaxSpeed);

        if (rb.linearVelocity.sqrMagnitude > 0.01f) transform.forward = rb.linearVelocity;
    }
}
