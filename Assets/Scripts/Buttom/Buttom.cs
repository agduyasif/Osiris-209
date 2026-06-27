using UnityEngine;
using UnityEngine.Events;

public class Buttom : MonoBehaviour
{
    [SerializeField] UnityEvent ButtomEvent;

    public void Press()
    {
        ButtomEvent.Invoke();
    }

}
