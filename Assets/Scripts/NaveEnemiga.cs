using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveEnemiga : MonoBehaviour
{
    public float Velocidad;
    public int VidaInicial = 3; // Puntos de vida iniciales de la nave enemiga
    private int VidaActual; // Puntos de vida actuales
    public float L�miteIzquierdo; // L�mite izquierdo para el movimiento
    public float L�miteDerecho; // L�mite derecho para el movimiento
    private bool moviendoseDerecha = true; // Flag para controlar la direcci�n del movimiento

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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el proyectil tiene el tag "ProyectilJugador"
        if (other.CompareTag("Proyectil"))
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
    }

    public void RecibirDa�o(int cantidad)
    {
        VidaActual -= cantidad;

        if (VidaActual <= 0)
        {
            // La nave enemiga ha sido destruida
            Destroy(gameObject);
        }
    }

    // Funci�n para invertir la direcci�n del proyectil
    public void InvertirDireccionProyectil(GameObject proyectil)
    {
        Proyectil scriptProyectil = proyectil.GetComponent<Proyectil>();
        if (scriptProyectil != null)
        {
            scriptProyectil.InvertirDireccion();
        }
    }
}
