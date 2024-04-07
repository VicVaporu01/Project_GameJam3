using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public GameObject inicioCanvas;
    public GameObject pauseCanvas;
    public GameObject gameOverCanvas;
    public Text cigarrosText;
    public Text vidaText;

    private int cigarrosRecogidos;
    private int vida;

    void Start()
    {
        // Al iniciar el juego, mostrar el canvas de inicio y ocultar los demás
        ShowInicioCanvas();
        HidePauseCanvas();
        HideGameOverCanvas();

        // Inicializar valores
        cigarrosRecogidos = 0;
        vida = 3; // Puedes ajustar este valor según tu juego

        UpdateCigarrosText();
        UpdateVidaText();
    }

    void UpdateCigarrosText()
    {
        // Actualizar el texto del contador de cigarros
        cigarrosText.text = "Cigarros: " + cigarrosRecogidos;
    }

    void UpdateVidaText()
    {
        // Actualizar el texto del contador de vida
        vidaText.text = "Vida: " + vida;
    }

    public void AddCigarro()
    {
        // Incrementar el contador de cigarros al recoger uno
        cigarrosRecogidos++;
        UpdateCigarrosText();

        // Verificar si se han recogido todos los cigarros (condición para ganar el juego)
        if (cigarrosRecogidos >= 20)
        {
            SceneManager.LoadScene("WinScene"); // Cambiar al nombre de la escena de victoria
        }
    }

    public void DamagePlayer()
    {
        // Reducir la vida del jugador
        vida--;

        // Actualizar el texto de vida
        UpdateVidaText();

        // Verificar si el jugador se queda sin vida (game over)
        if (vida <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        // Mostrar el canvas de Game Over
        ShowGameOverCanvas();
    }

    public void RestartGame()
    {
        // Reiniciar el juego cargando la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowInicioCanvas()
    {
        inicioCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    public void ShowPauseCanvas()
    {
        inicioCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
    }

    public void ShowGameOverCanvas()
    {
        inicioCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }

    public void HideInicioCanvas()
    {
        inicioCanvas.SetActive(false);
    }

    public void HidePauseCanvas()
    {
        pauseCanvas.SetActive(false);
    }

    public void HideGameOverCanvas()
    {
        gameOverCanvas.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

