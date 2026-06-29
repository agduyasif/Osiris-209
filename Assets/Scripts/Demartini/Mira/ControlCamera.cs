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
    public float UpD;
    [SerializeField] float sens = 1;
    SistemaMira mira;
    public float xRotation = 0;
    /*[SerializeField] Camera Mcamera;
    [SerializeField] Image nothing;
    [SerializeField] Image CanGrab;
    [SerializeField] Image CanGrapple;

    private void Start()
    {
        mira = new SistemaMira(Mcamera, nothing, CanGrab, CanGrapple);
    }*/
    void Update()
    {
        move = LookControl.action.ReadValue<Vector2>();
        float factor = xRotation == 180 ? -1 : 1;

        LeftR += move.x * sens * Time.deltaTime * factor;
        UpD += move.y * sens * Time.deltaTime * factor;

        UpD = Mathf.Clamp(UpD, -80, 80);

        transform.rotation = Quaternion.Euler(-UpD, LeftR, xRotation);
        Player.rotation = Quaternion.Euler(0, LeftR, xRotation);

        //mira.MiraUpdate();
    }
}
