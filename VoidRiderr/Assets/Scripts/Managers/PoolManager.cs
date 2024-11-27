using System.Collections;
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

    public void AskForObject(Transform posToSpawn)
    {
        for (int i = 0; i < createdObjects.Count; i++)
        {
            if (!createdObjects[i].activeInHierarchy)
            {
                // Primero, configura el parent y la posición
                createdObjects[i].transform.SetParent(posToSpawn);
                createdObjects[i].transform.position = posToSpawn.position;
                createdObjects[i].transform.rotation = posToSpawn.rotation;

                // Luego activa el objeto
                createdObjects[i].SetActive(true);
                //return createdObjects[i];
            }
        }
    
        if (createdObjects.Count < maxPoolSize)
        {
            // Primero, crea el objeto y configura su parent y posición
            GameObject createdObject = Instantiate(prefabToCreate, posToSpawn.position, posToSpawn.rotation);
            createdObject.transform.SetParent(posToSpawn);
            StartCoroutine(Wait());
            createdObjects.Add(createdObject);
            createdObject.SetActive(true);
           // return createdObject;
        }

       // return null;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = Vector3.zero;
    }
}