using UnityEngine;
using UnityEngine.UI;

public class FrascoLuz : MonoBehaviour
{
    [SerializeField] Light luz;
    Image mediolleno;
    Image lleno;
    Image vacio;
    GameObject pared;
    public int luciernagas { get; private set;}
    private void Start()
    {
        luz.enabled = false;
        mediolleno = GameObject.FindWithTag("MedioLleno").GetComponent<Image>();
        lleno = GameObject.FindWithTag("Lleno").GetComponent<Image>();
        vacio = GameObject.FindWithTag("vacio").GetComponent<Image>();
        pared = GameObject.FindWithTag("Pared").GetComponent<GameObject>();
        luciernagas = 0;
    }

    public void sumar()
    {
        luciernagas ++;

        if (luciernagas == 1) 
        {
            mediolleno.enabled = true;
            vacio.enabled = false;
        }

        if (luciernagas >= 2)
        {
            luz.enabled = true;
            lleno.enabled = true;
            mediolleno.enabled = false;
            Destroy(pared);
        }
    }
}
