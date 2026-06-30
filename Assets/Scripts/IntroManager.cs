using System.Collections;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [Header("Referencias UI y Sonido")]
    [SerializeField] private AudioSource _crashSound;
    [SerializeField] private Animator _screenAnimator; 

    [Header("Jugador")]
    [SerializeField] private MonoBehaviour _playerScript; 

    void Start()
    {
        if (_playerScript != null)
            _playerScript.enabled = false;

        StartCoroutine(SecuenciaIntro());
    }

    IEnumerator SecuenciaIntro()
    {
        yield return new WaitForSeconds(16f);

        if (_screenAnimator != null)
        {
            _screenAnimator.Play("ParpadeoIntro");
        }

        yield return new WaitForSeconds(2.0f);

        if (_playerScript != null)
            _playerScript.enabled = true;

        _screenAnimator.gameObject.transform.parent.gameObject.SetActive(false);
    }
}

