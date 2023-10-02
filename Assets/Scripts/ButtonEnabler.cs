using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEnabler : MonoBehaviour
{
    [SerializeField] 
    public int nivel = 0;

    void Awake()
    {
        int superado = PlayerPrefs.GetInt("NivelSuperado" + (nivel + 1).ToString(), 0);
        int desbloqueado = PlayerPrefs.GetInt("Nivel" + nivel.ToString() + "Completado", 0);

        Debug.Log("el valor del método superado es " + superado);

        // Verifica si el nivel anterior se ha completado y si el nivel actual está desbloqueado
        GetComponent<Button>().interactable = (superado == 1) && (desbloqueado == 1);
    }
}
