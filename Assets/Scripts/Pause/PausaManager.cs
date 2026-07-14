using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PausaManager : MonoBehaviour
{
    public GameObject menuPausaPanel;
    public Volume postProcessVolume;
    private bool estaPausado = false;
    private DepthOfField dof;
    [SerializeField] private GameObject _volumeMenu;

    void Start()
    {
        if (postProcessVolume != null && postProcessVolume.profile.TryGet(out dof))
        {
            dof.active = false;
        }
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
        if (dof != null) dof.active = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Reanudar()
    {
        estaPausado = false;
        Time.timeScale = 1f;
        if (menuPausaPanel != null) menuPausaPanel.SetActive(false);
        if (dof != null) dof.active = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (menuPausaPanel != null) menuPausaPanel.SetActive(false);
        if (_volumeMenu != null) _volumeMenu.SetActive(false);
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