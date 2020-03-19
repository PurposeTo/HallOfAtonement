using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;


    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    public bool shouldExpand = true;

    private void Awake()
    {
        SharedInstance = this;
    }


    private void Start()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            CreateNewObjectToPool(objectToPool);
        }
    }


    public GameObject GetPooledObject(Vector3 position, Quaternion rotation)
    {

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                GameObject returnedGameObject = pooledObjects[i];

                SetPooledObjectAtTheWorld(returnedGameObject, position, rotation);

                return returnedGameObject;
            }
        }

        if (shouldExpand)
        {
            GameObject returnedGameObject = CreateNewObjectToPool(objectToPool);

            SetPooledObjectAtTheWorld(returnedGameObject, position, rotation);

            return returnedGameObject;
            
        }
        else
        {
            return null;
        }
    }



    private void SetPooledObjectAtTheWorld(GameObject returnedGameObject, Vector3 position, Quaternion rotation)
    {
        returnedGameObject.transform.position = position;
        returnedGameObject.transform.rotation = rotation;
        returnedGameObject.SetActive(true);

        if (returnedGameObject.TryGetComponent(out IPooledObject pooledObject))
        {
            pooledObject.OnObjectSpawn();
        }
    }


    private GameObject CreateNewObjectToPool(GameObject newGameObject)
    {
        newGameObject = Instantiate(newGameObject);
        newGameObject.transform.SetParent(gameObject.transform);
        newGameObject.SetActive(false);
        pooledObjects.Add(newGameObject);

        return newGameObject;
    }
}
