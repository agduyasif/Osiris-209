using UnityEngine;
using UnityEngine.Events;

public class Buttom : MonoBehaviour, IGrappable
{
    [Header("Eventos")]
    [SerializeField] UnityEvent ButtomEvent;

    [Header("Sonido")]
    [SerializeField] private AudioSource _sonidoElectricidad;

    public void AlEnganchar()
    {
        Press();
    }
    public void Press()
    {
        if (_sonidoElectricidad != null)
        {
            _sonidoElectricidad.Play();
        }
        ButtomEvent.Invoke();
    }

}
