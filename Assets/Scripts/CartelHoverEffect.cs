using UnityEngine;
using UnityEngine.EventSystems;

public class CartelHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Asigná acá la FACHADA (la imagen del cartel)")]
    public GameObject imagenAMover;

    public float anguloInclinacion = 5f;
    private Quaternion rotacionOriginal;

    void Start()
    {
        if (imagenAMover == null)
        {
            imagenAMover = this.gameObject;
        }

        rotacionOriginal = imagenAMover.transform.rotation;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imagenAMover.transform.rotation = rotacionOriginal * Quaternion.Euler(0, 0, anguloInclinacion);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imagenAMover.transform.rotation = rotacionOriginal;
    }
}