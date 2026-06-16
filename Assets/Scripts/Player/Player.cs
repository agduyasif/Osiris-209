using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region VARIABLES: COMPONENTES E INTERNAS
    private Rigidbody rb;
    private BoxCollider col;
    private BalancePlayer PB;

    private Movement movement;
    private Control control;

    private float standingColliderSizeY;
    private float standingColliderCenterY;
    #endregion

    #region VARIABLES: CONFIGURACIÓN EN INSPECTOR
    [Header("Referencias Obligatorias")]
    public Animator animator;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private InputActionReference movecontrol;

    [Header("Configuración de Movimiento")]
    [SerializeField] private float speed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private bool vibration = true;

    [Header("Sistema de Gancho (Grappling)")]
    public bool isGrappling = false;
    public GrappleMove grappleMove;

    [Header("Configuración de Sonido")]
    [SerializeField] private AudioSource aS;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip jumpSound;

    [Header("Configuración de Agachado")]
    [SerializeField] private float cameraTransitionSpeed;
    [SerializeField] private float standingCameraHeight;
    [SerializeField] private float crouchCameraHeight;
    [SerializeField] private float crouchColliderSizeY;
    #endregion

    #region MÉTODOS PÚBLICOS
    public bool IsGrounded() => movement.IsGrounded();
    #endregion

    #region MÉTODOS DE UNITY (LIFECYCLE)
    void Start()
    {
        // Configuración del cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Inicialización de componentes
        rb = GetComponent<Rigidbody>();
        PB = GetComponent<BalancePlayer>();
        col = GetComponent<BoxCollider>();

        // Captura de datos iniciales
        if (playerCamera != null) standingCameraHeight = playerCamera.transform.localPosition.y;
        if (col != null)
        {
            standingColliderSizeY = col.size.y;
            standingColliderCenterY = col.center.y;
        }

        // Instancias de clases de control
        movement = new Movement(transform, speed, crouchSpeed, rb, animator, aS, walkSound, jumpSound);
        control = new Control(movecontrol, movement, transform, vibration);
        grappleMove = new GrappleMove(rb, control, 0.2f, playerCamera.transform);
        grappleMove.SetShoothook(GetComponentInChildren<Shoothook>());
    }

    private void Update()
    {
        // Si el jugador está haciendo equilibrio, congelamos el resto de la lógica
        if (PB != null && PB.IsBalancing)
        {
            return;
        }

        // Manejo de estados: Grappling vs Movimiento Normal
        if (isGrappling)
        {
            ManejarLogicaGrappling();
        }
        else
        {
            ManejarLogicaNormal();
        }

        // Procesar transición de altura
        CrouchingHeight();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 size = new Vector3(0.6f, 0.2f, 0.6f);
        Gizmos.DrawWireCube(transform.position + Vector3.down * 1.1f, size);
    }
    #endregion

    #region LÓGICA INTERNA
    private void ManejarLogicaGrappling()
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

    private void ManejarLogicaNormal()
    {
        control.ArtificialUpdate(!isGrappling);
        movement.CheckGroundedStatus();
    }

    private void CrouchingHeight()
    {
        float targetCamHeight = movement.IsCrouching ? crouchCameraHeight : standingCameraHeight;
        float targetColSizeY = movement.IsCrouching ? crouchColliderSizeY : standingColliderSizeY;

        // Interpolación de la Cámara
        if (playerCamera != null)
        {
            Vector3 camLocalPos = playerCamera.transform.localPosition;
            camLocalPos.y = Mathf.Lerp(camLocalPos.y, targetCamHeight, Time.deltaTime * cameraTransitionSpeed);
            playerCamera.transform.localPosition = camLocalPos;
        }

        // Interpolación del Colisionador
        if (col != null)
        {
            Vector3 currentSize = col.size;
            currentSize.y = Mathf.Lerp(currentSize.y, targetColSizeY, Time.deltaTime * cameraTransitionSpeed);
            col.size = currentSize;

            float bottomY = standingColliderCenterY - (standingColliderSizeY / 2f);

            Vector3 currentCenter = col.center;
            currentCenter.y = bottomY + (currentSize.y / 2f);
            col.center = currentCenter;
        }
    }
    #endregion
}
