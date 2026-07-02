using UnityEngine;

public class Water : MonoBehaviour
{
    Player playerScript;
    System.Action previousMove;
    float previousDrag;
    [SerializeField] GameObject image;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) image.SetActive(true);
        if (other.gameObject.layer !=6) return;

        playerScript = other.GetComponent<Player>();
        previousMove = playerScript.Move;
        playerScript.Move = playerScript.ManejarLogicaSwim;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        previousDrag = rb.linearDamping;
        rb.linearDamping = 5f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8) image.SetActive(false);
        if (other.gameObject.layer != 6) return;
       

        playerScript.Move = previousMove;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.linearDamping = previousDrag;
        rb.AddForce(Vector3.forward * 10, ForceMode.Impulse);
    }
}
