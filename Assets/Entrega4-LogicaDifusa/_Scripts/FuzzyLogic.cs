using UnityEngine;

public class FuzzyLogic : MonoBehaviour
{
    [Header("Ruido")]
    [SerializeField] private AnimationCurve callado;
    [SerializeField] private AnimationCurve medio;
    [SerializeField] private AnimationCurve ruidoso;

    [Header("Luz")]
    [SerializeField] private AnimationCurve oscuro;
    [SerializeField] private AnimationCurve dificilDeVer;
    [SerializeField] private AnimationCurve visible;
    [SerializeField] private AnimationCurve brillante;

    [Header("Rangos maximos")]
    [SerializeField] private float ruidoMaximo = 100f;
    [SerializeField] private float luzMaxima = 100f;

    [Header("Valores de salida (Deteccion)")]
    [SerializeField] private float dataBaja = 10f;
    [SerializeField] private float dataMedia = 50f;
    [SerializeField] private float dataAlta = 90f;

    private float _calladoValor, _medioRuidoValor, _ruidosoValor;
    private float _oscuroValor, _dificilDeVerValor, _visibleValor, _brillanteValor;
    private float _bajaValor, _mediaValor, _altaValor;

    public float deteccionValor { get; private set; }

    public void Evaluate(float ruidoActual, float luzActual)
    {
        var inputRuido = ruidoActual / ruidoMaximo;
        var inputLuz = luzActual / luzMaxima;

        _calladoValor = Mathf.Clamp(callado.Evaluate(inputRuido), 0, 1);
        _medioRuidoValor = Mathf.Clamp(medio.Evaluate(inputRuido), 0, 1);
        _ruidosoValor = Mathf.Clamp(ruidoso.Evaluate(inputRuido), 0, 1);

        _oscuroValor = Mathf.Clamp(oscuro.Evaluate(inputLuz), 0, 1);
        _dificilDeVerValor = Mathf.Clamp(dificilDeVer.Evaluate(inputLuz), 0, 1);
        _visibleValor = Mathf.Clamp(visible.Evaluate(inputLuz), 0, 1);
        _brillanteValor = Mathf.Clamp(brillante.Evaluate(inputLuz), 0, 1);

        _bajaValor = EvaluateLowTable();
        _mediaValor = EvaluateMediumTable();
        _altaValor = EvaluateHighTable();

        deteccionValor = (_bajaValor * dataBaja + _mediaValor * dataMedia + _altaValor * dataAlta)
                        / (_bajaValor + _mediaValor + _altaValor);
    }

    private float EvaluateLowTable()
    {
        return Mathf.Max(
            Mathf.Min(_calladoValor, _oscuroValor),
            Mathf.Min(_calladoValor, _dificilDeVerValor),
            Mathf.Min(_medioRuidoValor, _oscuroValor)
        );
    }

    private float EvaluateMediumTable()
    {
        return Mathf.Max(
            Mathf.Min(_calladoValor, _visibleValor),
            Mathf.Min(_calladoValor, _brillanteValor),
            Mathf.Min(_medioRuidoValor, _dificilDeVerValor),
            Mathf.Min(_medioRuidoValor, _visibleValor),
            Mathf.Min(_ruidosoValor, _oscuroValor)
        );
    }

    private float EvaluateHighTable()
    {
        return Mathf.Max(
            Mathf.Min(_medioRuidoValor, _brillanteValor),
            Mathf.Min(_ruidosoValor, _dificilDeVerValor),
            Mathf.Min(_ruidosoValor, _visibleValor),
            Mathf.Min(_ruidosoValor, _brillanteValor)
        );
    }
}
