using UnityEngine;

public class GrappleMove
{
    Rigidbody rb;
    float pushForce;
    Control control;
    Vector3 anchorPointo;

    public GrappleMove(Rigidbody _rb, Control _control, float _pushForce)
    {
        rb = _rb;
        control = _control;
        pushForce = _pushForce;
    }

    public void Push()
    {


        Vector3 dir = control.getDir();

        Vector3 ropeDir = (rb.position - anchorPointo).normalized;
        Vector3 tangentelDir = (dir - Vector3.Project(dir, ropeDir)).normalized;


        Vector3 tangentialVel = rb.linearVelocity - Vector3.Project(rb.linearVelocity, ropeDir);
        float currentSwingSpeed = tangentialVel.magnitude;

        if (currentSwingSpeed > 15) return;

        float scaledForce = pushForce * Mathf.Max(currentSwingSpeed, 1f);
        rb.AddForce(tangentelDir * scaledForce, ForceMode.Acceleration);
    }

    public void setAnchor(Vector3 point)
    {
        anchorPointo = point;
    }

}
