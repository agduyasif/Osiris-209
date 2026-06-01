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
    public bool isGrappling = false;
    public GrappleMove grappleMove;

    [Header("Configuración de Sonido")]
    [SerializeField] private AudioSource aS;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip jumpSound;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();

        movement = new Movement(transform, speed, rb, animator, aS, walkSound, jumpSound);
        control = new Control(movecontrol, movement, transform, vibration);
        grappleMove = new GrappleMove(rb, control, 0.2f);
    }

    private void Update()
    {
        if (isBalancing)
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
        else if (isGrappling)
        {
            if (movement.IsGrounded())
            {
                control.ArtificialUpdate(true);
                movement.CheckGroundedStatus();
            }
            else
            {
                grappleMove.Push();
            }
        }
        else
        {
            control.ArtificialUpdate(!isGrappling);
            movement.CheckGroundedStatus();
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 size = new Vector3(0.6f, 0.2f, 0.6f);
        Gizmos.DrawWireCube(transform.position + Vector3.down * 1.1f, size);
    }


}
