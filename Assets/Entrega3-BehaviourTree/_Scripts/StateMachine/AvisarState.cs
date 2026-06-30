using UnityEngine;

public class AvisarState : BaseNPCState
{
    public override void Construct()
    {
        motor.stateEnum = NPCState.Avisar;
        motor.rb.linearVelocity = Vector3.zero;
        blackboard.seHaAvisadoACompaneros = true;
        Debug.Log($"{name}: AVISAR a compañeros");
    }
}
