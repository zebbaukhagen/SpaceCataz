//////////////////////////////////////////////
//Assignment/Lab/Project: Final Project
//Name: Zebulun Baukhagen
//Section: 2023SP.SGD.213.2172
//Instructor: Brian Sowers
//Date: 05/01/2023
/////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand;
}

public class ObjectPool : MonoBehaviour
{
    /// <summary>
    /// ObjectPool holds references to pre-instantiated gameObjects in order to save CPU on runtime
    /// </summary>
 
    public static ObjectPool sharedInstance;
    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool;

    void Awake()
    {
        sharedInstance = this; // there can be only one
    }

    // Start is called before the first frame update
    void Start()
    {
        // make a new list of pooled objects
        // go through all of the items to pool, instantiate them, set them inactive and add them to the pool
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        // called from enemies and player
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // go through all of the pooled items with the tag and if there are any inactive, return one
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            // go through the items to pool
            if (item.objectToPool.CompareTag(tag))
            {
                // if the item is "should expand"
                if (item.shouldExpand)
                {
                    // make a new one, set inactive and add it to the pool then return it
                    GameObject obj = Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
}
