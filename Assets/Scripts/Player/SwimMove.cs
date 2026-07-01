using UnityEngine;
using UnityEngine.InputSystem;

public class SwimMove
{
    Rigidbody rb;
    Transform cam;
    Control control;
    float swinForce = 10;

    public SwimMove(Rigidbody rb, Transform cam, Control control)
    {
        this.rb = rb;
        this.cam = cam;
        this.control = control;
        
    }

    public void Swim()
    {
        Vector2 input = control.GetInput();
        if (input.y > 0.1f)
        {
            rb.AddForce(cam.forward * swinForce, ForceMode.Acceleration);
        }
        if (Keyboard.current.spaceKey.isPressed)
        { rb.AddForce(Vector3.up * swinForce, ForceMode.Acceleration); }
        else if (Keyboard.current.leftShiftKey.isPressed)
        {
            rb.AddForce(Vector3.down * swinForce, ForceMode.Acceleration);
        }
    }


}
