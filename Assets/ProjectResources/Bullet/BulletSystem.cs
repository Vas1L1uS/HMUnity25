using Unity.Entities;
using UnityEngine;

public class BulletSystem : ComponentSystem
{
    private EntityQuery _bulletQuery;

    protected override void OnCreate()
    {
        _bulletQuery = GetEntityQuery(
            ComponentType.ReadOnly<BulletData>(),
            ComponentType.ReadOnly<Transform>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_bulletQuery).ForEach(
            (Entity entity, Transform transform, ref BulletData bulletData) =>
            {
                var pos = transform.position;
                pos += bulletData.Direction * bulletData.Speed * Time.DeltaTime;
                transform.position = pos;
            });
    }
}
