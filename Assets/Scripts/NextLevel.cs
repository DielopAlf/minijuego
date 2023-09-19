using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public GameObject audioObject;
    private AudioSource audioSource;
    public AudioClip interfazaudio;
    void Start()
    {
        audioSource = audioObject.GetComponent<AudioSource>();
        Debug.Log("cargarnivel1");
    }

    public void LoadA(string nivel)
    {
        audioSource.PlayOneShot(interfazaudio);
        SceneManager.LoadScene(nivel);

    }
}
