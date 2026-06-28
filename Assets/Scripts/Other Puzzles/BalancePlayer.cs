using UnityEngine;
using UnityEngine.InputSystem;

public class BalancePlayer : MonoBehaviour
{

    [Header("Referencias")]
    [SerializeField] private InputActionReference movecontrol;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float speed = 5f; 

    private Rigidbody rb;
    private BalanceLogic balanceLogic;

    public bool IsBalancing { get; private set; }

    void Start()
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

        float inclinacion = balanceLogic.balanceHandle * 20f;
        playerCamera.transform.localRotation = Quaternion.Euler(0, 0, -inclinacion);

        if (balanceLogic.CheckIfFallen())
        {
            IsBalancing = false;
            ResetScene.Reset();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Equilibrio"))
        {
            IsBalancing = true;
            balanceLogic.Reset();
        }

        if (other.CompareTag("Fin"))
        {
            IsBalancing = false;
            playerCamera.transform.localRotation = Quaternion.identity;
        }
    }

}
