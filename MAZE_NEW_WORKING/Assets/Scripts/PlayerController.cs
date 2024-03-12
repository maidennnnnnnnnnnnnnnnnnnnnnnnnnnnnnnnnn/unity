using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private int count = 0;
    private float movementX;
    private float movementY;
    private float lookX;
    private float lookY;
    public float speed = 4;
    public Camera mainCamera;
    public float lookSpeed = 0.2f;
    // public InputAction input;
    // private InputAction move;
    // private InputAction look;
    private Rigidbody rb;
    public InputActionAsset inputActionsAsset;
    private InputActionMap playerActionMap;
    private InputAction move;
    private InputAction look;

    private void Awake()
    {
        // Assuming you've assigned the InputActionAsset in the Inspector.
        playerActionMap = inputActionsAsset.FindActionMap("Player");

        move = playerActionMap.actions[0];
        look = playerActionMap.actions[1];
    }

    private void OnEnable()
    {
        move.Enable();
        look.Enable();
        move.performed += OnMove;
        move.canceled += OnMove;

        // Get the 'Look' action from the 'Player' action map
        // look = playerActionMap["Look"];
        look.performed += OnLook;
        look.canceled += OnLook;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        move.Disable();
        look.Disable();
        move.performed -= OnMove;
        move.canceled -= OnMove; // Unsubscribe from the 'canceled' event
        look.performed -= OnLook;
        look.canceled -= OnLook;

        // Reset cursor state
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    // private void OnEnable()
    // {
    //     input.Enable(); // Enable the entire input system

    //     // Get the 'Player' action map from the input actions
    //     InputActionMap playerActionMap = input.actionMap;

    //     // Get the 'Move' action from the 'Player' action map
    //     move = playerActionMap["Move"];
    //     move.performed += OnMove;
    //     move.canceled += OnMove;

    //     // Get the 'Look' action from the 'Player' action map
    //     look = playerActionMap["Look"];
    //     look.performed += OnLook;
    //     look.canceled += OnLook;

    //     Cursor.lockState = CursorLockMode.Locked;
    //     Cursor.visible = false;
    // }


    // private void OnDisable()
    // {
    //     move.Disable();
    //     move.performed -= OnMove;
    //     move.canceled -= OnMove; // Unsubscribe from the 'canceled' event
    //     look.Disable();
    //     look.performed -= OnLook;
    //     look.canceled -= OnLook;

    //     // Reset cursor state
    //     Cursor.lockState = CursorLockMode.None;
    //     Cursor.visible = true;
    // }


    void Start()
    {

        rb = GetComponent<Rigidbody>();
        count = 0;


    }
    void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // Button was pressed, read the movement value from the context.
            Vector2 movementVector = context.ReadValue<Vector2>();

            movementX = movementVector.x;
            movementY = movementVector.y;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            // Button was released, reset the movement values.
            movementX = 0;
            movementY = 0;
        }
    }


    void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookVector = context.ReadValue<Vector2>();

        lookX = lookVector.x;
        lookY = lookVector.y;

        float rotationX = mainCamera.transform.localEulerAngles.y + lookX * lookSpeed;
        float rotationY = mainCamera.transform.localEulerAngles.x - lookY * lookSpeed;
        mainCamera.transform.localEulerAngles = new Vector3(rotationY, rotationX, 0);
    }

}
