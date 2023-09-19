using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text tiempoText; // Texto que muestra el tiempo restante
    public Text vidasText; // Texto que muestra las vidas restantes
    public Text resultadoText; // Texto que muestra el resultado (victoria o derrota)

    void Start()
    {
        // Inicializar la UI
        tiempoText.text = "Tiempo: ";
        vidasText.text = "Vidas: ";
        resultadoText.text = "";
    }

    public void ActualizarTiempo(float tiempoRestante)
    {
        tiempoText.text = "Tiempo: " + Mathf.Round(tiempoRestante).ToString();
    }

    public void ActualizarVidas(int vidas)
    {
        vidasText.text = "Vidas: " + vidas.ToString();
    }

    public void MostrarResultado(bool victoria)
    {
        if (victoria)
        {
            resultadoText.text = "¡Victoria!";
        }
        else
        {
            resultadoText.text = "¡Derrota!";
        }
    }
}
