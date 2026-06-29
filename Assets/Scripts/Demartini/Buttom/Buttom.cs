using UnityEngine;
using UnityEngine.Events;

public class Buttom : MonoBehaviour, IGrappable
{
    [SerializeField] UnityEvent ButtomEvent;

    public void AlEnganchar()
    {
        Press();
    }
    public void Press()
    {
        ButtomEvent.Invoke();
    }

}
