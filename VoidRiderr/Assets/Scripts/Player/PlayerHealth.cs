using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private int _health = 20;

    private void OnTriggerEnter(Collider other)
    {
        int currentDamage = other.GetComponent<EnemyBullet>().damage;
        _health -= currentDamage;
        if (_health <= 0) SceneManager.LoadScene(0);
    }
}
