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
        // public bool shouldExpand = true; // В данный момент не могу это сделать, так как нет взаимодействия словаря с Pool
    }

    public static ObjectPooler SharedInstance;


    public List<Pool> pools;
    public Dictionary<GameObject, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        SharedInstance = this;
    }


    void Start()
    {

        poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

        for (int i = 0; i < pools.Count; i++)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int j = 0; j < pools[i].size; j++)
            {
                GameObject newGameObject = CreateNewObjectToPool(pools[i].prefab);
                objectPool.Enqueue(newGameObject);
            }

            poolDictionary.Add(pools[i].prefab, objectPool);
        }
    }


    public GameObject SpawnFromPool(GameObject prefabKey, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(prefabKey))
        {
            Debug.LogError("Pool with prefabKey " + prefabKey + " does not exist");
            return null;
        }


        // Посмотреть на первый обьект в очереди.
        GameObject objectToSpawn = poolDictionary[prefabKey].Peek();

        if (objectToSpawn.activeInHierarchy) 
        {
            // Если включен, то сделать новый.
            objectToSpawn = CreateNewObjectToPool(prefabKey);
        }
        else
        {
            // Если он выключен, то можно использовать. 
            objectToSpawn = poolDictionary[prefabKey].Dequeue();
        }

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        if (objectToSpawn.TryGetComponent(out IPooledObject pooledObject))
        {
            pooledObject.OnObjectSpawn();
        }

        poolDictionary[prefabKey].Enqueue(objectToSpawn);


        return objectToSpawn;
    }


    private GameObject CreateNewObjectToPool(GameObject newGameObject)
    {
        newGameObject = Instantiate(newGameObject);
        newGameObject.transform.SetParent(gameObject.transform);
        newGameObject.SetActive(false);

        return newGameObject;
    }
}
