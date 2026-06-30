using UnityEngine;

public class CurarState : BaseNPCState
{
    [SerializeField] private float curacionPorSegundo = 10f;

    public override void Construct()
    {
        motor.stateEnum = NPCState.Curar;
        motor.rb.linearVelocity = Vector3.zero;
        Debug.Log($"{name}: CURAR");
    }

    public override void UpdateState()
    {
        blackboard.vida = Mathf.Min(blackboard.vida + curacionPorSegundo * Time.deltaTime, blackboard.vidaMaxima);
        blackboard.tengoPocionDeCuracion = blackboard.vida < blackboard.vidaMaxima;
    }
}
