using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorEnemigo : MonoBehaviour
{
    public GameObject ProyectilPrefab; // Prefab del proyectil que dispara
    public Transform PuntoDisparo; // Punto de origen del disparo
    public float FrecuenciaDisparo = 1.0f; // Frecuencia de disparo en segundos
    private float TiempoUltimoDisparo;

    void Update()
    {
        // Disparar proyectiles automáticamente a intervalos regulares
        if (Time.time - TiempoUltimoDisparo >= FrecuenciaDisparo)
        {
            Disparar();
            TiempoUltimoDisparo = Time.time;
        }
    }

    void Disparar()
    {
        if (ProyectilPrefab != null && PuntoDisparo != null)
        {
            // Crear el proyectil
            GameObject proyectil = Instantiate(ProyectilPrefab, PuntoDisparo.position, Quaternion.identity);

            // Llamar al método InvertirDireccion del proyectil
            proyectil.GetComponent<Proyectil>().InvertirDireccion();
        }
    }
}
