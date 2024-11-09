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
    }

    public GameObject AskForObject(Transform posToSpawn)
    {
        // Primero intenta buscar un objeto inactivo en el pool.
        for (int i = 0; i < createdObjects.Count; i++)
        {
            if (!createdObjects[i].activeInHierarchy)
            {
                createdObjects[i].transform.position = posToSpawn.position;
                createdObjects[i].SetActive(true);
                createdObjects[i].transform.parent = posToSpawn;
                return createdObjects[i];
            }
        }
        
        if (createdObjects.Count < maxPoolSize)
        {
            GameObject createdObject = Instantiate(prefabToCreate, posToSpawn.position, Quaternion.identity);
            createdObjects.Add(createdObject);
            return createdObject;
        }

        // Si el pool está lleno, retorna null o realiza una acción específica.
        Debug.LogWarning("Pool máximo alcanzado. No se pueden crear más objetos.");
        return null;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = Vector3.zero; // Opcional: reiniciar la posición
    }
}