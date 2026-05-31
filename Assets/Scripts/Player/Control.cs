using UnityEngine;
using UnityEngine.InputSystem;
public class Control
{
    public InputActionReference MoveControl;
    Transform transform;
    bool vibration = true;
    public Movement movement;

    public Control(InputActionReference _move, Movement _movement, Transform _trasform, bool _vibration)
    {
        movement = _movement;
        MoveControl = _move;
        transform = _trasform;
        vibration = _vibration;
    }

    public void ArtificialUpdate(bool hasControl)
    {
        Vector2 move = MoveControl.action.ReadValue<Vector2>();

        Vector3 dir = getDir();

        movement.move(dir, hasControl);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            movement.jump();
        }

        /*if (move.magnitude > 0.1f && vibration == true)
        {
            VibrationMeter.Instance.AddVibration(30f * Time.deltaTime);
            VibrationSystem.Instance.CreateVibration(transform.position, 10f);

        }*/
    }

    public Vector3 getDir()
    {
        Vector2 move = MoveControl.action.ReadValue<Vector2>();
        Vector3 dir = transform.forward * move.y;
        dir += transform.right * move.x;
        return dir;
    }

}
