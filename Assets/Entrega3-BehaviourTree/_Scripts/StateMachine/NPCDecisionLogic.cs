/// <summary>
/// Misma logica del Decision Tree (NPCDecisionTreeAgent) pero como una funcion pura que
/// devuelve el estado al que debe transicionar la State Machine. Evita duplicar las condiciones.
/// </summary>
public static class NPCDecisionLogic
{
    public static NPCState Evaluate(NPCBlackboard blackboard)
    {
        if (blackboard.EstaMuerto())
            return NPCState.Muere;

        if (blackboard.veoAlJugador)
        {
            if (!blackboard.seHaAvisadoACompaneros)
                return NPCState.Avisar;

            if (!blackboard.playerEnRange)
                return NPCState.Acercar;

            return blackboard.playerEstaAtacando ? NPCState.Evadir : NPCState.Atacar;
        }

        if (blackboard.VidaPorcentajeMayorOIgualA(50f))
            return NPCState.Patrol;

        if (blackboard.tengoPocionDeCuracion)
            return NPCState.Curar;

        if (blackboard.tengoMaterialesParaPocion)
            return NPCState.CrafteoPocion;

        return blackboard.puedoVerMaterialesNecesarios ? NPCState.Recoger : NPCState.Patrol;
    }
}
