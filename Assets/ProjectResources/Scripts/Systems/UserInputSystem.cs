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
    private InputAction _flameAction;

    private float2 _moveInput;
    private float _shootInput;
    private float _dashRunInput;
    private float _flameInput;

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

        _dashRunAction = new InputAction("run", binding: "<Keyboard>/shift");
        _dashRunAction.performed += context => { _dashRunInput = context.ReadValue<float>(); };
        _dashRunAction.started += context => { _dashRunInput = context.ReadValue<float>(); };
        _dashRunAction.canceled += context => { _dashRunInput = context.ReadValue<float>(); };
        _dashRunAction.Enable();

        _flameAction = new InputAction("flame", binding: "<Keyboard>/f");
        _flameAction.started += context => { _flameInput = 1; };
        //_flameAction.performed += context => { _flameInput = 2; };
        _flameAction.canceled += context => { _flameInput = 0; };
        _flameAction.Enable();
    }

    protected override void OnStopRunning()
    {
        _moveAction.Disable();
        _shootAction.Disable();
        _dashRunAction.Disable();
        _flameAction.Disable();
    }

    protected override void OnUpdate()
    {
        Entities.With(_inputQuery).ForEach(
            (Entity entity, ref InputData inputData) =>
            {
                inputData.Move = _moveInput;
                inputData.Shoot = _shootInput;
                inputData.DashRun = _dashRunInput;

                if (_flameInput == 1 && inputData.Flame == 1)
                {
                    inputData.Flame = -1;
                }
                else if (_flameInput == 0)
                {
                    inputData.Flame = 0;
                }
                else if (inputData.Flame != -1)
                {
                    inputData.Flame = _flameInput;
                }

            });
    }
}
