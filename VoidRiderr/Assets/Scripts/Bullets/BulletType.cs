using UnityEngine;

public class BulletType : MonoBehaviour
{
    public enum BulletOwner
    {
        Player,
        Enemy
    }

    public BulletOwner owner;
}