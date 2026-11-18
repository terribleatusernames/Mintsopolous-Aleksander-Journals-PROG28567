using UnityEngine;
using UnityEngine.InputSystem;

public class PhsyicsTesting : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector3 inputDirection;
    public float moveSpeed = 10f;
    public float rotationSpeed = 360f;

    public float maxSpeed = 10f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
   
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            rb.MoveRotation(rb.rotation + rotationSpeed * Time.deltaTime);
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            rb.MoveRotation(rb.rotation - rotationSpeed * Time.deltaTime);
        }

  
        inputDirection = Vector2.zero;

        if (Keyboard.current.upArrowKey.isPressed)
        {
            inputDirection += transform.up;
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            inputDirection += -transform.up;
        }

     
        if (inputDirection.magnitude > 0)
        {
            
            inputDirection.Normalize();

            rb.AddForce(inputDirection * moveSpeed, ForceMode2D.Force);
        }
    }
}
