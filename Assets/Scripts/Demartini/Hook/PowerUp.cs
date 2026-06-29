using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public static event Action OnCharge;
    public static event Action OffCharge;


    public void Activar()
    {
        StopAllCoroutines();
        StartCoroutine(DirectMode());
    }

    private System.Collections.IEnumerator DirectMode()
    {
        OnCharge?.Invoke();
        yield return new WaitForSeconds(10f);
        OffCharge?.Invoke();
    }
}
