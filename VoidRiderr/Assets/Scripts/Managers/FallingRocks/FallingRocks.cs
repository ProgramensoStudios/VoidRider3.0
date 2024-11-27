using System;
using UnityEngine;

public class FallingRocks : MonoBehaviour
{
   public Rigidbody[] rocksRb;

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.layer != 6) return;
      for (var indexRocks = 0; indexRocks < rocksRb.Length; indexRocks++)
      {
         if (rocksRb[indexRocks] != null)
         {
            rocksRb[indexRocks].useGravity = true;
         }
         else
         {
            Debug.LogWarning($"El Rigidbody en la posición {indexRocks} es nulo.");
         }
      }
   }
}

