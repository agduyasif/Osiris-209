using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Movement movement;
    Control control;
    [SerializeField] float speed;
    [SerializeField] InputActionReference movecontrol;
    Rigidbody rb;


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        movement = new Movement(transform, speed, rb);
        control = new Control(movecontrol, movement, transform);
    }

    private void Update()
    {
        control.ArtificialUpdate();
    }

}
