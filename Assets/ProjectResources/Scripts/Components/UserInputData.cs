using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{
    public float Speed;
    public float DashRunFactor;
    public float DashRunDuration;
    public float DashRunTimeReload;
    public MonoBehaviour ShootAction;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new InputData());
        dstManager.AddComponentData(entity, new MoveData
        {
            Speed = this.Speed,
            DashRunFactor = this.DashRunFactor,
            DashRunDuration = this.DashRunDuration,
            DashRunTimeReload = this.DashRunTimeReload
        });

        if (ShootAction != null && ShootAction is IAbility)
        {
            dstManager.AddComponentData(entity, new ShootData());
        }
    }
}

public struct InputData : IComponentData
{
    public float2 Move;
    public float Shoot;
    public float DashRun;
}

public struct MoveData : IComponentData
{
    public float Speed;
    public float DashRunFactor;
    public float DashRunDuration;
    public float DashRunTimeReload;
}

public struct ShootData : IComponentData
{

}
