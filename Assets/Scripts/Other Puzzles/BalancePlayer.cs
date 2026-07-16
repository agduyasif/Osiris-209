using UnityEngine;
using UnityEngine.InputSystem;

public class BalancePlayer : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private InputActionReference movecontrol;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float speed = 5f;

    [Header("Script de Movimiento Normal")]
    [SerializeField] private MonoBehaviour scriptMovimientoNormal;

    private Rigidbody rb;
    private BalanceLogic balanceLogic;

    public bool IsBalancing { get; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        balanceLogic = new BalanceLogic();
    }

    void Update()
    {
        if (!IsBalancing) return;

        Vector2 input = movecontrol.action.ReadValue<Vector2>();
        balanceLogic.UpdateLogic(input.x);

        Vector3 dirAdelante = transform.forward * input.y;
        balanceLogic.TubeMove(dirAdelante, speed, rb);

        if (balanceLogic.CheckIfFallen())
        {
            TerminarEquilibrio();
            ResetScene.Reset();
        }
    }

    void LateUpdate()
    {
        if (!IsBalancing) return;
        float inclinacion = balanceLogic.balanceHandle * 20f;
        playerCamera.transform.localRotation = Quaternion.Euler(0, 0, -inclinacion);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Equilibrio"))
        {
            Debug.Log("Toqué el inicio del cilindro");
            IsBalancing = true;
            balanceLogic.Reset();

            if (scriptMovimientoNormal != null)
            {
                scriptMovimientoNormal.enabled = false;
            }
        }

        if (other.CompareTag("Fin"))
        {
            TerminarEquilibrio(); 
        }
    }

    public void TerminarEquilibrio()
    {
        IsBalancing = false;
        playerCamera.transform.localRotation = Quaternion.identity;

        if (scriptMovimientoNormal != null)
        {
            scriptMovimientoNormal.enabled = true;
        }
    }
}