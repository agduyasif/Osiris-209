using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public InputActionReference controlLook;
    public Transform playerTransform;
    private Vector2 lookInput;
    private float upDown;
    private float leftRight;
    public float sensitivity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lookInput = controlLook.action.ReadValue<Vector2>();

        upDown -= lookInput.y * sensitivity;
        leftRight += lookInput.x * sensitivity;

        upDown = Mathf.Clamp(upDown, -60f, 60f);
        transform.rotation = Quaternion.Euler(upDown, leftRight, 0);
        playerTransform.rotation = Quaternion.Euler(0, leftRight, 0);
    }
}
