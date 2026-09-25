using System.Collections.Generic;
using UnityEngine;

public class BombPool : MonoBehaviour
{
    [SerializeField] GameObject bombPrefab;
    [SerializeField] int cantidadInicial = 10;

    Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        Debug.Log("Pool iniciando");
        for (int i = 0; i < cantidadInicial; i++)
        {
            GameObject bomb = Instantiate(bombPrefab);
            Debug.Log("Bomba creada: " + bomb.name);
            bomb.GetComponent<Explosive>().SetPool(this);
            bomb.SetActive(false);
            pool.Enqueue(bomb);
        }
        Debug.Log("Total en pool: " + pool.Count);
    }

    public GameObject GetBomb()
    {
        if (pool.Count > 0)
        {
            GameObject bomb = pool.Dequeue();
            bomb.SetActive (true);
            return bomb;
        }
        else
        {
            GameObject bomb = Instantiate(bombPrefab);
            bomb.GetComponent<Explosive>().SetPool(this);
            return bomb;
        }
    }

    public void ReturnBomb(GameObject bomb)
    {
        bomb.SetActive(false);
        pool.Enqueue(bomb);
    }
}
