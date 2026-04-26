using UnityEngine;

public class Explosive : MonoBehaviour
{
    float time;
    private void Update()
    {
        time += Time.deltaTime;

        if (time > 3)
        {
            Collider[] o = Physics.OverlapSphere(transform.position, 2);
        }
    }
    private void OnDrawGizmosSelected()
    {
        // 1. Le damos un color al dibujo (Rojo queda bien para explosiones)
        Gizmos.color = Color.red;

        // 2. Le decimos que dibuje una esfera de alambre (WireSphere) 
        // en la misma posición de la granada y con el mismo radio de tu explosión.
        Gizmos.DrawWireSphere(transform.position, 2);
    }
}
