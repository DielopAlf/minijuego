using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float Velocidad;
    public int Daño = 1; // Daño que inflige el proyectil
    public float TiempoDeVida = 5f; // Tiempo de vida del proyectil

    public AudioClip choqueAudioClip; // Variable para el sonido de fondo
    public AudioClip choqueEnemigoAudioClip;
    public AudioSource audioSource;



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
        if (other.CompareTag("NavePrincipal") && tag == "ProyectilEnemigo")
        {
            // Obtener el componente NavePrincipal del objeto colisionado
            NavePrincipal navePrincipal = other.gameObject.GetComponent<NavePrincipal>();

            // Verificar si se encontró el componente NavePrincipal
            if (navePrincipal != null)
            {
                // Aplicar daño a la nave principal
                navePrincipal.RecibirDaño(Daño);
                audioSource.PlayOneShot(choqueAudioClip);
            }

            // Destruir el proyectil al impactar con la nave principal
            Destroy(gameObject);
        }
        else if (other.CompareTag("NaveEnemiga") && tag == "ProyectilJugador")
        {
            // Obtener el componente NaveEnemiga del objeto colisionado
            NaveEnemiga naveEnemiga = other.gameObject.GetComponent<NaveEnemiga>();

            // Verificar si se encontró el componente NaveEnemiga
            if (naveEnemiga != null)
            {
                // Aplicar daño a la nave enemiga
                naveEnemiga.RecibirDaño(Daño);
                audioSource.PlayOneShot(choqueEnemigoAudioClip);
            }

            // Destruir el proyectil al impactar con la nave enemiga
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
