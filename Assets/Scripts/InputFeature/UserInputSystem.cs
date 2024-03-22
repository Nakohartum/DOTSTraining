using ShiftFeature;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ECSTutotial
{
    public partial class UserInputSystem : SystemBase
    {
        private EntityQuery _movementQuery;
        private InputAction _moveAction;
        private float2 _moveInput;
        
        private InputAction _shootAction;
        private float _shootInput;

        private InputAction _shiftAction;
        private float _shiftInput;
        
        protected override void OnCreate()
        {
            _movementQuery = GetEntityQuery(ComponentType.ReadWrite<UserInputData>(), ComponentType.ReadWrite<ShiftComponent>());
        }
        
        protected override void OnStartRunning()
        {
            _moveAction = new InputAction("move", binding: "<Gamepad>/rightStick");
            _moveAction.AddCompositeBinding("Dpad")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");
        
            _moveAction.performed += context => { _moveInput = context.ReadValue<Vector2>(); };
            _moveAction.started += context => { _moveInput = context.ReadValue<Vector2>(); };
            _moveAction.canceled += context => { _moveInput = context.ReadValue<Vector2>(); };
            _moveAction.Enable();

            _shootAction = new InputAction("shoot", binding: "<Keyboard>/Space");
            _shootAction.performed += context => { _shootInput = context.ReadValue<float>(); };
            _shootAction.started += context => { _shootInput = context.ReadValue<float>(); };
            _shootAction.canceled += context => { _shootInput = context.ReadValue<float>(); };
            
            _shootAction.Enable();
            
            _shiftAction = new InputAction("shift", binding: "<Keyboard>/LeftShift");
            _shiftAction.performed += context => { _shiftInput = context.ReadValue<float>(); };
            _shiftAction.started += context => { _shiftInput = context.ReadValue<float>(); };
            _shiftAction.canceled += context => { _shiftInput = context.ReadValue<float>(); };
            _shiftAction.Enable();
        }
        
        protected override void OnStopRunning()
        {
            _moveAction.Disable();
            _shootAction.Disable();
            _shiftAction.Disable();
        }
        
        protected override void OnUpdate()
        {
            var moveInput = math.length(_moveInput) > 0 ? _moveInput : 0;
            var shootInput = _shootInput > 0 ? _shootInput : 0;
            var shiftInput = _shiftInput > 0 ? _shiftInput : 0;
            Entities.WithStoreEntityQueryInField(ref _movementQuery).ForEach((ref UserInputData data) =>
            {
                data.Move = moveInput;
                data.Shoot = shootInput;
                data.Shift = shiftInput;
            }).Run(); 
        }
    }
}