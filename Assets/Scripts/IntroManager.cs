using System.Collections;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [Header("Referencias UI y Sonido Intro")]
    [SerializeField] private AudioSource _crashSound;
    [SerializeField] private Animator _screenAnimator;

    [Header("Sonidos de Ambiente (Juego)")]
    [SerializeField] private AudioSource _musicaFondo;
    [SerializeField] private AudioSource _sonidoPajaros;

    [Header("Jugador")]
    [SerializeField] private MonoBehaviour _playerScript;
    private static bool introVista = false;

    void Start()
    {
        if (introVista)
        {
            if (_playerScript != null)
                _playerScript.enabled = true;

            _screenAnimator.gameObject.transform.parent.gameObject.SetActive(false);

            ActivarAmbiente();
            return;
        }

        introVista = true;

        if (_crashSound != null)
        {
            _crashSound.Play();
        }

        if (_playerScript != null)
            _playerScript.enabled = false;

        StartCoroutine(SecuenciaIntro());
    }

    IEnumerator SecuenciaIntro()
    {
        yield return new WaitForSeconds(13.5f);

        if (_screenAnimator != null)
        {
            _screenAnimator.Play("ParpadeoIntro");
        }

        yield return new WaitForSeconds(2.0f);

        if (_playerScript != null)
            _playerScript.enabled = true;

        ActivarAmbiente();

        _screenAnimator.gameObject.SetActive(false);
    }

    private void ActivarAmbiente()
    {
        if (_musicaFondo != null && !_musicaFondo.isPlaying)
            _musicaFondo.Play();

        if (_sonidoPajaros != null && !_sonidoPajaros.isPlaying)
            _sonidoPajaros.Play();
    }
}

