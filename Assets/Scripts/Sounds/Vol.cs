using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Vol : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioMixer _mixerPrincipal;
    [SerializeField] private Slider _sliderMusica;
    [SerializeField] private Slider _sliderSFX;
    [SerializeField] private float _initialVol = 0.50f;

    void Start()
    {
        if (_sliderMusica != null)
            _sliderMusica.value = PlayerPrefs.GetFloat("VolMusicaGuardado", _initialVol);

        if (_sliderSFX != null)
            _sliderSFX.value = PlayerPrefs.GetFloat("VolSFXGuardado", _initialVol);

        SetVolumenMusica();
        SetVolumenSFX();
    }

    public void SetVolumenMusica()
    {
        float volumen = _sliderMusica.value;

        if (volumen <= 0.0001f) volumen = 0.0001f;

        _mixerPrincipal.SetFloat("VolMusica", Mathf.Log10(volumen) * 20);
        PlayerPrefs.SetFloat("VolMusicaGuardado", _sliderMusica.value);
    }

    public void SetVolumenSFX()
    {
        float volumen = _sliderSFX.value;

        if (volumen <= 0.0001f) volumen = 0.0001f;

        _mixerPrincipal.SetFloat("VolSFX", Mathf.Log10(volumen) * 20);
        PlayerPrefs.SetFloat("VolSFXGuardado", _sliderSFX.value);
    }
}
