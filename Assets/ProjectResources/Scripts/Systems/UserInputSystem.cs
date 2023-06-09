using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInputSystem : ComponentSystem
{
    private EntityQuery _inputQuery;

    private InputAction _moveAction;
    private InputAction _shootAction;
    private InputAction _dashRunAction;

    private float2 _moveInput;
    private float _shootInput;
    private float _dashRunInput;

    protected override void OnCreate()
    {
        _inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>());
    }

    protected override void OnStartRunning()
    {
        _moveAction = new InputAction("move", binding: "<Gamepad>/leftStick");
        _moveAction.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        _moveAction.performed += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.started += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.canceled += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.Enable();

        _shootAction = new InputAction("shoot", binding: "<Keyboard>/x");
        _shootAction.performed += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.started += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.canceled += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.Enable();

        _dashRunAction = new InputAction("shoot", binding: "<Keyboard>/shift");
        _dashRunAction.performed += context => { _dashRunInput = context.ReadValue<float>(); };
        _dashRunAction.started += context => { _dashRunInput = context.ReadValue<float>(); };
        _dashRunAction.canceled += context => { _dashRunInput = context.ReadValue<float>(); };
        _dashRunAction.Enable();
    }

    protected override void OnStopRunning()
    {
        _moveAction.Disable();
        _shootAction.Disable();
        _dashRunAction.Disable();
    }

    protected override void OnUpdate()
    {
        Entities.With(_inputQuery).ForEach(
            (Entity entity, ref InputData inputData) =>
            {
                inputData.Move = _moveInput;
                inputData.Shoot = _shootInput;
                inputData.DashRun = _dashRunInput;
            });
    }
}
