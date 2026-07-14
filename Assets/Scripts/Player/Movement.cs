
using UnityEngine;
using UnityEngine.XR;


public class Movement
{
    #region VARIABLES: PARÁMETROS Y ESTADOS
    private float speed = 5f;
    private float crouchSpeed = 2.5f;
    public bool IsCrouching { get; private set; }
    #endregion

    #region VARIABLES: COMPONENTES Y REFERENCIAS 
    private readonly Transform transform;
    private readonly Rigidbody rb;
    private readonly Animator anim;
    private readonly AudioSource audioSource;
    private readonly AudioClip walkSound;
    private readonly AudioClip jumpSound;
    float baseSpeed;
    #endregion

    #region CONSTRUCTOR
    public Movement(Transform _transform, float _speed, float _crouchSpeed, Rigidbody _rb, Animator _anim,
        AudioSource _audioSource, AudioClip _walkSound, AudioClip _jumpSound)
    {
        transform = _transform;
        speed = _speed;
        crouchSpeed = _crouchSpeed;
        rb = _rb;
        anim = _anim;
        audioSource = _audioSource;
        walkSound = _walkSound;
        jumpSound = _jumpSound;

        if (audioSource != null && walkSound != null)
        {
            audioSource.clip = walkSound;
            audioSource.loop = true;
        }
    }
    #endregion

    #region MÉTODOS PÚBLICOS: INTERFAZ DE CONTROL

    // Movimiento horizontal y las físicas del personaje 

    public void move(Vector3 dir, bool hasControl)
    {
        bool grounded = IsGrounded();
        bool moving = dir.magnitude > 0.1f;

        float currentSpeed = IsCrouching ? crouchSpeed : speed;
        Vector3 horizontalVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        anim.SetBool("IsJumping", !grounded);
        bool highSpeed = horizontalVel.magnitude > currentSpeed;

        if (highSpeed && !hasControl)
        {
            anim.SetBool("IsJumping", !grounded);
            anim.SetBool("IsWalking", false);
            WalkSoundControl(false);
            return;
        }

        // Aplicación de físicas de movimiento
        if (grounded)
        {
            rb.linearVelocity = new Vector3(dir.x * currentSpeed, rb.linearVelocity.y, dir.z * currentSpeed);
        }
        else
        {
            if (horizontalVel.magnitude > currentSpeed)
            {
                return;
            }
            rb.AddForce(dir * currentSpeed, ForceMode.Acceleration);
        }

        anim.SetBool("IsJumping", !grounded);
        anim.SetBool("IsCrouching", IsCrouching);

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

    public void jump()
    {
        if (IsGrounded())
        {
            if (IsCrouching)
            {
                IsCrouching = false;
                anim.SetBool("IsCrouching", false);
            }

            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            anim.SetBool("IsJumping", true);
            WalkSoundControl(false);

            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }

    public void ToggleCrouch()
    {
        IsCrouching = !IsCrouching;
        anim.SetBool("IsCrouching", IsCrouching);
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
    #endregion

    #region MÉTODOS PRIVADOS: LÓGICA INTERNA
    private void WalkSoundControl(bool reproducir)
    {
        if (audioSource == null) return;

        if (reproducir && !audioSource.isPlaying) audioSource.Play();
        else if (!reproducir && audioSource.isPlaying) audioSource.Pause();
    }

    public void RunBoost(bool activo)
    {
        speed = activo ? baseSpeed * 1.25f : baseSpeed;
    }
    #endregion
}