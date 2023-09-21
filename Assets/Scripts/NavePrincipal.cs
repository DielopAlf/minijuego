using UnityEngine;
using UnityEngine.SceneManagement;

public class NavePrincipal : MonoBehaviour
{
    [SerializeField] KeyCode botonDerecha;
    [SerializeField] KeyCode botonIzquierda;
    [SerializeField] KeyCode botonDisparo;
    public float Speed = 10f;
    public float maxdistancia;
    public GameObject proyectilPrefab; // Prefab del proyectil que disparar�
    public Transform puntoDisparo; // Punto de origen del disparo
    public float frecuenciaDisparo = 0.5f; // Frecuencia de disparo en segundos
    private float tiempoUltimoDisparo;

    private int vidas = 3;
    public GameObject panelDerrota; // Panel de derrota
    public GameObject panelVictoria; // Panel de victoria

    // Agrega una variable para el tiempo l�mite de destrucci�n de las naves enemigas
    public float tiempoLimiteDestrucionEnemigas = 60f; // Tiempo en segundos

    private bool juegoTerminado = false;

    private int navesEnemigasDestruidas = 0; // Contador de naves enemigas destruidas
    public int totalNavesEnemigas = 10; // Total de naves enemigas en la escena

    void Update()
    {
        if (!juegoTerminado)
        {
            // Movimiento horizontal
            if (Input.GetKey(botonDerecha) && transform.position.x < maxdistancia)
            {
                transform.Translate(Vector3.right * Time.deltaTime * Speed);
            }

            if (Input.GetKey(botonIzquierda) && transform.position.x > -maxdistancia)
            {
                transform.Translate(Vector3.left * Time.deltaTime * Speed);
            }

            // Disparo de proyectiles al presionar el bot�n asignado
            if (Input.GetKey(botonDisparo) && Time.time - tiempoUltimoDisparo >= frecuenciaDisparo)
            {
                Disparar();
                tiempoUltimoDisparo = Time.time;
            }
        }

        // Verificar si se ha agotado el tiempo l�mite de destrucci�n de las naves enemigas
        if (Time.time >= tiempoLimiteDestrucionEnemigas && !juegoTerminado)
        {
            MostrarPantallaDerrota();
            juegoTerminado = true;
        }
    }

    void Disparar()
    {
        if (proyectilPrefab != null && puntoDisparo != null)
        {
            GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);
            proyectil.tag = "ProyectilJugador"; // Etiqueta para identificar los proyectiles del jugador
        }
    }

    public void RecibirDa�o(int cantidadDeDa�o)
    {
        vidas -= cantidadDeDa�o;

        if (vidas <= 0)
        {
            // La nave ha perdido todas sus vidas
            MostrarPantallaDerrota();
            Destroy(gameObject); // O desactiva la nave principal si quieres que siga siendo visible en la pantalla de derrota
            juegoTerminado = true;
        }
    }

    private void MostrarPantallaDerrota()
    {
        // Activa el panel de derrota en la jerarqu�a de UI
        panelDerrota.SetActive(true);

        // Pausa el juego
        Time.timeScale = 0f;
    }

    // Funci�n para verificar condiciones de victoria y mostrar pantalla de victoria
    private void VerificarVictoria()
    {
        if (navesEnemigasDestruidas >= totalNavesEnemigas)
        {
            MostrarPantallaVictoria();
            juegoTerminado = true;
        }
    }

    public void NaveEnemigaDestruida()
    {
        navesEnemigasDestruidas++;
        VerificarVictoria();
    }

    private void MostrarPantallaVictoria()
    {
        // Activa el panel de victoria en la jerarqu�a de UI
        panelVictoria.SetActive(true);

        // Pausa el juego
        Time.timeScale = 0f;
    }

    public void ReiniciarNivel()
    {
        // Ocultar el panel de derrota si est� activo
        if (panelDerrota.activeSelf)
        {
            panelDerrota.SetActive(false);
        }

        // Ocultar el panel de victoria si est� activo
        if (panelVictoria.activeSelf)
        {
            panelVictoria.SetActive(false);
        }

        

        // Restablecer la variable de juego terminado
        juegoTerminado = false;

        // Cargar de nuevo la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Restablecer la escala de tiempo del juego
        Time.timeScale = 1;
    }

    public void VolverAlMenuPrincipal()
    {
        // Cargar la escena del men� principal
        SceneManager.LoadScene("Menu");

        // Restablecer la escala de tiempo del juego
        Time.timeScale = 1;
    }

    public void CargarSiguienteNivel()
    {
        // Obtener el �ndice del nivel actual
        int indiceNivelActual = SceneManager.GetActiveScene().buildIndex;

        // Cargar el siguiente nivel en funci�n del �ndice actual
        SceneManager.LoadScene(indiceNivelActual + 1);
    }

    public void VolverAlMenuSeleccionNivel()
    {
        SceneManager.LoadScene("SeleccionNivel");
    }
}
