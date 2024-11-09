using UnityEngine;

public class Turret : Enemy
{
    [SerializeField] private LookAtYOnly yRot;
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
    
}
