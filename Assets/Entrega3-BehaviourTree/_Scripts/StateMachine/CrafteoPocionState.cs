using UnityEngine;

public class CrafteoPocionState : BaseNPCState
{
    public override void Construct()
    {
        motor.stateEnum = NPCState.CrafteoPocion;
        motor.rb.linearVelocity = Vector3.zero;
        blackboard.tengoMaterialesParaPocion = false;
        blackboard.tengoPocionDeCuracion = true;
        Debug.Log($"{name}: CRAFTEO POCION");
    }
}
