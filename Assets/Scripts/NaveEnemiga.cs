using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveEnemiga : MonoBehaviour
{
    public float Velocidad;
    public int VidaInicial = 3; // Puntos de vida iniciales de la nave enemiga
    private int VidaActual; // Puntos de vida actuales
    public float LímiteIzquierdo; // Límite izquierdo para el movimiento
    public float LímiteDerecho; // Límite derecho para el movimiento
    private bool moviendoseDerecha = true; // Flag para controlar la dirección del movimiento

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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el proyectil tiene el tag "ProyectilJugador"
        if (other.CompareTag("Proyectil"))
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
    }

    public void RecibirDaño(int cantidad)
    {
        VidaActual -= cantidad;

        if (VidaActual <= 0)
        {
            // La nave enemiga ha sido destruida
            Destroy(gameObject);
        }
    }

    // Función para invertir la dirección del proyectil
    public void InvertirDireccionProyectil(GameObject proyectil)
    {
        Proyectil scriptProyectil = proyectil.GetComponent<Proyectil>();
        if (scriptProyectil != null)
        {
            scriptProyectil.InvertirDireccion();
        }
    }
}
