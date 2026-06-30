using UnityEngine;

public class MuereState : BaseNPCState
{
    public override void Construct()
    {
        motor.stateEnum = NPCState.Muere;
        motor.rb.linearVelocity = Vector3.zero;
        motor.rb.isKinematic = true;
        Debug.Log($"{name}: MUERE");
    }
}
