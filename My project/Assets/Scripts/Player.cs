using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputActionReference controlMove;
    public InputActionReference hookAction; 

    public int speed;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        controlMove.action.Enable();
        //hookAction.action.Enable();

        //hookAction.action.performed += OnHook;
    }

    private void OnDisable()
    {
        controlMove.action.Disable();
        //hookAction.action.Disable();

        //hookAction.action.performed -= OnHook;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = controlMove.action.ReadValue<Vector2>();

        Vector3 dir = transform.forward * move.y;
        dir += transform.right * move.x;
        transform.position += dir * speed * Time.deltaTime;

        if (move.magnitude > 0.1f)
        {
            VibrationMeter.Instance.AddVibration(30f * Time.deltaTime);
            VibrationSystem.Instance.CreateVibration(transform.position, 10f);

            Debug.Log(move);
        }
    } 

    void OnHook(InputAction.CallbackContext ctx)
    {
        VibrationMeter.Instance.AddVibration(25f);
        VibrationSystem.Instance.CreateVibration(transform.position, 12f);
    }
}
