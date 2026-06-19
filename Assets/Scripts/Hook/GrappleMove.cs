using UnityEngine;

public class GrappleMove
{
    
    Rigidbody rb;
    float pushForce;
    Control control;
    Vector3 anchorPointo;
    float currentSwingSpeed;
    Transform cam;
    float baseJump = 0f;
    Shoothook shoothook;


    public GrappleMove(Rigidbody _rb, Control _control, float _pushForce, Transform _cam)
    {
        rb = _rb;
        control = _control;
        pushForce = _pushForce;
        cam = _cam;
    }

    public void Push()
    {
        Vector3 dir = control.getDir();
        Vector3 ropeDir = (rb.position - anchorPointo).normalized;
        Vector3 tangentelDir = (dir - Vector3.Project(dir, ropeDir)).normalized;
        Vector3 tangentialVel = rb.linearVelocity - Vector3.Project(rb.linearVelocity, ropeDir);
        currentSwingSpeed = tangentialVel.magnitude;
        float scaledForce = pushForce * Mathf.Max(currentSwingSpeed, 1f);
        if (currentSwingSpeed > 15f)
        {
            Debug.LogError("LimitSpeed");
            return;
        }
        Debug.Log("Speed " + currentSwingSpeed);
        rb.AddForce(tangentelDir * scaledForce, ForceMode.Acceleration);
    }

    public void setAnchor(Vector3 point)
    {
        anchorPointo = point;
    }

    public void Jump()
    {
        float jumpForce = baseJump + currentSwingSpeed;
        shoothook.Release();

        Vector3 jumpDir = (rb.linearVelocity.normalized + Vector3.up * 0.3f).normalized;
        rb.AddForce(jumpDir * jumpForce, ForceMode.Impulse);
    }
   

    public void SetShoothook(Shoothook _shoothook)
    {
        shoothook = _shoothook;
    }
}
