using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject prefabToCreate;
    public int maxPoolSize;
    public List<GameObject> createdObjects;

    private void Start()
    {
        createdObjects = new List<GameObject>(maxPoolSize);
        for (int i = 0; i < maxPoolSize; i++)
        {
            // Crea el objeto y almacena su parent original
            GameObject createdObject = Instantiate(prefabToCreate);
            createdObject.SetActive(false);
            createdObject.transform.SetParent(transform); // Asignar el PoolManager como parent inicial
            createdObjects.Add(createdObject);
        }
    }

    public void AskForObject(Transform posToSpawn)
    {
        for (int i = 0; i < createdObjects.Count; i++)
        {
            if (!createdObjects[i].activeInHierarchy)
            {
                GameObject obj = createdObjects[i];

                // Asigna temporalmente el parent deseado y activa la bala
                obj.transform.SetParent(posToSpawn);
                obj.transform.position = posToSpawn.position;
                obj.transform.rotation = posToSpawn.rotation;
                obj.SetActive(true);

                return;
            }
        }

        if (createdObjects.Count < maxPoolSize)
        {
            // Crea un nuevo objeto si el pool no está lleno
            GameObject createdObject = Instantiate(prefabToCreate, posToSpawn.position, posToSpawn.rotation);

            // Configura el parent original y lo agrega al pool
            createdObject.transform.SetParent(posToSpawn);
            createdObjects.Add(createdObject);
            createdObject.SetActive(true);
        }
    }

    public void ReturnObject(GameObject obj)
    {
        // Desactiva el objeto y lo devuelve a su parent original
        obj.SetActive(false);
        obj.transform.SetParent(transform); // Retorna al PoolManager como parent
        obj.transform.position = Vector3.zero;
    }
}

