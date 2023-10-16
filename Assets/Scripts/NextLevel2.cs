using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel2 : MonoBehaviour

{

    void Start()
    {
        //  Debug.Log("cargarnivel2");
    }


    public void LoadA(string nivel)
    {

        SceneManager.LoadScene(nivel);
        Debug.Log(nivel);

    }
}