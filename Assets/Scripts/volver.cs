using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
//using MyNamespace;
using static LeanTween;
public class Volver : MonoBehaviour
{
    public void VolverAlMenuPrincipal()
    {
        // Cargar la escena del menú principal
        SceneManager.LoadScene("MenuInicial");

    }
}
