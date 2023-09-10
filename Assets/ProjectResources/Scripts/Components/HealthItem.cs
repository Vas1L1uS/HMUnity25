using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour, IAbilityTarget
{
    public event EventHandler Destroyed;
    public int Health => _health;
    public List<GameObject> Targets { get; set; }

    [SerializeField] private int _health = 10;

    public void Execute()
    {
        foreach (var target in Targets)
        {
            if (target.TryGetComponent(out IHealthAbility health))
            {
                health.GetHealth(Health);
                StartCoroutine(TimerToDestroy());
                return;
            }
        }
    }

    private IEnumerator TimerToDestroy()
    {
        yield return new WaitForEndOfFrame();
        Destroyed?.Invoke(this, EventArgs.Empty);
        Destroy(this.gameObject);
    }
}