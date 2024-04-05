using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    

    [SerializeField] private string inicioSceneName = "Inicio";
    [SerializeField] private string pausaSceneName = "Pausa";
    [SerializeField] private string finalSceneName = "Final";
    [SerializeField] private string winSceneName = "Win"; // Nuevo campo para el nombre de la escena "Win"

    public void LoadInicioScene()
    {
        SceneManager.LoadScene(inicioSceneName);
    }

    public void LoadPausaScene()
    {
        SceneManager.LoadScene(pausaSceneName);
    }

    public void LoadFinalScene()
    {
        SceneManager.LoadScene(finalSceneName);
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene(winSceneName); // Método para cargar la escena "Win"
    }
}