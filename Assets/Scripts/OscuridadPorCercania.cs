using UnityEngine;

public class OscuridadPorCercania : MonoBehaviour
{
    [SerializeField] Transform jugador;
    [SerializeField] float distanciaMax = 70f;
    [SerializeField] float distanciaMin = 40f;
    [SerializeField] float distanciaActual;   // solo para mirar
    [SerializeField] float oscuridadActual;   // solo para mirar

    void Update()
    {
        distanciaActual = Vector3.Distance(jugador.position, transform.position);
        oscuridadActual = Mathf.InverseLerp(distanciaMax, distanciaMin, distanciaActual);
        Shader.SetGlobalFloat("_Oscuridad", oscuridadActual);
    }
}
