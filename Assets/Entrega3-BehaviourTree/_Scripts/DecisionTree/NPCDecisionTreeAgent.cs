using UnityEngine;

[RequireComponent(typeof(NPCBlackboard), typeof(AIBehaviour), typeof(Rigidbody))]
public class NPCDecisionTreeAgent : MonoBehaviour
{
    [SerializeField] private NPCBlackboard blackboard;
    [SerializeField] private AIBehaviour aiBehaviour;
    [SerializeField] private Rigidbody rb;

    [Header("Velocidades")]
    [SerializeField] private float patrolSpeed = 3f;
    [SerializeField] private float seekSpeed = 5f;
    [SerializeField] private float pursueSpeed = 6f;
    [SerializeField] private float evadeSpeed = 7f;
    [SerializeField] private float steeringSpeed = 3f;

    private DecisionTreeNode root;

    private void Awake()
    {
        blackboard = GetComponent<NPCBlackboard>();
        aiBehaviour = GetComponent<AIBehaviour>();
        rb = GetComponent<Rigidbody>();
        root = BuildTree();
    }

    private void Update()
    {
        root.Evaluate();
    }

    private DecisionTreeNode BuildTree()
    {
        var muere = new ActionNode("Muere", Muere);
        var avisar = new ActionNode("Avisar", Avisar);
        var acercar = new ActionNode("Acercar", Acercar);
        var evadir = new ActionNode("Evadir", Evadir);
        var atacar = new ActionNode("Atacar", Atacar);
        var patrol = new ActionNode("Patrol", Patrol);
        var curar = new ActionNode("Curar", Curar);
        var crafteoPocion = new ActionNode("CrafteoPocion", CrafteoPocion);
        var recoger = new ActionNode("Recoger", Recoger);

        var playerAtacandoQ = new QuestionNode(() => blackboard.playerEstaAtacando, evadir, atacar);
        var playerEnRangeQ = new QuestionNode(() => blackboard.playerEnRange, playerAtacandoQ, acercar);
        var seHaAvisadoQ = new QuestionNode(() => blackboard.seHaAvisadoACompaneros, playerEnRangeQ, avisar);

        var puedoVerMaterialesQ = new QuestionNode(() => blackboard.puedoVerMaterialesNecesarios, recoger, patrol);
        var tengoMaterialesQ = new QuestionNode(() => blackboard.tengoMaterialesParaPocion, crafteoPocion, puedoVerMaterialesQ);
        var tengoPocionQ = new QuestionNode(() => blackboard.tengoPocionDeCuracion, curar, tengoMaterialesQ);
        var vidaMayorIgual50Q = new QuestionNode(() => blackboard.VidaPorcentajeMayorOIgualA(50f), patrol, tengoPocionQ);

        var veoJugadorQ = new QuestionNode(() => blackboard.veoAlJugador, seHaAvisadoQ, vidaMayorIgual50Q);

        return new QuestionNode(() => blackboard.EstaMuerto(), muere, veoJugadorQ);
    }

    private void Muere()
    {
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
        Debug.Log($"{name}: MUERE");
    }

    private void Avisar()
    {
        rb.linearVelocity = Vector3.zero;
        blackboard.seHaAvisadoACompaneros = true;
        Debug.Log($"{name}: AVISAR a compañeros");
    }

    private void Acercar()
    {
        SetSpeed(seekSpeed);
        aiBehaviour.Seek(blackboard.target.position, rb);
    }

    private void Evadir()
    {
        SetSpeed(evadeSpeed);
        aiBehaviour.Evade(blackboard.target.position, rb, blackboard.targetRb);
    }

    private void Atacar()
    {
        SetSpeed(pursueSpeed);
        aiBehaviour.Pursue(blackboard.target.position, rb, blackboard.targetRb);
    }

    private void Patrol()
    {
        SetSpeed(patrolSpeed);
        aiBehaviour.Wander(rb);
    }

    private void Curar()
    {
        rb.linearVelocity = Vector3.zero;
        blackboard.vida = Mathf.Min(blackboard.vida + 10f * Time.deltaTime, blackboard.vidaMaxima);
        if (blackboard.vida >= blackboard.vidaMaxima)
            blackboard.tengoPocionDeCuracion = false;
    }

    private void CrafteoPocion()
    {
        rb.linearVelocity = Vector3.zero;
        blackboard.tengoMaterialesParaPocion = false;
        blackboard.tengoPocionDeCuracion = true;
        Debug.Log($"{name}: CRAFTEO POCION");
    }

    private void Recoger()
    {
        rb.linearVelocity = Vector3.zero;
        blackboard.tengoMaterialesParaPocion = true;
        Debug.Log($"{name}: RECOGER materiales");
    }

    private void SetSpeed(float speed)
    {
        aiBehaviour.maxSpeed = speed;
        aiBehaviour.steeringMaxSpeed = steeringSpeed;
    }
}
