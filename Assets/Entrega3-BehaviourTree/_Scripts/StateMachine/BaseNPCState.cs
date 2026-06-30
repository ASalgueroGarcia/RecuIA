using UnityEngine;

[RequireComponent(typeof(NPCStateMachineMotor))]
[RequireComponent(typeof(NPCBlackboard))]
public abstract class BaseNPCState : MonoBehaviour
{
    [SerializeField] protected NPCStateMachineMotor motor;
    [SerializeField] protected NPCBlackboard blackboard;
    [SerializeField] protected AIBehaviour aiBehaviour;

    private void Awake()
    {
        motor = GetComponent<NPCStateMachineMotor>();
        blackboard = GetComponent<NPCBlackboard>();
        aiBehaviour = GetComponent<AIBehaviour>();
    }

    public virtual void Construct() { }

    public virtual void Destruct() { }

    public virtual void UpdateState() { }

    public virtual void FixedUpdateState() { }

    public void Transition()
    {
        var nextState = NPCDecisionLogic.Evaluate(blackboard);
        motor.ChangeState(motor.GetState(nextState));
    }
}
