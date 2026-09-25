using UnityEngine;

public class BombFactory : MonoBehaviour
{
    [SerializeField] BombPool pool;

    public GameObject create(Vector3 posicion, Quaternion rotation)
    {
        GameObject bomb = pool.GetBomb();
        bomb.transform.position = posicion;
        bomb.transform.rotation = rotation;
        return bomb;
    }
}
