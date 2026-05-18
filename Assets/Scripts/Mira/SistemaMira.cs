using UnityEngine;
using UnityEngine.UI;
public class SistemaMira : MonoBehaviour
{
    [SerializeField] Camera Mcamera;
    [SerializeField] Image nothing;
    [SerializeField] Image CanGrab;
    [SerializeField] Image CanGrapple;

    /*public SistemaMira(Camera camera, Image _nothing, Image _CanGrab, Image _CanGrapple)
    {
        Mcamera = camera;
        nothing = _nothing;
        CanGrab = _CanGrab;
        CanGrapple = _CanGrapple;
    }*/
    public static SistemaMira Instance { get; private set; }
    public Vector3 AimPoint {  get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        Ray rayo = Mcamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));


        if (Physics.Raycast(rayo, out RaycastHit hit, 100))
        {
            AimPoint = hit.point;

            if (hit.collider.gameObject.layer == 7)
            {
                CanGrab.enabled = false;
                nothing.enabled = false;
                CanGrapple.enabled = true;
            }
            else if (hit.rigidbody != null)
            {
                CanGrab.enabled = true;
                nothing.enabled = false;
                CanGrapple.enabled = false;
            }
            else
            {
                CanGrab.enabled = false;
                nothing.enabled = true;
                CanGrapple.enabled = false;
            }

        }
        else
        {
            AimPoint = rayo.GetPoint(100);

            CanGrab.enabled = false;
            nothing.enabled = true;
            CanGrapple.enabled = false;
        }
    }
}
