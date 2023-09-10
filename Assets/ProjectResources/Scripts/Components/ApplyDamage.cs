using System;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamage : MonoBehaviour, IAbilityTarget
{
    public event EventHandler Destroyed;

    public int Damage = 10;
    public List<GameObject> Targets { get; set; }

    public void Execute()
    {
        foreach (var target in Targets)
        {
            if (target.TryGetComponent(out IHealthAbility health)) health.GetDamage(Damage);
        }
    }
}