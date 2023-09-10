using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBulletItem : MonoBehaviour, IAbilityTarget
{
    public event EventHandler Destroyed;
    public float Time;
    public List<GameObject> Targets { get; set; }

    public void Execute()
    {
        foreach (var target in Targets)
        {
            if (target.TryGetComponent(out ShootAbility shootAbility))
            {
                shootAbility.TakeReflectBulletItem(Time);
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