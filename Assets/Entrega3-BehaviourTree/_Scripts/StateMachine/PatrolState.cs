using UnityEngine;

public class PatrolState : BaseNPCState
{
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float steeringMaxSpeed = 2f;

    public override void Construct()
    {
        motor.stateEnum = NPCState.Patrol;
        aiBehaviour.maxSpeed = maxSpeed;
        aiBehaviour.steeringMaxSpeed = steeringMaxSpeed;
        Debug.Log($"{name}: PATROL");
    }

    public override void FixedUpdateState()
    {
        aiBehaviour.Wander(motor.rb);
    }
}
