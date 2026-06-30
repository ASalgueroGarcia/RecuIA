using UnityEngine;

public class AtacarState : BaseNPCState
{
    [SerializeField] private float maxSpeed = 6f;
    [SerializeField] private float steeringMaxSpeed = 4f;

    public override void Construct()
    {
        motor.stateEnum = NPCState.Atacar;
        aiBehaviour.maxSpeed = maxSpeed;
        aiBehaviour.steeringMaxSpeed = steeringMaxSpeed;
        Debug.Log($"{name}: ATACAR al jugador");
    }

    public override void FixedUpdateState()
    {
        if (blackboard.target == null || blackboard.targetRb == null) return;
        aiBehaviour.Pursue(blackboard.target.position, motor.rb, blackboard.targetRb);
    }
}
