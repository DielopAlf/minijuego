using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

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

    public TextMeshProUGUI tiempoRestanteText;

    private bool perdio = false;

    // Variables para los sonidos
    public AudioClip fondoAudioClip; // Sonido de fondo
    public AudioClip disparoAudioClip; // Sonido de disparo de la nave principal
    public AudioClip victoriaAudioClip; // Sonido de victoria
    public AudioClip derrotaAudioClip; // Sonido de derrota
    public AudioClip choqueAudioClip; // Sonido de choque con proyectil enemigo
    public AudioClip choqueNaveEnemigaAudioClip; // Sonido de choque con nave enemiga
    public AudioClip destruccionEnemigaAudioClip; // Sonido de destrucción de naves enemigas
    public AudioClip clipSonidoFondo; // Variable para el sonido de fondo
    public AudioSource audioSource;

    private float tiempoRestanteNivel;

    public GameObject vidaPrefab; // Prefab del sprite de vida
    public Transform vidaParent; // Transform del objeto padre para los sprites de vida
    private List<GameObject> vidasVisuales = new List<GameObject>(); // Lista de sprites de vida visuales

    void Start()
    {
        tiempoInicial = Time.time;

        nivelActual = PlayerPrefs.GetInt("NivelActual", 1);
        nivelCompletado = PlayerPrefs.GetInt("Nivel" + nivelActual + "Completado", 0);

        navesEnemigas = GameObject.FindGameObjectsWithTag("NaveEnemiga");
        totalNavesEnemigas = navesEnemigas.Length;

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clipSonidoFondo;
        audioSource.Play();

        InicializarVidasVisuales();
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

                audioSource.PlayOneShot(disparoAudioClip);
            }
        }

        tiempoRestanteNivel = tiempoLimiteDestrucionEnemigas - (Time.time - tiempoInicial);

        if (tiempoRestanteNivel <= 0 && !juegoTerminado)
        {
            MostrarPantallaDerrota();
            juegoTerminado = true;
        }

        tiempoRestanteText.text = "" + Mathf.FloorToInt(tiempoRestanteNivel);

    } // fin de update

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

        QuitarVidaVisual();

        if (vidas <= 0 && !perdio)
        {
            audioSource.PlayOneShot(choqueAudioClip);

            MostrarPantallaDerrota();
            perdio = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ProyectilEnemigo"))
        {
            audioSource.PlayOneShot(choqueNaveEnemigaAudioClip);

            // Resto de la lógica para manejar la colisión con un proyectil enemigo
        }
    }

    private void MostrarPantallaDerrota()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(derrotaAudioClip);

        panelDerrota.SetActive(true);
        Time.timeScale = 0f;
        Speed = 0f;
    }

    private void MostrarVictoria()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(victoriaAudioClip);

        panelVictoria.SetActive(true);
        Time.timeScale = 0f;

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
        Time.timeScale = 1;

        tiempoLimiteDestrucionEnemigas = Time.time + 60f;

        tiempoRestanteNivel = tiempoLimiteDestrucionEnemigas;

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
            audioSource.PlayOneShot(destruccionEnemigaAudioClip);

            MostrarVictoria();
            juegoTerminado = true;
        }
    }

    private void InicializarVidasVisuales()
    {
        // Asegúrate de que vidaPrefab y vidaParent estén configurados en el Inspector
        if (vidaPrefab != null && vidaParent != null)
        {
            for (int i = 0; i < vidas; i++)
            {
                Vector3 spawnPosition = new Vector3(vidaParent.position.x + i * 1.5f, vidaParent.position.y, vidaParent.position.z);
                GameObject vidaVisual = Instantiate(vidaPrefab, spawnPosition, Quaternion.identity, vidaParent);
                vidasVisuales.Add(vidaVisual);
            }
        }
    }

    private void QuitarVidaVisual()
    {
        if (vidasVisuales.Count > 0)
        {
            Destroy(vidasVisuales[vidasVisuales.Count - 1]);
            vidasVisuales.RemoveAt(vidasVisuales.Count - 1);
        }
    }
}