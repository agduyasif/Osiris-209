using UnityEngine;

public class GrappleMove
{
    Rigidbody rb;
    float pushForce;
    Control control;
    public GrappleMove(Rigidbody _rb, Control _control, float _pushForce)
    {
        rb = _rb;
        control = _control;
        pushForce = _pushForce;
    }

    public void Push()
    {
        Vector3 dir = control.getDir();
        rb.AddForce(dir * pushForce, ForceMode.Acceleration);
    }
}
