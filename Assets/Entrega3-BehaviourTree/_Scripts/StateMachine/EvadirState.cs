using UnityEngine;

public class EvadirState : BaseNPCState
{
    [SerializeField] private float maxSpeed = 7f;
    [SerializeField] private float steeringMaxSpeed = 5f;

    public override void Construct()
    {
        motor.stateEnum = NPCState.Evadir;
        aiBehaviour.maxSpeed = maxSpeed;
        aiBehaviour.steeringMaxSpeed = steeringMaxSpeed;
        Debug.Log($"{name}: EVADIR al jugador");
    }

    public override void FixedUpdateState()
    {
        if (blackboard.target == null || blackboard.targetRb == null) return;
        aiBehaviour.Evade(blackboard.target.position, motor.rb, blackboard.targetRb);
    }
}
