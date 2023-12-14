using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterEffectSystem : ComponentSystem
{
    private EntityQuery _effectQuery;

    protected override void OnCreate()
    {
        _effectQuery = GetEntityQuery(
            ComponentType.ReadOnly<InputData>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_effectQuery).ForEach(
            (Entity entity, UserInputData userInputData, ref InputData inputData) =>
            {
                if (inputData.Flame > 0f && userInputData.FlameAction != null && userInputData.FlameAction is IAbility ability)
                {
                    ability.Execute();
                }
            });
    }
}