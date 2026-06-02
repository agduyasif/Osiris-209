using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlNavegacion : MonoBehaviour
{
    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}