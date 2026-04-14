using UnityEngine;
using UnityEngine.InputSystem;
public class Control 
{
    public InputActionReference MoveControl;
    Transform transform;

    public Movement movement;

    public Control(InputActionReference _move, Movement _movement, Transform _trasform)
    {
        movement = _movement;
        MoveControl = _move;
        transform = _trasform;
    }
    public void ArtificialUpdate()
    {
        Vector2 move = MoveControl.action.ReadValue<Vector2>();

        Vector3 dir = transform.forward * move.y;
        dir += transform.right * move.x;

        movement.move(dir);

        if (Keyboard.current.spaceKey.wasPressedThisFrame) {
            movement.jump();
        }
    }
}
