using UnityEngine;

public class RecogerState : BaseNPCState
{
    [SerializeField] private float maxSpeed = 4f;
    [SerializeField] private float steeringMaxSpeed = 3f;

    public override void Construct()
    {
        motor.stateEnum = NPCState.Recoger;
        aiBehaviour.maxSpeed = maxSpeed;
        aiBehaviour.steeringMaxSpeed = steeringMaxSpeed;
        Debug.Log($"{name}: RECOGER materiales");
        blackboard.tengoMaterialesParaPocion = true;
    }
}
