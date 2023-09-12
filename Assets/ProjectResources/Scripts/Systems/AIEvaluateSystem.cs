using Unity.Entities;
using UnityEngine;

public class AIEvaluateSystem : ComponentSystem
{
    private EntityQuery _evaluateQuery;

    protected override void OnCreate()
    {
        _evaluateQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_evaluateQuery).ForEach(
            (Entity entity, BehaviourManager manager) =>
            {
                float highScore = float.MinValue;

                if (manager.ActiveBehaviour != null) highScore = manager.ActiveBehaviour.Evaluate();

                foreach (var behaviour in manager.Behaviours)
                {
                    if (manager.ActiveBehaviour != null && behaviour.Equals(manager.ActiveBehaviour)) continue;

                    if (behaviour is IBehaviour ai)
                    {
                        var currentScore = ai.Evaluate();

                        if (currentScore > highScore)
                        {
                            highScore = currentScore;
                            manager.ActiveBehaviour?.Stop();
                            manager.ActiveBehaviour = ai;
                        }
                    }
                }
            });
    }
}
