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

    private BalancePlayer PB;

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
        PB = GetComponent<BalancePlayer>();

        movement = new Movement(transform, speed, rb, animator, aS, walkSound, jumpSound);
        control = new Control(movecontrol, movement, transform, vibration);
        grappleMove = new GrappleMove(rb, control, 0.2f, playerCamera.transform);
        grappleMove.SetShoothook(GetComponentInChildren<Shoothook>());
    }

    private void Update()
    {
        if (PB != null && PB.IsBalancing)
        {
            return;
        }

        if (isGrappling)
        {
            if (movement.IsGrounded())
            {
                control.ArtificialUpdate(true);
                movement.CheckGroundedStatus();
            }
            else
            {
                grappleMove.Push();
                if (Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    grappleMove.Jump();
                }
            }
        }
        else
        {
            control.ArtificialUpdate(!isGrappling);
            movement.CheckGroundedStatus();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 size = new Vector3(0.6f, 0.2f, 0.6f);
        Gizmos.DrawWireCube(transform.position + Vector3.down * 1.1f, size);
    }
}
