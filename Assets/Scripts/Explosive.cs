using UnityEngine;
using UnityEngine.SceneManagement;
public class Explosive : MonoBehaviour
{
    float time;
    [SerializeField] MeshRenderer expl;
    Rigidbody rb;
    float force = 5;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time > 3)
        {
            Collider[] objs = Physics.OverlapSphere(transform.position, 2);
            
            expl.enabled = true;

            foreach (var item in objs)
            {
                if (item.TryGetComponent<Player>(out var player))
                {
                    string currentScene = SceneManager.GetActiveScene().name;
                    SceneManager.LoadScene(currentScene);
                }
            }
            if (time > 4) { Destroy(gameObject); }
        }
        
    }
    
    
    
    
    
    private void OnDrawGizmosSelected()
    {       
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2);
    }
}
