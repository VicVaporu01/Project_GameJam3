using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject inicioCanvas;
    public GameObject pauseCanvas;
    public GameObject gameOverCanvas;
    public GameObject winCanvas; // Agregamos una referencia al canvas de la escena Win

    public Button playButton;
    public Button optionsButton;
    public Button tutorialButton;
    public Button exitButton;
    public Button replayButton; // Botón para reiniciar la escena Game Over
    public Button restartButton; // Botón para reiniciar la escena Win

    void Start()
    {
        ShowInicioCanvas();
        HidePauseCanvas();
        HideGameOverCanvas();
        HideWinCanvas(); // Al inicio, ocultamos el canvas de la escena Win

        playButton.onClick.AddListener(OnInicioPlayButtonClick);
        optionsButton.onClick.AddListener(OnOptionsButtonClick);
        tutorialButton.onClick.AddListener(OnTutorialButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
        replayButton.onClick.AddListener(OnGameOverReplayButtonClick); // Escuchamos el clic del botón "Replay"
        restartButton.onClick.AddListener(OnWinRestartButtonClick); // Escuchamos el clic del botón "Restart"
    }

    void OnInicioPlayButtonClick()
    {
        Debug.Log("<color=green>¡El juego ha comenzado desde el inicio!</color>");
        // Aquí deberías cargar la escena del juego desde el inicio o realizar cualquier otra acción necesaria
    }

    void OnGameOverReplayButtonClick()
    {
        Debug.Log("<color=orange>¡Reiniciando el juego desde el game over!</color>");
        // Aquí deberías reiniciar la escena del juego o realizar cualquier otra acción necesaria
    }

    void OnWinRestartButtonClick()
    {
        Debug.Log("<color=blue>¡Reiniciando la escena Win!</color>");
        // Aquí deberías reiniciar la escena Win o realizar cualquier otra acción necesaria
    }

    void OnOptionsButtonClick()
    {
        Debug.Log("<color=yellow>¡Abriendo opciones!</color>");
        // Aquí deberías mostrar el canvas de opciones o realizar cualquier otra acción necesaria
    }

    void OnTutorialButtonClick()
    {
        Debug.Log("<color=blue>¡Abriendo tutorial!</color>");
        // Aquí deberías mostrar el canvas de tutorial o realizar cualquier otra acción necesaria
    }

    void OnExitButtonClick()
    {
        Debug.Log("<color=red>¡Saliendo del juego!</color>");
        // Aquí deberías salir del juego o volver al menú principal
    }

    public void ShowInicioCanvas()
    {
        inicioCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        winCanvas.SetActive(false); // Al mostrar el canvas de inicio, también ocultamos el canvas de la escena Win
    }

    public void ShowPauseCanvas()
    {
        inicioCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        winCanvas.SetActive(false); // Al mostrar el canvas de pausa, también ocultamos el canvas de la escena Win
    }

    public void ShowGameOverCanvas()
    {
        inicioCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        winCanvas.SetActive(false); // Al mostrar el canvas de game over, también ocultamos el canvas de la escena Win
    }

    public void ShowWinCanvas()
    {
        winCanvas.SetActive(true); // Mostramos el canvas de la escena Win
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

    public void HideWinCanvas()
    {
        winCanvas.SetActive(false); // Ocultamos el canvas de la escena Win
    }
}
