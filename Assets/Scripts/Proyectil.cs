using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float Velocidad;
    public int Daño = 1; // Daño que inflige el proyectil
    public float TiempoDeVida = 5f; // Tiempo de vida del proyectil

    void Start()
    {
        // Destruye el proyectil después de un tiempo
        Destroy(gameObject, TiempoDeVida);
    }

    void Update()
    {
        // Mover hacia arriba
        transform.Translate(Vector3.up * Velocidad * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el proyectil colisiona con la nave principal
        if (other.CompareTag("NavePrincipal"))
        {
            // Obtener el componente NavePrincipal del objeto colisionado
            NavePrincipal navePrincipal = other.gameObject.GetComponent<NavePrincipal>();

            // Verificar si se encontró el componente NavePrincipal
            if (navePrincipal != null)
            {
                // Aplicar daño a la nave principal
                navePrincipal.RecibirDaño(Daño);
            }

            // Destruir el proyectil al impactar con la nave principal
            Destroy(gameObject);
        }
        else
        {
            // Destruir el proyectil al impactar con cualquier otro objeto
            Destroy(gameObject);
        }
    }

    public void InvertirDireccion()
    {
        // Cambia la dirección del proyectil invirtiendo su velocidad
        Velocidad = -Velocidad;
    }
}
