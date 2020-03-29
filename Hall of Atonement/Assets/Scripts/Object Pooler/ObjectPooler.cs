using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int size;
        public bool shouldExpand = true; // В данный момент не могу это сделать, так как нет взаимодействия словаря с Pool
        [HideInInspector] public Transform PoolParent;
        [HideInInspector] public Queue<GameObject> objectPoolQueue;
    }

    public static ObjectPooler SharedInstance;


    public List<Pool> pools;

    public Dictionary<GameObject, Pool> poolDictionary = new Dictionary<GameObject, Pool>();

    private void Awake()
    {
        SharedInstance = this;
    }


    private void OnEnable()
    {

        for (int i = 0; i < pools.Count; i++)
        {
            GameObject parent = new GameObject(pools[i].prefab.name + " Pool");
            pools[i].PoolParent = Instantiate(parent).transform;
            pools[i].PoolParent.SetParent(gameObject.transform);


            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int j = 0; j < pools[i].size; j++)
            {
                GameObject newGameObject = CreateNewObjectToPool(pools[i].prefab, pools[i].PoolParent);
                objectPool.Enqueue(newGameObject);
            }

            pools[i].objectPoolQueue = objectPool;

            poolDictionary.Add(pools[i].prefab, pools[i]);
            Debug.Log("Pool with " + pools[i].prefab.name + "has been created!");
        }
    }


    public GameObject SpawnFromPool(GameObject prefabKey, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (!poolDictionary.ContainsKey(prefabKey))
        {
            Debug.LogError("Pool with prefabKey " + prefabKey.name + " does not exist");
            return null;
        }

        Pool pool = poolDictionary[prefabKey];



        // Посмотреть на первый обьект в очереди.
        GameObject objectToSpawn = pool.objectPoolQueue.Peek();

        if (objectToSpawn.activeInHierarchy)
        {
            // Если включен
            // И можно расширить пул
            if (pool.shouldExpand)
            {
                //То сделать новый объект
                objectToSpawn = CreateNewObjectToPool(prefabKey, pool.PoolParent);
            }

        }
        else
        {
            // Если он выключен, то можно использовать. 
            objectToSpawn = pool.objectPoolQueue.Dequeue();
        }

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        if (parent != null) { objectToSpawn.transform.SetParent(parent); }
        objectToSpawn.SetActive(true);

        if (objectToSpawn.TryGetComponent(out IPooledObject pooledObject))
        {
            pooledObject.OnObjectSpawn();
        }

        pool.objectPoolQueue.Enqueue(objectToSpawn);


        return objectToSpawn;
    }


    private GameObject CreateNewObjectToPool(GameObject newGameObject, Transform poolParent)
    {
        newGameObject = Instantiate(newGameObject);
        newGameObject.transform.SetParent(poolParent);
        newGameObject.SetActive(false);

        return newGameObject;
    }
}
