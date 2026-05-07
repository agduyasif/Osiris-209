using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Nivel Prototipo");
    }

    public void Salir()
    {
        Application.Quit();
    }
}