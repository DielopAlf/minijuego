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

    private NavePrincipal navePrincipal; // Referencia a la nave principal

    void Start()
    {
        VidaActual = VidaInicial; // Establecer la vida inicial

        // Buscar la nave principal en la escena
        navePrincipal = GameObject.FindObjectOfType<NavePrincipal>();
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

    public void RecibirDa�o(int cantidad)
    {
        VidaActual -= cantidad;

        if (VidaActual <= 0)
        {
            // Notificar a la nave principal que esta nave enemiga ha sido destruida
            if (navePrincipal != null)
            {
                navePrincipal.NaveEnemigaDestruida();
            }

            // Destruir la nave enemiga
            Destroy(gameObject);
        }
    }
}
