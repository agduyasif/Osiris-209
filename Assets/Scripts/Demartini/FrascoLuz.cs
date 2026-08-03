using UnityEngine;
using UnityEngine.UI;

public class FrascoLuz : MonoBehaviour
{
    [SerializeField] Light luz;
    int luciernagas = 0;
    private void Start()
    {
        luz.enabled = false;
    }

    public void sumar()
    {
        luciernagas ++;

        if (luciernagas >= 2)
        {
            luz.enabled = true;
        }
    }
}
