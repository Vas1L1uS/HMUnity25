using Unity.Entities;
using UnityEngine;

public class HumanMoveSystem : ComponentSystem
{
    private EntityQuery _query;

    protected override void OnCreate()
    {
        _query = GetEntityQuery(ComponentType.ReadOnly<HumanMoveComponent>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_query).ForEach((Entity entity, Transform transform, HumanMoveComponent humanMove) =>
        {
            var p = transform.position;
            p.y += humanMove._speed * Time.DeltaTime;
            transform.position = p;
        });
    }
}
