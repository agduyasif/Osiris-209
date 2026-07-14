
using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour
{
    [SerializeField] ControlCamera camara;
    [SerializeField] Transform player;
    [SerializeField] ParticleSystem GravityInvertedPart;
    [SerializeField] ParticleSystem GravityPart;
    bool invertido = false;

    public void flip()
    {
        invertido = !invertido;

        if (invertido)
        {
            Physics.gravity = new Vector3(0, 9.81f, 0);
            GravityInvertedPart.Play();
        }
        else
        {
            Physics.gravity = new Vector3(0, -9.81f, 0);
            GravityPart.Play();
        }

        StopAllCoroutines();
        StartCoroutine(RotarCamara());
    }

    private IEnumerator RotarCamara()
    {
        yield return new WaitForSeconds(1f);
        camara.xRotation = invertido ? 180 : 0;
    }

}
