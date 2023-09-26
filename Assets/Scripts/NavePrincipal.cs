using UnityEngine;
using UnityEngine.SceneManagement;

public class NavePrincipal : MensajeManager
{
    [SerializeField] KeyCode botonDerecha;
    [SerializeField] KeyCode botonIzquierda;
    [SerializeField] KeyCode botonDisparo;
    public float Speed = 10f;
    public float maxdistancia;
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float frecuenciaDisparo = 0.5f;
    private float tiempoUltimoDisparo;

    private float tiempoInicial;
    private float tiempoTranscurrido;

    private int vidas = 3;
    public GameObject panelDerrota;
    public GameObject panelVictoria;

    public float tiempoLimiteDestrucionEnemigas = 60f;
    private bool juegoTerminado = false;

    private int navesEnemigasDestruidas = 0;
    public int totalNavesEnemigas = 0; // Se actualizará en Start

    private int nivelActual = 1;
    [SerializeField] private int numeroMaximoNiveles = 3;

    private int nivelCompletado;

    public GameObject[] navesEnemigas; // Arreglo para almacenar las naves enemigas

    private bool perdio = false; // Nueva variable para verificar si el jugador perdió

    // Variables para los sonidos
    public AudioClip fondoAudioClip; // Sonido de fondo
    public AudioClip disparoAudioClip; // Sonido de disparo de la nave principal
    public AudioClip victoriaAudioClip; // Sonido de victoria
    public AudioClip derrotaAudioClip; // Sonido de derrota
    public AudioClip choqueAudioClip; // Sonido de choque
    public AudioClip destruccionEnemigaAudioClip; // Sonido de destrucción de naves enemigas
    public AudioClip clipSonidoFondo; // Variable para el sonido de fondo
    public AudioSource audioSource;
    // Variable para el tiempo restante del nivel
    private float tiempoRestanteNivel;

    void Start()
    {
        tiempoInicial = Time.time;

        nivelActual = PlayerPrefs.GetInt("NivelActual", 1);
        nivelCompletado = PlayerPrefs.GetInt("Nivel" + nivelActual + "Completado", 0);

        // Buscar todas las naves enemigas y almacenarlas en el arreglo
        navesEnemigas = GameObject.FindGameObjectsWithTag("NaveEnemiga");
        totalNavesEnemigas = navesEnemigas.Length;

        // Mostrar el mensaje de ataque antes de comenzar
        MostrarMensajeAtaque(3f); // Utiliza la función heredada

        // Calcular el tiempo restante del nivel
        tiempoRestanteNivel = tiempoLimiteDestrucionEnemigas;
        audioSource = GetComponent<AudioSource>();
        // Configura el AudioSource para el sonido de fondo
       // fondoAudioClip = GetComponent<AudioSource>();
        //fondoAudioClip.clip = clipSonidoFondo; // Asigna el clip de sonido de fondo
        //fondoAudioClip.Play(); // Reproduce el sonido de fondo al iniciar el nivel
    }

    void Update()
    {
        if (!juegoTerminado)
        {
            if (Input.GetKey(botonDerecha) && transform.position.x < maxdistancia)
            {
                transform.Translate(Vector3.right * Time.deltaTime * Speed);
            }

            if (Input.GetKey(botonIzquierda) && transform.position.x > -maxdistancia)
            {
                transform.Translate(Vector3.left * Time.deltaTime * Speed);
            }

            if (Input.GetKey(botonDisparo) && Time.time - tiempoUltimoDisparo >= frecuenciaDisparo)
            {
                Disparar();
                tiempoUltimoDisparo = Time.time;

                // Reproduce el sonido de disparo al presionar el botón de disparo
                audioSource.PlayOneShot(disparoAudioClip);
            }
        }

        // Actualizar el tiempo restante del nivel
        tiempoRestanteNivel = tiempoLimiteDestrucionEnemigas - (Time.time - tiempoInicial);

        if (tiempoRestanteNivel <= 0 && !juegoTerminado)
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
            proyectil.tag = "ProyectilJugador";
        }
    }

    public void RecibirDaño(int cantidadDeDaño)
    {
        vidas -= cantidadDeDaño;

        if (vidas <= 0 && !perdio)
        {
            // Reproduce el sonido de choque al recibir daño
            audioSource.PlayOneShot(choqueAudioClip);

            MostrarPantallaDerrota();
            perdio = true;
        }
    }

    private void MostrarPantallaDerrota()
    {
        // Detiene el sonido de fondo al mostrar la pantalla de derrota
      //  fondoAudioClip.Stop();

        // Reproduce el sonido de derrota
        audioSource.PlayOneShot(derrotaAudioClip);

        panelDerrota.SetActive(true);
        Time.timeScale = 0f;
        Speed = 0f;
    }

    private void MostrarVictoria()
    {
        // Detiene el sonido de fondo al mostrar la pantalla de victoria
        //fondoAudioSource.Stop();

        // Reproduce el sonido de victoria
        audioSource.PlayOneShot(victoriaAudioClip);

        panelVictoria.SetActive(true);
        Time.timeScale = 0f;

        // Desbloquear el siguiente nivel
        PlayerPrefs.SetInt("Nivel" + (nivelActual + 1) + "Completado", 1);
        PlayerPrefs.Save();
    }

    private void VerificarVictoria()
    {
        if (navesEnemigasDestruidas >= totalNavesEnemigas)
        {
            if (nivelActual < numeroMaximoNiveles)
            {
                nivelActual++;
                PlayerPrefs.SetInt("NivelActual", nivelActual);
                PlayerPrefs.Save();

                CargarSiguienteNivel();
            }
            else
            {
                MostrarVictoriaFinal();
            }
            juegoTerminado = true;
        }
    }

    private void CargarSiguienteNivel()
    {
        string nombreNivel = "Nivel" + nivelActual;
        SceneManager.LoadScene(nombreNivel);
    }

    private void MostrarVictoriaFinal()
    {
        Debug.Log("¡Has completado todos los niveles!");
    }

    public void ReiniciarNivel()
    {
        // Restablecer el tiempo a 1 (velocidad normal)
        Time.timeScale = 1;

        // Restablecer el tiempo límite de destrucción de enemigos
        tiempoLimiteDestrucionEnemigas = Time.time + 60f;

        // Restablecer la variable de tiempo restante del nivel
        tiempoRestanteNivel = tiempoLimiteDestrucionEnemigas;

        // Restablecer la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VolverAlMenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void NaveEnemigaDestruida()
    {
        navesEnemigasDestruidas++;

        if (navesEnemigasDestruidas >= totalNavesEnemigas)
        {
            // Reproduce el sonido de destrucción de naves enemigas
            audioSource.PlayOneShot(destruccionEnemigaAudioClip);

            MostrarVictoria();
            juegoTerminado = true;
        }
    }
}
