using UnityEngine;
using System.Collections;

public class TriggerCinematica : MonoBehaviour
{
    [Header("Configuración de Cinemática")]
    [SerializeField] private Transform _cameraA;
    [SerializeField] private Transform _cameraB;
    [SerializeField] private float _travelTime = 5f;
    [SerializeField] private GameObject modeloGancho;
    [SerializeField] private GameObject _uiCredits;
    [SerializeField] private AudioSource _horrorSound;

    [Header("Bandas Laterales")]
    [SerializeField] private RectTransform _bandaINF;
    [SerializeField] private RectTransform _bandaSUP;
    [SerializeField] private float _finalPOS = 150f;

    private void Start()
    {
        if (_bandaSUP != null)
            _bandaSUP.sizeDelta = new Vector2(_bandaSUP.sizeDelta.x, 0);

        if (_bandaINF != null)
            _bandaINF.sizeDelta = new Vector2(_bandaINF.sizeDelta.x, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player playerScript = other.GetComponent<Player>();

        if (playerScript != null)
        {
            playerScript.enabled = false;

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
            }

            Animator anim = other.GetComponentInChildren<Animator>();
            if (anim != null)
            {
                anim.SetBool("IsWalking", false);
            }

            AudioSource audio = other.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Stop();

                if (modeloGancho != null)
                {
                    modeloGancho.SetActive(false); 
                }

                Camera principalCamara = Camera.main;

                if (principalCamara != null)
                {
                    principalCamara.transform.SetParent(null);

                    if (_horrorSound != null)
                    {
                        _horrorSound.Play();
                    }

                    StartCoroutine(cameraMovement(principalCamara.transform));
                }
            }
        }
    }
    private IEnumerator cameraMovement(Transform cam)
    {
        cam.position = _cameraA.position;
        cam.rotation = _cameraA.rotation;

        if (_bandaINF != null) _bandaINF.sizeDelta = new Vector2(_bandaINF.sizeDelta.x, 0);
        if (_bandaSUP != null) _bandaSUP.sizeDelta = new Vector2(_bandaSUP.sizeDelta.x, 0);

        float _timePassed = 0f;

        while (_timePassed < _travelTime)
        {
            _timePassed += Time.deltaTime;
            float porcentaje = _timePassed / _travelTime;   

            float suavizado = Mathf.SmoothStep(0f, 1f, porcentaje);

            cam.position = Vector3.Lerp(_cameraA.position, _cameraB.position, suavizado);
            cam.rotation = Quaternion.Slerp(_cameraA.rotation, _cameraB.rotation, suavizado);

            float _actualHeight = Mathf.Lerp(0f, _finalPOS, suavizado);

            if (_bandaINF != null)
                _bandaINF.sizeDelta = new Vector2(_bandaINF.sizeDelta.x, _actualHeight);
            if (_bandaSUP != null)
                _bandaSUP.sizeDelta = new Vector2(_bandaSUP.sizeDelta.x, _actualHeight);

            yield return null;
        }

        cam.position = _cameraB.position;
        cam.rotation = _cameraB.rotation;

        if (_uiCredits != null)
        {
            _uiCredits.SetActive(true);
            _bandaSUP.sizeDelta = new Vector2(_bandaSUP.sizeDelta.x, 0);
            _bandaINF.sizeDelta = new Vector2(_bandaINF.sizeDelta.x, 0);
        }

    }
}
