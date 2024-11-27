using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour
{
   [SerializeField] private int health;

   public delegate void ReceiveDamage(int health);
   public ReceiveDamage OnReceiveDamage;

    private void OnTriggerEnter(Collider other)
    {
        var currentDamage = other.GetComponent<HarmPlayer>();
        if (currentDamage != null)
        {
            var damage = currentDamage.damage;
            other.gameObject.SetActive(false);
            health -= damage;
            OnReceiveDamage?.Invoke(health);
        }
        if (health <= 0) SceneManager.LoadScene("MuertePorSnuSnu");
        
    }
}
