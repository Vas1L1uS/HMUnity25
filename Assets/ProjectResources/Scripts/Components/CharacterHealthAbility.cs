using System;
using System.Collections;
using UnityEngine;

public class CharacterHealthAbility : MonoBehaviour, IHealthAbility
{
    public event EventHandler Destroyed;

    public int Health { get => _health; }

    [SerializeField] private int _health = 100;
    [SerializeField] private float _invulnerableTime = 0.5f;

    private bool _isInvulnerable;

    public void Execute()
    {
        Debug.LogWarning("No implementation!");
    }

    public void GetDamage(int damage)
    {
        if (_isInvulnerable) return;

        _health -= damage;
        StartCoroutine(TimerDontGetDamage(_invulnerableTime));

        if (_health < 0) _health = 0;
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