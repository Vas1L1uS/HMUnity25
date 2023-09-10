using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CollisionAbility : MonoBehaviour, IConvertGameObjectToEntity
{
    public List<Collider> Collisions { get; set; } = new List<Collider>();

    public Collider MyCollider;
    public List<MonoBehaviour> CollisionActions = new List<MonoBehaviour>();
    public List<IAbilityTarget> CollisionActionsAbilities = new List<IAbilityTarget>();

    private void Start()
    {
        foreach (var action in CollisionActions)
        {
            if (action is IAbilityTarget ability)
            {
                CollisionActionsAbilities.Add(ability);
                ability.Destroyed += RemoveDestroyedAbility;
            }
            else
            {
                Debug.LogError("Collision action must derive from IAbility!");
            }
        }
    }

    public void Execute()
    {
        foreach (var action in CollisionActionsAbilities)
        {
            action.Targets = new List<GameObject>();
            Collisions.ForEach(collider =>
            {
                if (collider != null) action.Targets.Add(collider.gameObject);
            });
            action.Execute();
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        float3 position = gameObject.transform.position;

        switch (MyCollider)
        {
            case SphereCollider sphere:
                sphere.ToWorldSpaceSphere(out var sphereCenter, out var sphereRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Sphere,
                    SphereCenter = sphereCenter - position,
                    SphereRadius = sphereRadius,
                    initialTakeOff = true
                });
                break;
            case CapsuleCollider capsule:
                capsule.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd, out var capsuleRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Capsule,
                    CapsuleStart = capsuleStart - position,
                    CapsuleEnd = capsuleEnd - position,
                    CapsuleRadius = capsuleRadius,
                    initialTakeOff = true
                });
                break;
            case BoxCollider box:
                box.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtents, out var boxOrientation);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Box,
                    BoxCenter = boxCenter - position,
                    BoxHalfExtents = boxHalfExtents,
                    BoxOrientation = boxOrientation,
                    initialTakeOff = true
                });
                break;
        }

        MyCollider.enabled = false;
    }

    private void RemoveDestroyedAbility(object sender, EventArgs e)
    {
        if (sender is IAbilityTarget ability) CollisionActionsAbilities.Remove(ability);
        if (sender is MonoBehaviour monoBehaviour) CollisionActions.Remove(monoBehaviour);
    }
}

public struct ActorColliderData : IComponentData
{
    public ColliderType ColliderType;
    public float3 SphereCenter;
    public float SphereRadius;
    public float3 CapsuleStart;
    public float3 CapsuleEnd;
    public float CapsuleRadius;
    public float3 BoxCenter;
    public float3 BoxHalfExtents;
    public quaternion BoxOrientation;
    public bool initialTakeOff;
}

public enum ColliderType
{
    Sphere,
    Capsule,
    Box
}