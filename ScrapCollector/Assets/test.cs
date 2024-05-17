using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class test : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActions;
    private InputAction steerAction;
    private InputAction forwardAction;

    private void OnEnable()
    {
        var gameplayMap = inputActions.FindActionMap("Collector");

        // Para el eje X del joystick
        steerAction = gameplayMap.FindAction("Steer");
        forwardAction = gameplayMap.FindAction("Forward");
        steerAction.performed += OnMoveSteer;
        forwardAction.performed += OnMoveForward;
        forwardAction.Enable();
        steerAction.Enable();
    }

    private void Jump(InputAction.CallbackContext context) {
        bool pressed = context.performed;
        Debug.Log($"Jumped: {pressed}");
    }

    private void OnDisable()
    {
        steerAction.Disable();
        forwardAction.Disable();
        steerAction.performed -= OnMoveSteer;
        forwardAction.performed -= OnMoveForward;
    }

    private void OnMoveSteer(InputAction.CallbackContext context)
    {
        float steer = context.ReadValue<float>();
        // El valor de moveX estará en el rango de -1 a 1.s
        Debug.Log($"Axis Steer: {steer}");
    }

    private void OnMoveForward(InputAction.CallbackContext context)
    {
        float forward = context.ReadValue<float>();
        // El valor de moveX estará en el rango de -1 a 1.
        Debug.Log($"Axis forward: {forward}");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
