using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public GameObject menuInicial;
    public AudioSource audioSource;
    public AudioClip interfazaudio;


    void Start()
    {
        menuInicial.SetActive(true);

        // Encuentra el componente AudioSource en el objeto que contiene el sonido
        audioSource = GameObject.Find("controllador").GetComponent<AudioSource>();
    }

    public void cerrarJuego()
    {
        Application.Quit();

        // Reproduce el sonido
        audioSource.PlayOneShot(interfazaudio);
    }

    public void botonplay()
    {
        audioSource.PlayOneShot(interfazaudio);
        menuInicial.SetActive(false);



    }

    public void volver()
    {
        menuInicial.SetActive(true);

        // Reproduce el sonido
        audioSource.PlayOneShot(interfazaudio);
    }
}