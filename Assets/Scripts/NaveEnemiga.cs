using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveEnemiga : MonoBehaviour
{
    public GameObject[] powerups;
    public int puntos = 10;
    public int GolpesparaRomperse = 1; // Número de veces que se debe golpear la plataforma para que se rompa
    private int GolpesDados = 0; // Número de veces que se ha golpeado la plataforma
    [Range(0, 1)]
    public float probabilidad = 0.1f;
    public void HitByBall(bool instantDestroy)
    {
        GolpesDados++;
        if (GolpesDados >= GolpesparaRomperse || instantDestroy == true)
        {
            DestroyNave();
        }
    }

    private void DestroyNave()
    {
        //PuntuacionController.Instance.AgregarPuntos(puntos);
        //ControladorVictoria.Instance.PlataformaDestruida();

        if (powerups.Length > 0)
        {

            if (Random.value < probabilidad)
            {
                int powerrandom = Random.Range(0, powerups.Length);

                Instantiate(powerups[powerrandom], transform.position, Quaternion.identity);

            }

        }


        Destroy(gameObject);
    }
}