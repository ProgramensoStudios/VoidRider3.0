using System;
using System.Linq;
using UnityEngine;

public class FallingRocks : MonoBehaviour
{
   public GameObject[] rocks;

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.layer != 6) return;
      for (var indexRocks = 0; indexRocks < rocks.Length; indexRocks++)
      {
         if (rocks[indexRocks] != null)
         {
            rocks[indexRocks].SetActive(true);
            var rocksRb = rocks[indexRocks].GetComponent<Rigidbody>();
            rocksRb.useGravity = true;
         }
         else
         {
            Debug.LogWarning($"El Rigidbody en la posición {indexRocks} es nulo.");
         }
      }
   }
}

