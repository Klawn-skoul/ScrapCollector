using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Aspects;
using Unity.Physics.Extensions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public partial class InputsSystem : SystemBase
{
    private InputAction actionForward;
    private InputAction actionSteer;
    protected override void OnCreate()
    {
        Controls inputActions = new Controls();
        inputActions.Enable();

        actionForward = inputActions.Collector.Forward;
        actionForward.performed += OnForward;
        actionForward.canceled += OnForward;
        actionForward.Enable();

        actionSteer = inputActions.Collector.Steer;
        actionSteer.performed += OnSteer;
        actionSteer.canceled += OnSteer;
        actionSteer.Enable();
    }

    protected override void OnUpdate()
    {
        if (forward != 0 || steer != 0) {
            foreach (var (PhyVe, Coll) in SystemAPI.Query<RigidBodyAspect, RefRO<Collector>>())
            {
                var collector = Coll.ValueRO;

                // reverse the steer input when going back 
                var steerDir = steer;
                if (forward < 0)
                    steerDir = -steer;

                PhyVe.ApplyAngularImpulseLocalSpace(Vector3.up * steerDir * collector.forceSteer * SystemAPI.Time.DeltaTime);
                PhyVe.ApplyImpulseAtPointLocalSpace(Vector3.forward * forward * collector.forceMovement * SystemAPI.Time.DeltaTime, collector.Offset);
                //PhyVe.ApplyLinearImpulse(Mass.ValueRO, Vector3.forward * forward * Coll.ValueRO.forceMovement);
            }
        }
    }

    protected override void OnDestroy()
    {
        actionForward.Disable();
        actionForward.performed -= OnForward;
        actionForward.canceled -= OnForward;

        actionSteer.Disable();
        actionSteer.performed -= OnSteer;
        actionSteer.canceled -= OnSteer;


    }

    float forward;
    private void OnForward(InputAction.CallbackContext context)
    {
        forward = context.ReadValue<float>();
        
        //Debug.Log($"Entity: Axis forward: {forward}");
    }
    float steer;
    private void OnSteer(InputAction.CallbackContext context)
    {
        steer = context.ReadValue<float>();
        
        //Debug.Log($"Entity: Axis steer: {steer}");
    }

    private void stopForward() {
        foreach (var (PhyVe, Coll) in SystemAPI.Query<RigidBodyAspect, RefRO<Collector>>())
        {
            
        }
    }
}
