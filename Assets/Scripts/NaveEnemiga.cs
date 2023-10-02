using UnityEngine;

public class NaveEnemiga : MonoBehaviour
{
    public float Velocidad;
    public int VidaInicial = 3;
    private int VidaActual;
    public float L�miteIzquierdo;
    public float L�miteDerecho;
    private bool moviendoseDerecha = true;
    public GameObject ProyectilPrefab;
    public Transform PuntoDisparo;
    public float FrecuenciaDisparo = 1.0f;
    private float TiempoUltimoDisparo;

    public int Da�oProyectilEnemigo = 1;

    private NavePrincipal navePrincipal;

    // Variables para los sonidos
    public AudioClip disparoEnemigoAudioClip;
    public AudioClip choqueAudioClip;
    public AudioClip choqueNavePrincipalAudioClip;
    public AudioSource audioSource; // Variable para el sonido de choque

    void Start()
    {
        VidaActual = VidaInicial;

        // Buscar la nave principal en la escena
        navePrincipal = GameObject.FindObjectOfType<NavePrincipal>();

        

        // Configura el AudioSource para el sonido de disparo de naves enemigas
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (moviendoseDerecha)
        {
            transform.Translate(Vector3.right * Velocidad * Time.deltaTime);

            if (transform.position.x >= L�miteDerecho)
            {
                moviendoseDerecha = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * Velocidad * Time.deltaTime);

            if (transform.position.x <= L�miteIzquierdo)
            {
                moviendoseDerecha = true;
            }
        }

        if (Time.time - TiempoUltimoDisparo >= FrecuenciaDisparo)
        {
            DispararAbajo();
            audioSource.PlayOneShot(disparoEnemigoAudioClip);
            TiempoUltimoDisparo = Time.time;
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
                proyectilScript.Da�o = Da�oProyectilEnemigo;
            }
        }
    }

    public void RecibirDa�o(int cantidad)
    {
        VidaActual -= cantidad;

        if (VidaActual <= 0)
        {
            // Reproduce el sonido de choque al recibir da�o
            audioSource.PlayOneShot(choqueAudioClip);

            if (navePrincipal != null)
            {
                navePrincipal.NaveEnemigaDestruida();
            }
            audioSource.PlayOneShot(choqueAudioClip);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ProyectilJugador"))
        {
            // Reproduce el sonido de choque al colisionar con un proyectil de la nave principal
            audioSource.PlayOneShot(choqueNavePrincipalAudioClip);

            // Resto de la l�gica para manejar la colisi�n con un proyectil de la nave principal
        }
    }
}
