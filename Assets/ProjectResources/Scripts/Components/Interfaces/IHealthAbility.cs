public interface IHealthAbility : IAbility
{
    int Health { get; }

    void GetDamage(int damage);
    void GetHealth(int health);
}