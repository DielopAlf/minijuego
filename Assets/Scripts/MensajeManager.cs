using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensajeManager : MonoBehaviour
{
    public GameObject panelMensajeAtaque; // Referencia al panel de mensaje de ataque

    protected void MostrarMensajeAtaque(float duracion)
    {
        panelMensajeAtaque.SetActive(true);
        Invoke("OcultarMensaje", duracion);
    }

    private void OcultarMensaje()
    {
        panelMensajeAtaque.SetActive(false);
    }
}