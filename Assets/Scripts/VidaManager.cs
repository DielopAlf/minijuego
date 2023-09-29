using UnityEngine;
using System.Collections.Generic;

public class VidaManager : MonoBehaviour
{
    public int vidasIniciales = 3; // Cantidad inicial de vidas
    public GameObject spriteVidaPrefab; // Prefab del sprite de vida
    public Vector3 posicionInicialVidas = new Vector3(0f, 0f, 0f); // Posici�n inicial de las vidas visuales

    private int vidas; // Variable para rastrear la cantidad actual de vidas
    private List<GameObject> vidasVisuales = new List<GameObject>(); // Lista para almacenar los sprites de vida

    private void Start()
    {
        vidas = vidasIniciales; // Configura la cantidad inicial de vidas

        // Configura las vidas visuales al inicio
        ConfigurarVidasVisuales();
    }

    // Funci�n para quitar una vida
    public void QuitarVida()
    {
        if (vidas > 0)
        {
            vidas--; // Reduce la cantidad de vidas

            // Actualiza las vidas visuales
            ActualizarVidasVisuales();

            if (vidas == 0)
            {
                // Aqu� puedes agregar l�gica adicional cuando el jugador se queda sin vidas
                Debug.Log("�Has perdido todas las vidas!");
            }
        }
    }

    // Funci�n para configurar las vidas visuales al inicio
    private void ConfigurarVidasVisuales()
    {
        for (int i = 0; i < vidasIniciales; i++)
        {
            // Instancia el sprite de vida visual
            GameObject vidaVisual = Instantiate(spriteVidaPrefab, transform);
            vidaVisual.transform.position = posicionInicialVidas + new Vector3(i * 0f, 0f, 0f);
            vidasVisuales.Add(vidaVisual); // Agrega el objeto a la lista
        }
    }

    // Funci�n para actualizar las vidas visuales
    private void ActualizarVidasVisuales()
    {
        if (vidasVisuales.Count > vidas)
        {
            // Si hay m�s vidas visuales de las que deber�a haber, destr�yelas
            Destroy(vidasVisuales[vidasVisuales.Count - 1]);
            vidasVisuales.RemoveAt(vidasVisuales.Count - 1);
        }
    }
}
