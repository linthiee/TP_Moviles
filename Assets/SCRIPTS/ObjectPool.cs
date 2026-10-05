using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    
    public GameObject obstaclePrefab;
    public int initialQuant = 15;
    
    private List<GameObject> pool = new List<GameObject>();

    void Awake()
    {
        instance = this;
        
        for (int i = 0; i < initialQuant; i++)
        {
            GameObject obj = Instantiate(obstaclePrefab, transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject AskForObstacle()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        GameObject newObj = Instantiate(obstaclePrefab, transform);
        pool.Add(newObj);
        return newObj;
    }
}
