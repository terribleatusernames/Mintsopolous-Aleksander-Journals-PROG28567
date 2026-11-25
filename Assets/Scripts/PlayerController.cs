using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed = 10f;
    Collider2D player;
    ContactFilter2D filter = new ContactFilter2D(); 

    private float apexHeight = 12f;
    private float apexTIme = 4f;


    public enum FacingDirection
    {
        left, right
    }

    void Start()
    {
        player = GetComponent<Collider2D>();

        filter.SetLayerMask(LayerMask.GetMask("Ground"));
        
        rb = GetComponent<Rigidbody2D>();

        
    }

    void Update()
    {
        
        Vector2 playerInput = new Vector2();

        if(Keyboard.current.leftArrowKey.isPressed)
        {
            playerInput.x = -1;
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            playerInput.x = 1;
        }

        if (Keyboard.current.upArrowKey.wasPressedThisFrame && IsGrounded())
        {
            rb.AddForce(new Vector2(0, (2 * apexHeight / apexTIme)), ForceMode2D.Impulse);
        }

        if (IsGrounded())
        {
            rb.gravityScale = 1;
        }
        else
        {
            rb.gravityScale = 2 * apexHeight / Mathf.Pow(apexTIme, 2);
        }


        MovementUpdate(playerInput);
    }


    private void MovementUpdate(Vector2 playerInput)
    {
        rb.AddForce(playerInput * moveSpeed, ForceMode2D.Force);
    }

    public bool IsWalking()
    {
        return rb.linearVelocity.x > 0.1f || rb.linearVelocity.x < -0.1f;
    }
    public bool IsGrounded()
    {
        return player.IsTouching(filter);
    }

    public FacingDirection GetFacingDirection()
    { 
        return (rb.linearVelocity.x > 0.1f) ? FacingDirection.right : (rb.linearVelocity.x < -0.1f) ? FacingDirection.left : FacingDirection.left;
    }
}
