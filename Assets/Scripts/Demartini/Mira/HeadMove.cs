using Unity.Mathematics;
using UnityEngine;

public class HeadMove : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Transform cam;
    [SerializeField] float frecuencia = 8f;
    [SerializeField] float amplitud = 0.05f;

    float timer = 0f;
    float posicionInicialY;

    void Start()
    {
        posicionInicialY = cam.localPosition.y;
    }
    void Update()
    {
        if (player.IsGrounded() && player.IsMoving()) 
        {
            timer += Time.deltaTime * frecuencia;

            float offsetY = Mathf.Sin(timer) * amplitud;
            cam.localPosition = new Vector3(cam.localPosition.x, posicionInicialY + offsetY, cam.localPosition.z);
        }
        else
        {
            timer = 0f;
            cam.localPosition = new Vector3(cam.localPosition.x, posicionInicialY, cam.localPosition.z);
        }
    }

}
