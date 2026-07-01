using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FuzzyLogicUI : MonoBehaviour
{
    [SerializeField] private FuzzyLogic fuzzyLogic;

    [SerializeField] private TMP_InputField ruidoInput;
    [SerializeField] private TMP_InputField luzInput;
    [SerializeField] private Button evaluarBtn;
    [SerializeField] private TMP_InputField resultadoInput;

    private void Awake()
    {
        evaluarBtn.onClick.AddListener(OnEvaluarClicked);
    }

    private void OnEvaluarClicked()
    {
        if (!float.TryParse(ruidoInput.text, out var ruido)) ruido = 0f;
        if (!float.TryParse(luzInput.text, out var luz)) luz = 0f;

        fuzzyLogic.Evaluate(ruido, luz);

        resultadoInput.text = $"{fuzzyLogic.deteccionValor:F2}";
    }
}
