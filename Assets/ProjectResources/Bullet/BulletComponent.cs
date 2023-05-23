using Unity.Entities;
using UnityEngine;

public class BulletComponent : MonoBehaviour, IConvertGameObjectToEntity
{
    public float Speed;
    [HideInInspector] public Vector3 Direction;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new BulletData
        {
            Speed = this.Speed,
            Direction = this.Direction
        });
    }
}

public struct BulletData : IComponentData
{
    public float Speed;
    public Vector3 Direction;
}
