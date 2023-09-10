using System.Collections.Generic;
using UnityEngine;

public interface ICollisionAbility : IAbility
{
    List<Collider> Collisions { get; set; }
}