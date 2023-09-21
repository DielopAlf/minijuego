using UnityEngine;

public class NaveEnemiga : MonoBehaviour
{
    public float Velocidad;
    public int VidaInicial = 3; // Puntos de vida iniciales de la nave enemiga
    private int VidaActual; // Puntos de vida actuales
    public float LímiteIzquierdo; // Límite izquierdo para el movimiento
    public float LímiteDerecho; // Límite derecho para el movimiento
    private bool moviendoseDerecha = true; // Flag para controlar la dirección del movimiento
    public GameObject ProyectilPrefab; // Prefab del proyectil que dispara
    public Transform PuntoDisparo; // Punto de origen del disparo
    public float FrecuenciaDisparo = 1.0f; // Frecuencia de disparo en segundos
    private float TiempoUltimoDisparo;

    public int DañoProyectilEnemigo = 1; // Daño que inflige el proyectil del enemigo

    void Start()
    {
        VidaActual = VidaInicial; // Establecer la vida inicial
    }

    void Update()
    {
        // Determinar la dirección del movimiento
        if (moviendoseDerecha)
        {
            transform.Translate(Vector3.right * Velocidad * Time.deltaTime);
            // Si llega al límite derecho, cambia de dirección
            if (transform.position.x >= LímiteDerecho)
            {
                moviendoseDerecha = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * Velocidad * Time.deltaTime);
            // Si llega al límite izquierdo, cambia de dirección
            if (transform.position.x <= LímiteIzquierdo)
            {
                moviendoseDerecha = true;
            }
        }

        // Disparar automáticamente hacia abajo
        if (Time.time - TiempoUltimoDisparo >= FrecuenciaDisparo)
        {
            DispararAbajo();
            TiempoUltimoDisparo = Time.time;
        }
    }

    void DispararAbajo()
    {
        if (ProyectilPrefab != null && PuntoDisparo != null)
        {
            GameObject proyectil = Instantiate(ProyectilPrefab, PuntoDisparo.position, Quaternion.identity);
            proyectil.tag = "ProyectilEnemigo"; // Etiqueta para identificar los proyectiles del enemigo

            // Asigna el daño al proyectil
            Proyectil proyectilScript = proyectil.GetComponent<Proyectil>();
            if (proyectilScript != null)
            {
                proyectilScript.Daño = DañoProyectilEnemigo;
            }
        }
    }

   /* void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el proyectil tiene el tag "ProyectilJugador"
        if (other.CompareTag("ProyectilJugador"))
        {
            // Obtener el componente Proyectil del proyectil
            Proyectil scriptProyectil = other.GetComponent<Proyectil>();

            // Verificar si se encontró el componente Proyectil
            if (scriptProyectil != null)
            {
                // Aplicar daño a la nave enemiga
                RecibirDaño(scriptProyectil.Daño);

                // Destruir el proyectil al impactar
                Destroy(other.gameObject);
            }
        }
    }*/

    public void RecibirDaño(int cantidad)
    {
        VidaActual -= cantidad;

        if (VidaActual <= 0)
        {
            // La nave enemiga ha sido destruida
            Destroy(gameObject);
        }
    }
}
