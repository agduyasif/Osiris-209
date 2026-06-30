using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements.Experimental;
public class PowerUp : MonoBehaviour
{
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
        yield return new WaitForSeconds(10f);
        OffCharge?.Invoke();
    }


}
