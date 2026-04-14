using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
    public InputActionReference LookControl;
    public Transform Player;
    Vector2 move;
    public float LeftR;
    float UpD;

   
    void Update()
    {
        move = LookControl.action.ReadValue<Vector2>();

        LeftR += move.x;
        UpD += move.y;

        UpD = Mathf.Clamp(UpD, -60, 60);

        transform.rotation = Quaternion.Euler(-UpD, LeftR, 0);
        Player.rotation = Quaternion.Euler(0, LeftR, 0);
    }
}
