using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerPool : MonoBehaviour
{
    public static LazerPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public Transform lazerPoolTransform;
    public int amountToPool;

    private void Awake()
    {
        SharedInstance = this; 
        
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool, lazerPoolTransform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0;i< amountToPool;i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
    
}
