using UnityEngine;
using System.Collections;

public class CaveAmbientation : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private AudioSource _audioGotas;

    [Header("Tiempos (en segundos)")]
    [SerializeField] private float tiempoMinimo = 2f;
    [SerializeField] private float tiempoMaximo = 6f;

    void Start()
    {
        StartCoroutine(CicloDeGotas());
    }

    IEnumerator CicloDeGotas()
    {

        while (true)
        {
            float esperaAleatoria = Random.Range(tiempoMinimo, tiempoMaximo);

            yield return new WaitForSeconds(esperaAleatoria);

            if (_audioGotas != null)
            {
                _audioGotas.pitch = Random.Range(0.8f, 1.2f);
                _audioGotas.Play();
            }
        }
    }
}
