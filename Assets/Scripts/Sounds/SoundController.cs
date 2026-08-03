using UnityEngine;
using System.Collections.Generic;
public class SoundController : MonoBehaviour
{
    [Header("Configuración de Audios")]
    [SerializeField] private AudioClip sonidoPiedra;
    [SerializeField] private AudioClip sonidoMadera;
    Dictionary<MaterialList, AudioClip> sonidos;

    void Awake()
    {
        sonidos = new Dictionary<MaterialList, AudioClip>
    {
        { MaterialList.Piedra, sonidoPiedra },
        { MaterialList.Madera, sonidoMadera }
    };
    }

    public AudioClip IMP_Sound(GameObject objectIMP)
    {
        // Buscamos el script que creamos antes
        SuperficieAgarrable superficie = objectIMP.GetComponent<SuperficieAgarrable>();

        if (superficie != null)
        {
            if (sonidos.ContainsKey(superficie.materialType))
            {
                return sonidos[superficie.materialType];
            }
        }

        return null;
    }
}
