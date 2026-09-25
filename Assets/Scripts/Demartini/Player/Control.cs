using UnityEngine;
using UnityEngine.InputSystem;

public class Control
{
    #region VARIABLES: REFERENCIAS PÚBLICAS
    public InputActionReference MoveControl;
    public Movement movement;
    #endregion

    #region VARIABLES: PRIVADAS INTERNAS
    private readonly Transform transform;
    private readonly bool vibration = true;
    #endregion

    #region CONSTRUCTOR
    public Control(InputActionReference _move, Movement _movement, Transform _transform, bool _vibration)
    {
        MoveControl = _move;
        movement = _movement;
        transform = _transform;
        vibration = _vibration;
    }
    #endregion

    #region MÉTODOS PÚBLICOS: INPUT
    public void ArtificialUpdate(bool hasControl)
    {
        Vector3 dir = getDir();

        // Control de Agachado (Shift Izquierdo)
       

        movement.move(dir, hasControl);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            movement.jump();
        }

        /* FRAN si lo vas a usar, cambié 'move.magnitude' por 'dir.magnitude' para que no te tire error 
        pq el 'move' solo existe dentro de getDir().
        igualmente tengo la otra version copiada si la necesitas*/

        /*
        if (dir.magnitude > 0.1f && vibration == true)
        {
            VibrationMeter.Instance.AddVibration(30f * Time.deltaTime);
            VibrationSystem.Instance.CreateVibration(transform.position, 10f);
        }
        */
    }

    public Vector3 getDir()
    {
        Vector2 moveInput = MoveControl.action.ReadValue<Vector2>();

        Vector3 dir = transform.forward * moveInput.y;
        dir += transform.right * moveInput.x;

        return dir;
    }

    public Vector2 GetInput()
    {
        return MoveControl.action.ReadValue<Vector2>();
    }

    #endregion
}
