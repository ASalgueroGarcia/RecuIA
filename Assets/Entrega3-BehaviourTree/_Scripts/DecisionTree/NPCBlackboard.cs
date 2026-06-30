using UnityEngine;

public class NPCBlackboard : MonoBehaviour
{
    [Header("Referencias")]
    public Transform target;
    public Rigidbody targetRb;

    [Header("Vida")]
    [Range(0, 100)] public float vida = 100f;
    [Range(0, 100)] public float vidaMaxima = 100f;

    [Header("Percepcion")]
    public float visionRadius = 15f;
    public float attackRange = 2.5f;
    public LayerMask obstacleMask;

    [Header("Percepcion")]
    public bool playerEstaAtacando;
    public bool seHaAvisadoACompaneros;

    [Header("Curacion")]
    public bool tengoPocionDeCuracion;
    public bool tengoMaterialesParaPocion;
    public bool puedoVerMaterialesNecesarios;

    public bool veoAlJugador;
    public bool playerEnRange;

    private void Awake()
    {
        if (target != null)
            targetRb = target.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (target == null) return;

        var toTarget = target.position - transform.position;
        var distance = toTarget.magnitude;

        playerEnRange = distance <= attackRange;

        if (distance > visionRadius)
        {
            veoAlJugador = false;
            return;
        }

        veoAlJugador = !Physics.Raycast(transform.position, toTarget.normalized, distance, obstacleMask);
    }

    public bool VidaPorcentajeMayorOIgualA(float porcentaje)
    {
        return vidaMaxima > 0f && vida / vidaMaxima * 100f >= porcentaje;
    }

    public bool EstaMuerto() => vida <= 0f;
}
