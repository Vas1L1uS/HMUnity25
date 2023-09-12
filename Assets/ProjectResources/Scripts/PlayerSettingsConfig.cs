using UnityEngine;

[CreateAssetMenu]
public class PlayerSettingsConfig : ScriptableObject
{
    public float MaxHealth;
    public float Speed;
    public float JumpHeight;
    public float Damage;
}

public class PlayerSettings
{
    public float MaxHealth;
    public float Speed;
    public float JumpHeight;
    public float Damage;
}