using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Nivel Prototipo");
    }
    public void IrAControles()
    {
        SceneManager.LoadScene("Controls");
    }

    public void Salir()
    {
        Application.Quit();
    }
}