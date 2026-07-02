using System;
using UnityEngine;
using System.Collections;
public class PowerUp : MonoBehaviour
{
    [SerializeField] ParticleSystem particulas;
    public static event Action OnCharge;
    public static event Action OffCharge;


    public void Activar()
    {
        StopAllCoroutines();
        StartCoroutine(DirectMode());
    }
  
    private IEnumerator DirectMode()
    {
        OnCharge?.Invoke();
        particulas.Play();
        yield return new WaitForSeconds(10f);
        OffCharge?.Invoke();
        particulas.Stop();
    }


}
