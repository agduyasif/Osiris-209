using System.ComponentModel;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] ControlCamera camara;
    [SerializeField] Transform player;
    bool invertido = false;

    public void flip()
    {
        invertido = !invertido;

        if (invertido)
        {
            Physics.gravity = new Vector3(0, 9.81f, 0);
            camara.xRotation = 180;
        }
        else
        {
            Physics.gravity = new Vector3(0, -9.81f, 0);
            camara.xRotation = 0;
        }

    }

}
