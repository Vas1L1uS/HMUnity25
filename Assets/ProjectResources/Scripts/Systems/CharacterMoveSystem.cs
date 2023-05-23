using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMoveSystem : ComponentSystem
{
    private EntityQuery _moveQuery;

    private float _currentDashRunTimeReload;
    private float _currentDashRunDuration;

    protected override void OnCreate()
    {
        _moveQuery = GetEntityQuery(
            ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<MoveData>(),
            ComponentType.ReadOnly<Transform>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_moveQuery).ForEach(
            (Entity entity, Transform transform, ref InputData inputData, ref MoveData move) =>
            {
                var pos = transform.position;

                if (inputData.DashRun > 0 && _currentDashRunTimeReload == 0)
                {
                    _currentDashRunDuration += Time.DeltaTime;
                    pos += new Vector3(inputData.Move.x, 0, inputData.Move.y) * move.Speed * move.DashRunFactor * Time.DeltaTime;
                    transform.position = pos;
                }
                else
                {
                    pos += new Vector3(inputData.Move.x, 0, inputData.Move.y) * move.Speed * Time.DeltaTime;
                    transform.position = pos;

                    _currentDashRunDuration = 0;
                }

                if (_currentDashRunDuration >= move.DashRunDuration)
                {
                    if (_currentDashRunTimeReload <= 0)
                    {
                        _currentDashRunTimeReload = move.DashRunTimeReload;
                    }
                }

                if (_currentDashRunTimeReload > 0)
                {
                    _currentDashRunTimeReload -= Time.DeltaTime;

                    if (_currentDashRunTimeReload <= 0)
                    {
                        _currentDashRunDuration = 0;
                        _currentDashRunTimeReload = 0;
                    }
                }

                if (inputData.Move.x != 0 || inputData.Move.y != 0)
                {
                     transform.rotation = Quaternion.Euler(transform.rotation.x, (float)GetRotationAngleY(inputData.Move), transform.rotation.z);
                }
            });
    }

    private double GetRotationAngleY(float2 directionVector)
    {
        if (directionVector.y == 0)
        {
            if (directionVector.x > 0)
            {
                return 90;
            }
            else if (directionVector.x < 0)
            {
                return -90;
            }
            else
            {
                return 0;
            }
        }
        else if (directionVector.x == 0)
        {
            if (directionVector.y > 0)
            {
                return 0;
            }
            else
            {
                return 180;
            }
        }

        double tang = directionVector.x / directionVector.y;

        double result = Math.Atan(tang) * 180 / Mathf.PI;

        if (directionVector.y < 0)
        {
            result -= 180;
        }

        return result;
    }
}
