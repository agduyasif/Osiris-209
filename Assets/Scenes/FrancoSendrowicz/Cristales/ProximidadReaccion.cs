using UnityEngine;

public class ProximidadReaccion : MonoBehaviour
{
    private static readonly int IdDistancia = Shader.PropertyToID("_DistanciaJugador");
    private static readonly int IdIntensidad = Shader.PropertyToID("_Intensidad");

    public Transform jugador;
    public Material[] cristalesReactivos;
    public Material materialPostProceso;
    public float velocidadFade = 2f;

    private bool jugadorAdentro = false;
    private float intensidadActual = 0f;

    void Start()
    {
        AplicarIntensidad(0f);
    }

    void OnDisable()
    {
        AplicarIntensidad(0f);
    }

    void Update()
    {
        if (jugador != null && cristalesReactivos != null)
        {
            float d = Vector3.Distance(transform.position, jugador.position);
            foreach (Material mat in cristalesReactivos)
            {
                if (mat != null) mat.SetFloat(IdDistancia, d);
            }
        }

        float objetivo = jugadorAdentro ? 1f : 0f;
        intensidadActual = Mathf.MoveTowards(intensidadActual, objetivo, velocidadFade * Time.deltaTime);
        AplicarIntensidad(intensidadActual);
    }

    void AplicarIntensidad(float valor)
    {
        if (materialPostProceso != null)
        {
            materialPostProceso.SetFloat(IdIntensidad, valor);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (jugador != null && other.transform.IsChildOf(jugador))
        {
            jugadorAdentro = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (jugador != null && other.transform.IsChildOf(jugador))
        {
            jugadorAdentro = false;
        }
    }
}