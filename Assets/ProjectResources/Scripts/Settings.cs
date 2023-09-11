using UnityEngine;

[CreateAssetMenu]
public class Settings : ScriptableObject
{
    public int HeroHealth => _heroHealth;

    [SerializeField] private int _heroHealth;
}