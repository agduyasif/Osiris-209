using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("Configuración de Audios")]
    [SerializeField] private AudioClip sonidoPiedra;
    [SerializeField] private AudioClip sonidoMadera;

    public AudioClip IMP_Sound(GameObject objectIMP)
    {
        // Buscamos el script que creamos antes
        SuperficieAgarrable superficie = objectIMP.GetComponent<SuperficieAgarrable>();

        if (superficie != null)
        {
            switch (superficie.materialType)
            {
                case MaterialList.Piedra:
                    return sonidoPiedra;
                case MaterialList.Madera:
                    return sonidoMadera;
            }
        }

        return null;
    }
}
