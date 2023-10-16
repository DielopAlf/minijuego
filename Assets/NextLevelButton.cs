using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    public string nextLevelName;

    private void Start()
    {
        
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(LoadNextLevel);
        }
        else
        {
            Debug.LogWarning("El GameObject no tiene un componente Button.");
        }
    }

    // Función para cargar el siguiente nivel
    private void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
