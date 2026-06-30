using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Turn State")]
    public bool isPlayerIt = false;

    [Header("Tag Settings")]
    [SerializeField] private float tagCooldown = 1f;
    private bool m_canTag = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Tag()
    {
        if (!m_canTag) return;

        isPlayerIt = !isPlayerIt;
        m_canTag = false;
        Invoke(nameof(ResetCooldown), tagCooldown);

        Debug.Log(isPlayerIt ? "¡El jugador pilla! La IA debe huir." : "¡La IA pilla! La IA debe perseguir.");
    }

    private void ResetCooldown() => m_canTag = true;
}