using UnityEngine;

public class RecieveDamage : MonoBehaviour
{
    public int health;
    public GameObject objToDestroy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            var damageToReceive = other.GetComponent<FollowEnemyBullet>().damage;
            health -= damageToReceive;
            if (health >= 0)
            {
                objToDestroy.SetActive(false);
            }
        }
    }
}
