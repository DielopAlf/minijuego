using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text vidasText;
    public Text tiempoRestanteText;

    void Start()
    {
        // Inicializa los textos con valores predeterminados o vacíos
        ActualizarVidas(0);
        ActualizarTiempo(0f);
    }

    public void ActualizarVidas(int cantidadVidas)
    {
        vidasText.text = "Vidas: " + cantidadVidas.ToString();
    }

    public void ActualizarTiempo(float tiempoRestante)
    {
        tiempoRestanteText.text = "Tiempo Restante: " + tiempoRestante.ToString("F1"); // Formatea el tiempo a un decimal
    }
}
