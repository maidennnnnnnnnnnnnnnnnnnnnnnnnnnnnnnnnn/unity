using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject exit;
    public float moveSpeed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            winScreen.SetActive(true);
            StartCoroutine("Waiter");
        }
    }
    IEnumerator Waiter()
    {
        yield return new WaitForSecondsRealtime(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return null;
    }
}