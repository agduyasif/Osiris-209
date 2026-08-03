using UnityEngine;

public class luciernaga : agarrable
{

    protected override void alAgarrar()
    {
        FrascoLuz luz = player.GetComponentInChildren<FrascoLuz>();


        if (luz != null) 
        {
            luz.sumar();
        }
    }
}
