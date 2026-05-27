using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.XR;

public class Movement
{
    Transform transform;
    Rigidbody rb;
    float speed = 5;
    Animator anim;
 
    public Movement(Transform _transform, float _speed, Rigidbody _rb, Animator _anim)
    {
        transform = _transform;
        speed = _speed;
        rb = _rb;
        anim = _anim;
    }
    public void move(Vector3 dir)
    {
        bool grounded = IsGrounded();
        bool moving = dir.magnitude > 0.1f;

        float force = grounded ? speed : speed * 0.15f;
        rb.AddForce(dir * force, ForceMode.Acceleration);

        anim.SetBool("IsJumping", !grounded);
        

        if (grounded && moving)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }
    public bool IsGrounded()
    {
        
        return Physics.BoxCast(transform.position, new Vector3(0.3f, 0.1f, 0.3f), Vector3.down, Quaternion.identity, 1.1f);
    }

    public void jump()
    {
        if (IsGrounded()) 
        {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            anim.SetBool("IsJumping", true);
        }
        
    }

    public void CheckGroundedStatus()
    {
        if (IsGrounded())
        {
            anim.SetBool("IsJumping", false);
        }
        else
        {
            anim.SetBool("IsJumping", true);
        }
    }
}
