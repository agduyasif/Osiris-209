using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform player;
    float timer;
    [SerializeField] GameObject granade;
    [SerializeField] Transform spawnGranade;
    [SerializeField] float range = 10;
    private void Update()
    {
        timer += Time.deltaTime;
        transform.LookAt(player.position);
        transform.localEulerAngles = new Vector3 (0, transform.localEulerAngles.y, 0);


        if (Vector3.Distance(transform.position, player.position) < range)
        {
            if (timer > 8) 
            {
                Instantiate(granade, spawnGranade.transform.position, spawnGranade.rotation);
                timer = 0;
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
