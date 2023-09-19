using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Instancia est�tica del GameManager para acceder desde otros scripts

    public float tiempoLimite = 60f; // Tiempo l�mite del juego en segundos
    private float tiempoRestante; // Tiempo restante del juego
    private bool juegoTerminado = false;

    public Text tiempoText; // Texto que muestra el tiempo restante en la interfaz de usuario
    public Text resultadoText; // Texto que muestra el resultado (victoria o derrota) en la interfaz de usuario

    public GameObject naveEnemigaPrefab; // Prefab de las naves enemigas
    public Transform puntoSpawnNaveEnemiga; // Punto de inicio de las naves enemigas
    public int cantidadNavesEnemigas = 5; // Cantidad inicial de naves enemigas

    private int puntuacion = 0; // Puntuaci�n del jugador
    public Text puntuacionText; // Texto que muestra la puntuaci�n en la interfaz de usuario

    private int vidas = 3; // Vidas iniciales del jugador
    public Text vidasText; // Texto que muestra las vidas en la interfaz de usuario

    void Awake()
    {
        // Configurar la instancia �nica del GameManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Inicializar el tiempo restante
        tiempoRestante = tiempoLimite;
        ActualizarTiempoUI();

        // Iniciar el juego
        StartCoroutine(IniciarJuego());
    }

    IEnumerator IniciarJuego()
    {
        // Crear las naves enemigas iniciales
        for (int i = 0; i < cantidadNavesEnemigas; i++)
        {
            CrearNaveEnemiga();
            yield return new WaitForSeconds(1.0f); // Esperar 1 segundo entre cada creaci�n
        }

        // Iniciar el temporizador del juego
        while (tiempoRestante > 0)
        {
            yield return new WaitForSeconds(1.0f);
            tiempoRestante -= 1.0f;
            ActualizarTiempoUI();
        }

        // Finalizar el juego cuando se agote el tiempo
        if (!juegoTerminado)
        {
            juegoTerminado = true;
            FinalizarJuego(false); // El jugador pierde si se agota el tiempo
        }
    }

    void ActualizarTiempoUI()
    {
        tiempoText.text = "Tiempo: " + Mathf.Round(tiempoRestante).ToString();
    }

    void CrearNaveEnemiga()
    {
        Instantiate(naveEnemigaPrefab, puntoSpawnNaveEnemiga.position, Quaternion.identity);
    }

    public void IncrementarPuntuacion(int cantidad)
    {
        puntuacion += cantidad;
        puntuacionText.text = "Puntuaci�n: " + puntuacion.ToString();
    }

    public void ReducirVida()
    {
        vidas--;
        vidasText.text = "Vidas: " + vidas.ToString();

        if (vidas <= 0)
        {
            if (!juegoTerminado)
            {
                juegoTerminado = true;
                FinalizarJuego(false); // El jugador pierde cuando se quedan sin vidas
            }
        }
    }

    public void FinalizarJuego(bool victoria)
    {
        if (victoria)
        {
            resultadoText.text = "�Victoria!";
        }
        else
        {
            resultadoText.text = "�Derrota!";
        }

        // Puedes agregar aqu� l�gica adicional, como reiniciar el juego o mostrar un men�.
    }
}
