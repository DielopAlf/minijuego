using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavePrincipal : MonoBehaviour
{
    [SerializeField] KeyCode botonDerecha;
    [SerializeField] KeyCode botonIzquierda;
    [SerializeField] KeyCode botonDisparo;
    public float Speed = 10f;
    public float maxdistancia;
    public GameObject proyectilPrefab; // Prefab del proyectil que disparar�
    public Transform puntoDisparo; // Punto de origen del disparo
    public float frecuenciaDisparo = 0.5f; // Frecuencia de disparo en segundos
    private float tiempoUltimoDisparo;

    private int vidas = 3;

    void Update()
    {
        // Movimiento horizontal
        if (Input.GetKey(botonDerecha) && transform.position.x < maxdistancia)
        {
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
        }

        if (Input.GetKey(botonIzquierda) && transform.position.x > -maxdistancia)
        {
            transform.Translate(Vector3.left * Time.deltaTime * Speed);
        }

        // Disparo de proyectiles al presionar el bot�n asignado
        if (Input.GetKey(botonDisparo) && Time.time - tiempoUltimoDisparo >= frecuenciaDisparo)
        {
            Disparar();
            tiempoUltimoDisparo = Time.time;
        }
    }

    void Disparar()
    {
        if (proyectilPrefab != null && puntoDisparo != null)
        {
            GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);
            proyectil.tag = "ProyectilJugador"; // Etiqueta para identificar los proyectiles del jugador
        }
    }

    public void RecibirDa�o(int cantidadDeDa�o)
    {
        vidas -= cantidadDeDa�o;

        if (vidas <= 0)
        {
            // Manejar la l�gica de juego cuando la nave queda sin vidas
            Destroy(gameObject);
        }
    }
}
