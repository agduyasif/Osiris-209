using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class ControlCamera : MonoBehaviour
{
    public InputActionReference LookControl;
    public Transform Player;
    Vector2 move;
    public float LeftR;
    float UpD;
    [SerializeField] float sens = 1;
    SistemaMira mira;

    [SerializeField] Camera Mcamera;
    [SerializeField] Image nothing;
    [SerializeField] Image CanGrab;
    [SerializeField] Image CanGrapple;

    private void Start()
    {
        mira = new SistemaMira(Mcamera, nothing, CanGrab, CanGrapple);
    }
    void Update()
    {
        move = LookControl.action.ReadValue<Vector2>();

        LeftR += move.x * sens * Time.deltaTime;
        UpD += move.y * sens * Time.deltaTime;

        UpD = Mathf.Clamp(UpD, -80, 80);

        transform.rotation = Quaternion.Euler(-UpD, LeftR, 0);
        Player.rotation = Quaternion.Euler(0, LeftR, 0);

        mira.MiraUpdate();
    }
}
