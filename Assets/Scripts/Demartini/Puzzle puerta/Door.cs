using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [Header("Configuración")]
    public float openHeight = 5f;
    [SerializeField] float smooth = 0.4f;
    [SerializeField] Weight platform;

    [Header("Sonidos de la Puerta")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _sonidoAbrir;
    [SerializeField] private AudioClip _sonidoCerrar;

    Vector3 closedPos;
    Vector3 openPos;
    Vector3 target;
    Vector3 velocity;

    private void OnEnable()
    {
        platform.OnActivated += open;
        platform.OnDeactivated += close;
    }

    private void OnDisable()
    {
        platform.OnActivated -= open;
        platform.OnDeactivated -= close;
    }

    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + Vector3.up * openHeight;
        target = closedPos;
    }


    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smooth);
    }

    public void open()
    {
        target = openPos;

        if (_audioSource != null && _sonidoAbrir != null)
        {
            _audioSource.PlayOneShot(_sonidoAbrir);
        }
    }

    public void close() 
    {
        target = closedPos;

        if (_audioSource != null && _sonidoCerrar != null)
        {
            _audioSource.PlayOneShot(_sonidoCerrar);
        }
    }

}
