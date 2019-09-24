using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<Pool> pools;

    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);

        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot , int awayFromY_Axis)
    {
        if (!poolDictionary.ContainsKey(tag)) { Debug.LogWarning("Pool with tag " + tag + " doesn't exist .."); return null; }

        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = pos - new Vector3(0,awayFromY_Axis,0);
        objToSpawn.transform.rotation = rot;

        IPooledObject iPooledObj = objToSpawn.GetComponent<IPooledObject>();
        
        if (iPooledObj != null) iPooledObj.OnObjectSpawn();
        poolDictionary[tag].Enqueue(objToSpawn);
        return objToSpawn;
    }


}
