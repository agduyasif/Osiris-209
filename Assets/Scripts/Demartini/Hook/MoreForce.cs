using UnityEngine;
using System.Collections;
public class MoreForce : MonoBehaviour
{
    [SerializeField] Shoothook shoothook;

    public void ActivarForce()
    {
        StopAllCoroutines();
        StartCoroutine(Moreforce());
    }

    private IEnumerator Moreforce()
    {
        float original = shoothook.pullSpeed;
        shoothook.pullSpeed = 10;
        yield return new WaitForSeconds(10);
        shoothook.pullSpeed = original;
    }
   
}
