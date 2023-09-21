using UnityEngine;

public class NaveEnemiga : MonoBehaviour
{
    public float Velocidad;
    public int VidaInicial = 3; // Puntos de vida iniciales de la nave enemiga
    private int VidaActual; // Puntos de vida actuales
    public float L�miteIzquierdo; // L�mite izquierdo para el movimiento
    public float L�miteDerecho; // L�mite derecho para el movimiento
    private bool moviendoseDerecha = true; // Flag para controlar la direcci�n del movimiento
    public GameObject ProyectilPrefab; // Prefab del proyectil que dispara
    public Transform PuntoDisparo; // Punto de origen del disparo
    public float FrecuenciaDisparo = 1.0f; // Frecuencia de disparo en segundos
    private float TiempoUltimoDisparo;

    public int Da�oProyectilEnemigo = 1; // Da�o que inflige el proyectil del enemigo

    void Start()
    {
        VidaActual = VidaInicial; // Establecer la vida inicial
    }

    void Update()
    {
        // Determinar la direcci�n del movimiento
        if (moviendoseDerecha)
        {
            transform.Translate(Vector3.right * Velocidad * Time.deltaTime);
            // Si llega al l�mite derecho, cambia de direcci�n
            if (transform.position.x >= L�miteDerecho)
            {
                moviendoseDerecha = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * Velocidad * Time.deltaTime);
            // Si llega al l�mite izquierdo, cambia de direcci�n
            if (transform.position.x <= L�miteIzquierdo)
            {
                moviendoseDerecha = true;
            }
        }

        // Disparar autom�ticamente hacia abajo
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

            // Asigna el da�o al proyectil
            Proyectil proyectilScript = proyectil.GetComponent<Proyectil>();
            if (proyectilScript != null)
            {
                proyectilScript.Da�o = Da�oProyectilEnemigo;
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

            // Verificar si se encontr� el componente Proyectil
            if (scriptProyectil != null)
            {
                // Aplicar da�o a la nave enemiga
                RecibirDa�o(scriptProyectil.Da�o);

                // Destruir el proyectil al impactar
                Destroy(other.gameObject);
            }
        }
    }*/

    public void RecibirDa�o(int cantidad)
    {
        VidaActual -= cantidad;

        if (VidaActual <= 0)
        {
            // La nave enemiga ha sido destruida
            Destroy(gameObject);
        }
    }
}
