using UnityEngine;

public class NaveEnemiga : MensajeManager
{
    public float Velocidad;
    public int VidaInicial = 3;
    private int VidaActual;
    public float LímiteIzquierdo;
    public float LímiteDerecho;
    private bool moviendoseDerecha = true;
    public GameObject ProyectilPrefab;
    public Transform PuntoDisparo;
    public float FrecuenciaDisparo = 1.0f;
    private float TiempoUltimoDisparo;

    public int DañoProyectilEnemigo = 1;

    private NavePrincipal navePrincipal;

    // Variables para los sonidos
    public AudioClip disparoEnemigoAudioClip;
    public AudioClip choqueAudioClip;
    public AudioSource audioSource; // Variable para el sonido de choque

    void Start()
    {
        VidaActual = VidaInicial;

        // Buscar la nave principal en la escena
        navePrincipal = GameObject.FindObjectOfType<NavePrincipal>();

        // Mostrar el mensaje de ataque antes de comenzar
        MostrarMensajeAtaque(3f);

        // Configura el AudioSource para el sonido de disparo de naves enemigas
        audioSource = GetComponent<AudioSource>();
        // disparoEnemigoAudioSource.clip = clipSonidoDisparoEnemigo; // Asigna el clip de sonido de disparo de naves enemigas
    }

    void Update()
    {
        if (moviendoseDerecha)
        {
            transform.Translate(Vector3.right * Velocidad * Time.deltaTime);

            if (transform.position.x >= LímiteDerecho)
            {
                moviendoseDerecha = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * Velocidad * Time.deltaTime);

            if (transform.position.x <= LímiteIzquierdo)
            {
                moviendoseDerecha = true;
            }
        }

        if (Time.time - TiempoUltimoDisparo >= FrecuenciaDisparo)
        {
            DispararAbajo();
            audioSource.PlayOneShot(disparoEnemigoAudioClip);
            TiempoUltimoDisparo = Time.time;

            // Reproduce el sonido de disparo de naves enemigas al disparar
           
           
        }
    }

    void DispararAbajo()
    {
        if (ProyectilPrefab != null && PuntoDisparo != null)
        {
            GameObject proyectil = Instantiate(ProyectilPrefab, PuntoDisparo.position, Quaternion.identity);
            proyectil.tag = "ProyectilEnemigo";

            Proyectil proyectilScript = proyectil.GetComponent<Proyectil>();
            if (proyectilScript != null)
            {
                proyectilScript.Daño = DañoProyectilEnemigo;
            }
        }
    }

    public void RecibirDaño(int cantidad)
    {
        VidaActual -= cantidad;

        if (VidaActual <= 0)
        {
            // Reproduce el sonido de choque al recibir daño
            audioSource.PlayOneShot(choqueAudioClip);

            if (navePrincipal != null)
            {
                navePrincipal.NaveEnemigaDestruida();
            }

            Destroy(gameObject);
        }
    }
}
