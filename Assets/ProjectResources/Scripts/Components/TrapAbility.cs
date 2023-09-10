using System;

public class TrapAbility : CollisionAbility, ICollisionAbility
{
    public event EventHandler Destroyed;

    public int Damage = 10;

    public new void Execute()
    {
        foreach (var target in base.Collisions)
        {
            var health = target?.gameObject?.GetComponent<IHealthAbility>();
            health?.GetDamage(Damage);
        }
    }
}