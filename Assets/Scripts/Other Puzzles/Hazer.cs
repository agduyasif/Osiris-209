using UnityEngine;
using System.Collections;

public class Hazer : MonoBehaviour
{
    float tiempoActivo = 4f;
    float tiempoEspera = 8f;
    private bool vientoPrendido = false;
    private ParticleSystem particulas;

    void Start()
    {
        particulas = GetComponentInChildren<ParticleSystem>();
        StartCoroutine(CicloViento());
    }

    IEnumerator CicloViento()
    {
        while (true)
        {
            vientoPrendido = false;
            if (particulas) particulas.Stop();
            yield return new WaitForSeconds(tiempoEspera);

            vientoPrendido = true;
            if (particulas) particulas.Play();
            yield return new WaitForSeconds(tiempoActivo);
        }
    }

    

    void OnTriggerStay(Collider other)
    {
        if (vientoPrendido && other.gameObject.layer == 6)
        {
            ResetScene.Reset();
        }
    }
}