using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlCamera : MonoBehaviour
{
    public InputActionReference LookControl;
    public Transform Player;
    Vector2 move;
    public float LeftR;
    float UpD;
    [SerializeField] float sens = 1;

   
    void Update()
    {
        move = LookControl.action.ReadValue<Vector2>();

        LeftR += move.x * sens * Time.deltaTime;
        UpD += move.y * sens * Time.deltaTime;

        UpD = Mathf.Clamp(UpD, -80, 80);

        transform.rotation = Quaternion.Euler(-UpD, LeftR, 0);
        Player.rotation = Quaternion.Euler(0, LeftR, 0);
    }
}
