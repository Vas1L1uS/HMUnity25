using System;
using System.Collections;
using UnityEngine;
using UnityGoogleDrive.Data;

public class CharacterHealthAbility : MonoBehaviour, IHealthAbility
{
    public event EventHandler Destroyed;
    public int Health { get => _health; }

    public Settings Settings;

    [SerializeField] private int _health = 100;
    [SerializeField] private float _invulnerableTime = 0.5f;
    [SerializeField] private ShootAbility _shootAbility;

    private void Awake()
    {
        _health = Settings.HeroHealth;
    }

    private bool _isInvulnerable;

    public void Execute()
    {
        Debug.LogWarning("No implementation!");
    }

    public void GetDamage(int damage)
    {
        if (_isInvulnerable) return;
        if (_health <= 0) return;

        _health -= damage;
        StartCoroutine(TimerDontGetDamage(_invulnerableTime));

        if (_health <= 0)
        {
            _health = 0;
        }
    }

    public void GetHealth(int health)
    {
        _health += health;
    }

    private IEnumerator TimerDontGetDamage(float time)
    {
        _isInvulnerable = true;
        yield return new WaitForSeconds(time);
        _isInvulnerable = false;
    }
}