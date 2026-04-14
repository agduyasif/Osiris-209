using UnityEngine;

public class Movement
{
    Transform transform;
    Rigidbody rb;
    float speed = 5;

 
    public Movement(Transform _transform, float _speed, Rigidbody _rb)
    {
        transform = _transform;
        speed = _speed;
        rb = _rb;
    }
    public void move(Vector3 dir)
    {

        transform.position += dir * speed * Time.deltaTime;
        
    }
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    public void jump()
    {
        if (IsGrounded()) { rb.AddForce(Vector3.up * 5, ForceMode.Impulse); }
        
    }
}
