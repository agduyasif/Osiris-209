using UnityEngine;
using UnityEngine.SceneManagement;
public class Explosive : MonoBehaviour
{
    float time;
    [SerializeField] GameObject efectoExpl;
    [SerializeField] MeshRenderer granademesh;
    Rigidbody rb;
    float force = 5;
    float expTime = 8;

    [Header("Sonido")]
    [SerializeField] private AudioClip explSound;
    private bool hasExpl = false;

    BombPool pool;

    public void SetPool(BombPool _pool)
    {
        pool = _pool;
    }
    private void OnEnable()
    {
        time = 0;
        hasExpl = false;
        granademesh.enabled = true;
        efectoExpl.SetActive(false);

        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
    }
 

    private void Update()
    {
        time += Time.deltaTime;

        if (time > expTime)
        {
            if (!hasExpl)
            {
                if (explSound != null)
                {
                    AudioSource.PlayClipAtPoint(explSound, transform.position);
                }
                hasExpl = true; 
            }

            Collider[] objs = Physics.OverlapSphere(transform.position, 2);

            efectoExpl.SetActive(true);
            granademesh.enabled = false;
            foreach (var item in objs)
            {
                if (item.TryGetComponent<Player>(out var player))
                {
                    ResetScene.Reset();
                }
                else if (item.TryGetComponent<Wall>(out var wall))
                {
                    wall.destroyWall();
                }
            }
            if (time > expTime + 1) { pool.ReturnBomb(gameObject); }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2);
    }
}
