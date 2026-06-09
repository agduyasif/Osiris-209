using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoothook : MonoBehaviour
{

    LineRenderer line;

    [SerializeField] Transform player;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] float pullSpeed = 1f;
    Player playerScript;
    SpringJoint joint;
    Rigidbody grabbedRb;
    [SerializeField] AudioSource As;
    [SerializeField] AudioClip grappleSound;

    private void Start()
    {
        playerScript = GetComponentInParent<Player>();
        line = GetRequired<LineRenderer>(gameObject);
        
    }
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (SistemaMira.Instance.IsGrappable)
            {
                StartGrapple(SistemaMira.Instance.AimPoint);
            }
            else if (SistemaMira.Instance.AimRb != null)
            {
                grabbedRb = SistemaMira.Instance.AimRb;
            }
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Release();
        }
        if (joint != null)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, joint.connectedAnchor);
        }
        if (grabbedRb != null)
        {
            pullRb();
        }
    }

    void StartGrapple(Vector3 grapplePoint)
    {
        if (joint != null) return;

        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = grapplePoint;

        float distanceFrom = Vector3.Distance(player.position, grapplePoint);

        joint.maxDistance = distanceFrom * 0.8f;
        joint.minDistance = distanceFrom * 0.25f;

        joint.spring = 10;
        joint.damper = 10;
        joint.massScale = 3;

        line.positionCount = 2;
        line.enabled = true;

        playerScript.isGrappling = true;
        playerScript.grappleMove.setAnchor(grapplePoint);

        As.PlayOneShot(grappleSound);
    }

    void pullRb()
    {
        if (grabbedRb != null)
        {
            float realPullSpeed = pullSpeed / Mathf.Max(0.1f, grabbedRb.mass);
            Vector3 puntoDeAgarre = transform.position + transform.forward * 1f;

            float distance = Vector3.Distance(grabbedRb.transform.position, puntoDeAgarre);
            InHook.inHook(grabbedRb);

            if (distance > 3f)
            {
                grabbedRb.transform.position = Vector3.MoveTowards(grabbedRb.transform.position, puntoDeAgarre, realPullSpeed * Time.deltaTime);
            }
            else
            {
                grabbedRb.transform.position = puntoDeAgarre;
            }
        }
    }


    public void Release()
    {
        Destroy(joint);
        joint = null;
        line.enabled = false;
        playerScript.isGrappling = false;
        grabbedRb = null;
    }

    T GetRequired<T>(GameObject obj) where T : Component
    {
        T comp = obj.GetComponent<T>();
        if (comp == null) Debug.LogError($"Falta {typeof(T).Name} en {obj.name}");
        return comp;
    }
}
