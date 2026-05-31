using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.XR;

public class Movement
{
    Transform transform;
    Rigidbody rb;
    float speed = 5;
    Animator anim;
    AudioSource audioSource;
    AudioClip sonidoCamina;
    AudioClip sonidoSalto;

    public Movement(Transform _transform, float _speed, Rigidbody _rb, Animator _anim, 
        AudioSource _audioSource, AudioClip _sonidoCamina, AudioClip sonidoSalto)
    {
        transform = _transform;
        speed = _speed;
        rb = _rb;
        anim = _anim;
        audioSource = _audioSource;
        sonidoCamina = _sonidoCamina;

        if (audioSource != null && sonidoCamina != null)
        {
            audioSource.clip = sonidoCamina;
            audioSource.loop = true;
        }

        this.sonidoSalto = sonidoSalto;
    }

   private void WalkSoundControl(bool reproducir)
    {
        if (audioSource == null) return;

        if (reproducir && !audioSource.isPlaying) audioSource.Play();
        else if (!reproducir && audioSource.isPlaying) audioSource.Pause();
    }

    public void move(Vector3 dir, bool hasControl)
    {
        bool grounded = IsGrounded();
        bool moving = dir.magnitude > 0.1f;

        Vector3 horizontalVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        anim.SetBool("IsJumping", !grounded);
        bool highSpeed = horizontalVel.magnitude > speed * speed;

        if (highSpeed && !hasControl)
        {
            anim.SetBool("IsJumping", !grounded);
            anim.SetBool("IsWalking", false);
            WalkSoundControl(false);
            return;
        }

        if (grounded)
        {
            rb.linearVelocity = new Vector3(dir.x * speed, rb.linearVelocity.y, dir.z * speed);
        }
        else
        {
            rb.AddForce(dir * speed, ForceMode.Acceleration);
        }

        anim.SetBool("IsJumping", !grounded);

        if (grounded && moving)
        {
            anim.SetBool("IsWalking", true);
            WalkSoundControl(true); 
        }
        else
        {
            anim.SetBool("IsWalking", false);
            WalkSoundControl(false); 
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
            WalkSoundControl(false); 
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
            WalkSoundControl(false);
            audioSource.PlayOneShot(sonidoSalto);
        }
    }

}