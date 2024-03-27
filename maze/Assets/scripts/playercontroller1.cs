using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playercontroller1 : MonoBehaviour
{
    public float speed = 20;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject win;
    public Camera mainCamera;
    public maze maze;
    private InputAction move;
    private InputAction look;
    private float lookX;
    private float lookY;

    public float lookSpeed = 0.15f;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    private void Awake()
    {
        input = new maze();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        move = input.Player.Move;
        move.Enable();
        move.performed += OnMove;
        move.canceled += OnMove;
        look = input.Player.Look;
        look.Enable();
        look.performed += OnLook;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        move.Disable();
        move.performed -= OnMove;
        move.canceled -= OnMove;
        look.Disable();
        look.performed -= OnLook;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        win.SetActive(false);
    }

    void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 movementVector = context.ReadValue<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
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

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 4)
        {
            win.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = cameraForward * movementY + cameraRight * movementX;
        movement.Normalize();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("collectible"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
        if (other.gameObject.CompareTag("win"))
        {
            winTextObject.SetActive(true);
        }
    }

}