using UnityEngine;

public class AcercarState : BaseNPCState
{
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float steeringMaxSpeed = 3f;

    public override void Construct()
    {
        motor.stateEnum = NPCState.Acercar;
        aiBehaviour.maxSpeed = maxSpeed;
        aiBehaviour.steeringMaxSpeed = steeringMaxSpeed;
        Debug.Log($"{name}: ACERCAR al jugador");
    }

    public override void FixedUpdateState()
    {
        if (blackboard.target == null) return;
        aiBehaviour.Seek(blackboard.target.position, motor.rb);
    }
}
