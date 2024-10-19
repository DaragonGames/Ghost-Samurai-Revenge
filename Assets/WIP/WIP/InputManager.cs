using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
     // For the Player Class to work with
    [NonSerialized]
    public Vector3 movement;
    [System.NonSerialized]
    public Vector3 aiming = Vector3.up; 

    public event Action<Vector3> primaryEvent;
    public event Action<Vector3> secondaryEvent;
    
    // Internal Input Actions for this class
    private InputAction move;
    private InputAction aim;
    private InputAction primary;
    private InputAction secondary;

    void Start()
    {
        Controls controls = new Controls();
        move = controls.Main.Move;
        aim = controls.Main.Aim;
        primary = controls.Main.Primary;
        secondary = controls.Main.Secondary;
        move.Enable();
        aim.Enable();
        primary.Enable();
        secondary.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 leftStick = move.ReadValue<Vector2>();
        Vector2 rightStick = aim.ReadValue<Vector2>();

        movement = leftStick;

        if (leftStick.magnitude < 0.15f)
        {
            movement = Vector3.zero;           
        }

        if (rightStick.magnitude > 0.2f)
        {
            aiming = rightStick.normalized;
        }
        else
        {
            if (leftStick.magnitude > 0.15f)
            {
                aiming = movement.normalized;           
            }
        }   

        HandleMouseMovement();     

        if (primary.triggered)
        {
            primaryEvent?.Invoke(aiming);
            return;
        }
        if (secondary.triggered)
        {
            secondaryEvent?.Invoke(aiming);
            return;
        }
    }

    void HandleMouseMovement()
    {
        
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        Vector3 input = new Vector3(x,y,0);
        if (input.magnitude > 0.05f)
        {
            aiming += input;
        }
        if (aiming.magnitude > 3)
        {
            aiming = aiming.normalized*2.5f;
        }        
    }

    void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
