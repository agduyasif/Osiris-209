using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Movement movement;
    Control control;
    public Animator animator;
    [SerializeField] float speed;
    [SerializeField] InputActionReference movecontrol;
    Rigidbody rb;
    [SerializeField] Camera playerCamera;
    bool isBalancing = false;
    BalanceLogic balanceLogic = new BalanceLogic();
    [SerializeField] bool vibration = true;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        movement = new Movement(transform, speed, rb, animator);
        control = new Control(movecontrol, movement, transform, vibration);

    }

    private void Update()
    {
        if (!isBalancing)
        {
            control.ArtificialUpdate();
            movement.CheckGroundedStatus();
        }
        else
        {
            Vector2 input = movecontrol.action.ReadValue<Vector2>();

            balanceLogic.UpdateLogic(input.x);

            Vector3 dirAdelante = transform.forward * input.y;
            balanceLogic.TubeMove(dirAdelante, speed, rb);

            float inclinacion = balanceLogic.balanceHandle * 20f;
            playerCamera.transform.localRotation = Quaternion.Euler(0, 0, -inclinacion);

            if (balanceLogic.CheckIfFallen())
            {
                isBalancing = false;
                ResetScene.Reset();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Equilibrio"))
        {
            isBalancing = true;
            balanceLogic.Reset();
        }

        if (other.CompareTag("Fin"))
        {
            isBalancing = false;
            playerCamera.transform.localRotation = Quaternion.identity;
        }
    }

}
