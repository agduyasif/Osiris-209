using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems; // Agregado para proteger la UI
using System.Collections.Generic; // Agregado para hacer listas

public class PausaManager : MonoBehaviour
{
    [Header("UI y Menús")]
    public GameObject menuPausaPanel;
    [SerializeField] private GameObject _volumeMenu;
    public GameObject miraUI;

    [Header("Post Procesado (Fondo Borroso)")]
    public GameObject volumenPausa;

    [Header("Control del Jugador")]
    [Tooltip("Arrastrá acá al objeto Player. Se apagarán TODOS sus scripts automáticamente.")]
    public GameObject objetoJugador;

    private bool estaPausado = false;

    // Esta lista va a recordar qué scripts estaban prendidos para no hacer lío al reanudar
    private List<MonoBehaviour> scriptsPausados = new List<MonoBehaviour>();

    void Start()
    {
        if (volumenPausa != null) volumenPausa.SetActive(false);
        if (menuPausaPanel != null) menuPausaPanel.SetActive(false);
        if (_volumeMenu != null) _volumeMenu.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
        {
            if (estaPausado) Reanudar();
            else Pausar();
        }
    }

    public void Pausar()
    {
        estaPausado = true;
        Time.timeScale = 0f;

        if (menuPausaPanel != null) menuPausaPanel.SetActive(true);
        if (volumenPausa != null) volumenPausa.SetActive(true);
        if (miraUI != null) miraUI.SetActive(false);

        // --- LA MAGIA: Apagamos todos los scripts del jugador (movimiento, cámara, gancho) ---
        if (objetoJugador != null)
        {
            scriptsPausados.Clear();

            // Busca todos los scripts dentro del Player, la Cámara y sus hijos
            MonoBehaviour[] todosLosScripts = objetoJugador.GetComponentsInChildren<MonoBehaviour>();

            foreach (MonoBehaviour script in todosLosScripts)
            {
                // Solo apagamos los scripts que estaban prendidos. 
                // Protegemos la Interfaz (UIBehaviour) y a este mismo script por las dudas.
                if (script != null && script.enabled && script != this && !(script is UIBehaviour))
                {
                    script.enabled = false; // Lo congelamos
                    scriptsPausados.Add(script); // Lo anotamos en la lista
                }
            }
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Reanudar()
    {
        estaPausado = false;
        Time.timeScale = 1f;

        if (menuPausaPanel != null) menuPausaPanel.SetActive(false);
        if (_volumeMenu != null) _volumeMenu.SetActive(false);
        if (volumenPausa != null) volumenPausa.SetActive(false);
        if (miraUI != null) miraUI.SetActive(true);

        // --- DESCONGELAR: Volvemos a prender solo los scripts que habíamos anotado ---
        foreach (MonoBehaviour script in scriptsPausados)
        {
            if (script != null)
            {
                script.enabled = true;
            }
        }
        scriptsPausados.Clear();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Volumen()
    {
        if (menuPausaPanel != null) menuPausaPanel.SetActive(false);
        if (_volumeMenu != null) _volumeMenu.SetActive(true);
    }
}