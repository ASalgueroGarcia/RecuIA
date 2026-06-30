using System;
using UnityEngine;

[RequireComponent(typeof(NPCBlackboard), typeof(AIBehaviour), typeof(Rigidbody))]
[RequireComponent(typeof(MuereState), typeof(AvisarState), typeof(AcercarState))]
[RequireComponent(typeof(EvadirState), typeof(AtacarState), typeof(PatrolState))]
[RequireComponent(typeof(CurarState), typeof(CrafteoPocionState), typeof(RecogerState))]
public class NPCStateMachineMotor : MonoBehaviour
{
    [Header("Estado actual")]
    public NPCState stateEnum;

    [Header("Componentes")]
    public Rigidbody rb;
    public AIBehaviour aiBehaviour;

    private BaseNPCState currentState;

    private MuereState muereState;
    private AvisarState avisarState;
    private AcercarState acercarState;
    private EvadirState evadirState;
    private AtacarState atacarState;
    private PatrolState patrolState;
    private CurarState curarState;
    private CrafteoPocionState crafteoPocionState;
    private RecogerState recogerState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        aiBehaviour = GetComponent<AIBehaviour>();

        muereState = GetComponent<MuereState>();
        avisarState = GetComponent<AvisarState>();
        acercarState = GetComponent<AcercarState>();
        evadirState = GetComponent<EvadirState>();
        atacarState = GetComponent<AtacarState>();
        patrolState = GetComponent<PatrolState>();
        curarState = GetComponent<CurarState>();
        crafteoPocionState = GetComponent<CrafteoPocionState>();
        recogerState = GetComponent<RecogerState>();

        currentState = patrolState;
    }

    private void Start()
    {
        currentState.Construct();
    }

    private void Update()
    {
        currentState.Transition();
        currentState.UpdateState();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState();
    }

    public BaseNPCState GetState(NPCState state)
    {
        switch (state)
        {
            case NPCState.Muere: return muereState;
            case NPCState.Avisar: return avisarState;
            case NPCState.Acercar: return acercarState;
            case NPCState.Evadir: return evadirState;
            case NPCState.Atacar: return atacarState;
            case NPCState.Patrol: return patrolState;
            case NPCState.Curar: return curarState;
            case NPCState.CrafteoPocion: return crafteoPocionState;
            case NPCState.Recoger: return recogerState;
            default: throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    public void ChangeState(BaseNPCState newState)
    {
        if (newState == currentState) return;

        currentState.Destruct();
        currentState = newState;
        currentState.Construct();
    }
}
