using UnityEngine;
using UnityEngine.UI;
public class Frasco : agarrable
{
    [SerializeField] GameObject frascoluzPre;
    [SerializeField] Image frascoVacio;
    protected override void alAgarrar()
    {
        Instantiate(frascoluzPre, player.position, Quaternion.identity, player);
        frascoVacio.enabled = true;
    }
   
}
