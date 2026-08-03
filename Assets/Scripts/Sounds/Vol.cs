using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Vol : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioMixer _mixerPrincipal;
    [SerializeField] private Slider _sliderMusica;
    [SerializeField] private Slider _sliderSFX;

    void Start()
    {
        SetVolumenMusica();
        SetVolumenSFX();
    }

    public void SetVolumenMusica()
    {
        float volumen = _sliderMusica.value;
        _mixerPrincipal.SetFloat("VolMusica", Mathf.Log10(volumen) * 20);
    }

    public void SetVolumenSFX()
    {
        float volumen = _sliderSFX.value;
        _mixerPrincipal.SetFloat("VolSFX", Mathf.Log10(volumen) * 20);
    }
}
