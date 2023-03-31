using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public struct Pool
    {
        public string Id;
        public GameObject Prefab;
    }

    public List<Pool> Pools;                                                                            // List of Pool Groups
    public Dictionary<string, List<GameObject>> PoolDictionary { get; } = new();                        // Dictionary of Pool Groups

    #region Singleton
    private void Awake()
    {
        SingletonManager.Register(this);
    }
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize Pools
        foreach (Pool pool in Pools)
        {
            List<GameObject> objectPool = new();
            PoolDictionary.Add(pool.Id, objectPool);
        }
    }

    /// <summary>
    /// Spawns an Object from the Pool
    /// </summary>
    /// <param name="id"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject SpawnFromPool(string id, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (!PoolDictionary.ContainsKey(id))
        {
            Debug.LogWarning("Pool with ID " + id + " doesn't exist.");
            return null;
        }

        // Recycle Object
        foreach (GameObject go in PoolDictionary[id])
        {
            if (!go.activeInHierarchy)
            {
                GameObject recycledObject = go;
                recycledObject.SetActive(true);

                // Remove from Pool, then Set Position and Rotation
                recycledObject.transform.SetParent(null);
                recycledObject.transform.position = position;
                recycledObject.transform.rotation = rotation;

                return recycledObject;
            }
        }

        // Spawn New Object
        foreach (Pool objPool in Pools)
        {
            if (objPool.Id == id)
            {
                // Spawn object and add poolable component
                GameObject newObject = Instantiate(objPool.Prefab, position, rotation, parent);

                // Set Position and Rotation
                //newObject.transform.position = position;
                //newObject.transform.rotation = rotation;

                //newObject.transform.parent = parent;
                newObject.transform.parent = null;

                // Add Gameobject to List
                PoolDictionary[id].Add(newObject);

                newObject.transform.parent = null;

                // Initialize Name
                newObject.transform.name = newObject.transform.name + PoolDictionary[id].Count.ToString();

                return newObject;
            }
        }

        return null;
    }

    /// <summary>
    /// Returns the Poolable GameObject Back to the Pool
    /// </summary>
    /// <param name="go"></param>
    public void ReturnToPool(GameObject go)
    {
        go.transform.SetParent(transform);
        go.SetActive(false);
    }
}
