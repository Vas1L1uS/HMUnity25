using UnityEngine;

[CreateAssetMenu]
public class PlayerSettingsConfig : ScriptableObject, IPlayerSettings
{
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public float JumpHeight { get => _jumpHeight; set => _jumpHeight = value; }
    public float Damage { get => _damage; set => _damage = value; }

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _damage;

    public PlayerSettings GetFormatPlayerSettings()
    {
        PlayerSettings playerSettings = new PlayerSettings()
        {
            MaxHealth = this.MaxHealth,
            Speed = this.Speed,
            JumpHeight = this.JumpHeight,
            Damage = this.Damage,
        };

        return playerSettings;
    }
}

public interface IPlayerSettings
{
    public float MaxHealth { get; set; }
    public float Speed { get; set; }
    public float JumpHeight { get; set; }
    public float Damage { get; set; }
}

public class PlayerSettings : IPlayerSettings
{

    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public float JumpHeight { get => _jumpHeight; set => _jumpHeight = value; }
    public float Damage { get => _damage; set => _damage = value; }

    private float _maxHealth;
    private float _speed;
    private float _jumpHeight;
    private float _damage;
}